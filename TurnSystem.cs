using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using System.Runtime.InteropServices;

public class TurnSystem : MonoBehaviour
{

    public static int stage;
    private string gimmick = null;
    private GameObject Row4;
    private GameObject Row5;
    public GameObject PrefabRow4;
    public GameObject PrefabRow5;

    public GameObject VinePrefab;

    public static int attackTurn = 20; // 공격 횟수
    private bool isRage = false; // 광폭화 여부
    public static int GoliathMaxHP; // 최대 체력
    public static int GoliathHP; // 현재 체력

    public int AbleMove = 0;
    public static int moveTurn = 0; // 이동 가능 횟수
    public static string attackShape; // 공격의 유형

    public int sumPiece; // 기물의 점수 합
    public int maxSumIndexI; // 공격 방식의 중심 행
    public int maxSumIndexJ; // 공격 방식의 중심 열

    private bool isGameOver = false;
    public static bool KingAlive = true;

    public static int[,] ReferenceSheet = new int[6,6]; // 참고 sheet (공격범위 참고용 sheet)
    public static float[,] BuffSheet = new float[6,6];
    public static int[,] GimmickSheet = new int[6, 6]; //기믹 sheet 각종 타일 기믹을 여기서 처리할 예정;
    int previousAttack = -1; //0 : horizontal, 1 : vertical, 2 : X, 3: cross, 4 : rectangle
    int random_number = -1; //AttackShape 결정


    

    //추가 
    //오브젝트에 TMP으로 텍스트 ui추가 필요함
    public GameObject wintxt;//엔드시 승리 텍스트 띄우기용
    public GameObject overtxt;//엔드시 게임오버 텍스트 띄우기 용
    public GameObject board;// 엔드시 보드를 없애서 추가 조작 막는용도
    public GameObject DirectionBoard; // 방향 설정 보드
    public GameObject obj; // BoardManagement
    public GameObject TurnTable; // 턴/이동횟수/특이사항 띄워주는 이미지

    public GameObject ForStage; //stage ui canvas 할당
    public GameObject ForReady; //readyphase ui canvs 할당

    public GameObject shop;
    public GameObject pieceList;
    public GameObject MainCamera;

    private GameObject DefaultKing;
    private GameObject DefaultQueen;


    private void Awake()
    {
        obj = GameObject.Find("BoardManagement");
        for(int i = 0; i <6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GimmickSheet[i, j] = 0;
            }
        }
        StageSetting();
        UISetting();
        
    }



    //기본적인 턴 루프를 만든다고 해보면...
    // Battle 을 더 큰 개념으로..
    // 순서 ReadyPhase -> BattlePhase (StartTurn ~ EndTurn 루프) -> EndPhase

    
    private void StageSetting() //스테이지 기본 세팅...
    {/*
        Destroy(obj.transform.GetChild(4).gameObject);
        Destroy(obj.transform.GetChild(5).gameObject);
        Row4 = Instantiate(PrefabRow4);
        Row5 = Instantiate(PrefabRow5);
        Row4.name = "4";
        Row5.name = "5";
        Row4.transform.parent = obj.transform;
        Row5.transform.parent = obj.transform;
        */
        CostCheck.PieceCost = 0;
        GameRestart();
        StageCheck();
    }

    private void UISetting() // UI 셋팅... ReadyPhase UI 상태로 만들어줘야 함....
    {
        wintxt.SetActive(false);
        overtxt.SetActive(false);
        DirectionBoard.SetActive(false);
        ForStage.SetActive(false);
    }

    private void StageCheck() //스테이지 관련 정보 입력...
    {
        switch (stage)
        {
            case 0: // 튜토리얼
                GoliathMaxHP = 2500;
                GoliathHP = 2500;
                CostCheck.MaxCost = 17;
                attackTurn = 20;
                gimmick = null;
                ReadyPhase();
                StartTutorial();

                break;
            case 1: // 숲1 골리앗 정보
                GoliathMaxHP = 3000;
                GoliathHP = 3000;
                CostCheck.MaxCost = 17;
                attackTurn = 20;
                gimmick = "DoubleAttack";
                ReadyPhase();
                break;

            case 2:
                GoliathMaxHP = 3500;
                GoliathHP = 3500;
                CostCheck.MaxCost = 17;
                attackTurn = 20;
                gimmick = "Vine";
                ReadyPhase();
                break;
                

        }
    }

    // 1. ReadyPhase : 병력 구매, 배치, 강화하는 페이즈
    public void ReadyPhase()
    {
        pieceList.SetActive(true);
        shop.SetActive(false);
        ForReady.SetActive(true);
        MainCamera.transform.position = new Vector3(8, 150, 25);
        MainCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void StartTutorial()
    {
        //튜토리얼 대화 스크립트 삽입.
        ReadyPhase();
    }

    public void StartBattlePhase()
    {
        // 튜토리얼 시작 로직 구현
        ForReady.SetActive(false); //S.B
        MainCamera.transform.position = new Vector3(5, 105, -40); //S.B
        MainCamera.transform.rotation = Quaternion.Euler(55, 0, 0); //S.B
        DefaultKing = GameObject.FindWithTag("King");//S.B
        DefaultQueen = GameObject.FindWithTag("Queen");
        DefaultKing.GetComponent<Piece>().damage = 30;
        switch (QueenScript.QueenUpgrade)
        {
            case 0:
                DefaultQueen.GetComponent<Piece>().damage = 20;
                break;
            case 1:
                DefaultQueen.GetComponent<Piece>().damage = 30;
                break;
            case 2:
                DefaultQueen.GetComponent<Piece>().damage = 45;
                break;
            case 3:
                DefaultQueen.GetComponent<Piece>().damage = 65;
                break;
        }
        DefaultKing.GetComponent<Piece>().RefreshPosition();
        DefaultQueen.GetComponent<Piece>().RefreshPosition();
        ForStage.SetActive(true);
        BoardManagement.RemainMove = GameObject.Find("RemainMove");

        for (int i = 4; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (i == 4 || j != 2&&j != 3)
                {
                    Destroy(obj.transform.GetChild(i).GetChild(j).GetChild(0).gameObject);
                    Destroy(obj.transform.GetChild(i).GetChild(j).GetComponent<ArrangeTile>());
                }
            }
        }
        SliderScript.isStartSlide = true;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                ReferenceSheet[i, j] = 0;
                BuffSheet[i, j] = 1;
            }
        }
        attackTurn--;// 공격 횟수 감소
        AttackPosition(); // 공격 범위
        ShowGoliathAttackRange(); // 범위 표시
        //GoliathPattern();
        CalTurn(); // 턴 계산
        SliderScript.isCounting = true;

    }

    private void StartTurn()
    {
        if (attackTurn > 0) // 공격 횟수가 0이 될 때까지
        {

            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    ReferenceSheet[i, j] = 0;
                    BuffSheet[i, j] = 1;
                }
            }
            attackTurn--;// 공격 횟수 감소
            AttackPosition(); // 공격 범위
            ShowGoliathAttackRange(); // 범위 표시
            GoliathPattern();
            DirectionBoard.SetActive(true);
        }

    }

    private void AttackPosition() //공격 위치 설정
    {
        
        System.Random random = new System.Random();
        //randomly decide the type of attack but exclude right before attackShape
        while (true)
        {
            random_number = random.Next(5);
            if (previousAttack == random_number)
            {
                continue;
            }
            if (attackTurn > 16 && random_number < 2)
            {
                continue;
            }
            else
            {
                break;
            }
        }
        switch (random_number)
        {
            case 0:
                attackShape = "Horizontal";
                break;
            case 1:
                attackShape = "Vertical";
                break;
            case 2:
                attackShape = "X";
                break;
            case 3:
                attackShape = "Cross";
                break;
            case 4:
                attackShape = "Rectangle";
                break;
        }

        previousAttack = random_number;
        
        int[,] piecesheet = BoardManagement.sheet;

        int CalCross(int i, int j)
        {
            int sumPiece = 0;
            if (piecesheet[i, j] != 0)
            {
                sumPiece += piecesheet[i, j];
            }
            if (piecesheet[i + 1, j] != 0)
            {
                sumPiece += piecesheet[i + 1, j];
            }
            if (piecesheet[i - 1, j] != 0)
            {
                sumPiece += piecesheet[i - 1, j];
            }
            if (piecesheet[i, j + 1] != 0)
            {
                sumPiece += piecesheet[i, j + 1];
            }
            if (piecesheet[i, j - 1] != 0)
            {
                sumPiece += piecesheet[i, j - 1];
            }
            return sumPiece;
        }

        int CalX(int i, int j)
        {
            int sumPiece = 0;
            if (piecesheet[i, j] != 0)
            {
                sumPiece += piecesheet[i, j];
            }
            if (piecesheet[i + 1, j + 1] != 0)
            {
                sumPiece += piecesheet[i + 1, j + 1];
            }
            if (piecesheet[i - 1, j + 1] != 0)
            {
                sumPiece += piecesheet[i - 1, j + 1];
            }
            if (piecesheet[i + 1, j - 1] != 0)
            {
                sumPiece += piecesheet[i + 1, j - 1];
            }
            if (piecesheet[i - 1, j - 1] != 0)
            {
                sumPiece += piecesheet[i - 1, j - 1];
            }
            return sumPiece;
        }

        int CalRec(int i, int j)
        {
            int sumPiece = 0;
            if (piecesheet[i + 1, j + 1] != 0)
            {
                sumPiece += piecesheet[i + 1, j + 1];
            }
            if (piecesheet[i, j + 1] != 0)
            {
                sumPiece += piecesheet[i, j + 1];
            }
            if (piecesheet[i + 1, j] != 0)
            {
                sumPiece += piecesheet[i + 1, j];
            }
            if (piecesheet[i, j] != 0)
            {
                sumPiece += piecesheet[i, j];
            }
            return sumPiece;
        }
        int maxSum;
        switch (attackShape)
        {
            case "Horizontal":
                maxSum = 0;
                maxSumIndexI = -1;
                maxSumIndexJ = -1;
                for (int i = 0; i < 6; i++)
                {
                    sumPiece = 0;
                    for (int j = 0; j < 6; j++)
                    {
                        if (piecesheet[i, j] != 0)
                        {
                            sumPiece += piecesheet[i, j];
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (sumPiece > maxSum) // 현재 합이 최대 합보다 크다면
                    {
                        maxSum = sumPiece; // 최대 합을 업데이트
                        maxSumIndexI = i; // 최대 합이 발생한 인덱스(i)를 저장
                    }
                }
                break;
            case "Vertical":
                maxSum = 0;
                maxSumIndexI = -1;
                maxSumIndexJ = -1;
                for (int i = 0; i < 6; i++)
                {
                    sumPiece = 0;
                    for (int j = 0; j < 6; j++)
                    {
                        if (piecesheet[j, i] != 0)
                        {
                            sumPiece += piecesheet[j, i];
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (sumPiece > maxSum) // 현재 합이 최대 합보다 크다면
                    {
                        maxSum = sumPiece; // 최대 합을 업데이트
                        maxSumIndexJ = i;
                    }
                }
                break;

            case "X":
                maxSum = 0;
                maxSumIndexI = -1;
                maxSumIndexJ = -1;
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        int XSum = CalX(i, j);
                        if (XSum > maxSum)
                        {
                            maxSum = XSum;
                            attackShape = "X";
                            maxSumIndexI = i;
                            maxSumIndexJ = j;
                        }
                    }
                }
                break;

            case "Cross":
                maxSum = 0;
                maxSumIndexI = -1;
                maxSumIndexJ = -1;
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        int CrosSum = CalCross(i, j);
                        if (CrosSum > maxSum)
                        {
                            maxSum = CrosSum;
                            maxSumIndexI = i;
                            maxSumIndexJ = j;
                        }

                    }
                }
                break;

            case "Rectangle":
                maxSum = 0;
                maxSumIndexI = -1;
                maxSumIndexJ = -1;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        int RecSum = CalRec(i, j);
                        if (RecSum > maxSum)
                        {
                            maxSum = RecSum;
                            maxSumIndexI = i;
                            maxSumIndexJ = j;
                        }
                    }
                }
                break;
        }
        

        if (attackShape == "Cross" || attackShape == "X") // 십자 모양 공격과 X자형 공격인 경우
        {
            AbleMove = 3;
        }
        else if (attackShape == "Vertical" || attackShape == "Horizontal" || attackShape == "Rectangle")
        {
            AbleMove = 4;
        }
        if (isRage) // 골리앗 광폭화 시 moveTurn 1 감소.
        {
            AbleMove -= 1;
        }
        TurnTable.transform.GetChild(2).GetComponent<Text>().text = $"{AbleMove}";
    }


    public void EndTurn() //End Turn 진입 (기물들의 움직임 수 소진시...)
    {

        GoliathAttack(); //골리앗의 공격 진행 함수 호출

        CalGoliathHP(); // 골리앗의 체력 감소 계산 함수 호출

        //추가
        CheckGoliath();//골리앗의 생사확인

        Debug.Log($"턴이 종료되었습니다. {attackTurn} 턴이 남았습니다.");

        //추가
        CheckTrun();//남은턴수 확인

        if (KingAlive==true && isGameOver == false)
        {
            StartTurn(); //스테이지 루프 활성화
        }

        
    }

    private void ShowGoliathAttackRange()
    {
        switch (attackShape)
        {
            case "Cross":
                obj.transform.GetChild(maxSumIndexI).GetChild(maxSumIndexJ-1).GetComponent<Tile>().GoliathRange = 1;
                ReferenceSheet[maxSumIndexI, maxSumIndexJ-1] = -2;
                for (int i = -1; i < 2; i++)
                {
                    obj.transform.GetChild(maxSumIndexI+i).GetChild(maxSumIndexJ).GetComponent<Tile>().GoliathRange = 1;
                    ReferenceSheet[maxSumIndexI+i, maxSumIndexJ] = -2;
                }
                obj.transform.GetChild(maxSumIndexI).GetChild(maxSumIndexJ + 1).GetComponent<Tile>().GoliathRange = 1;
                ReferenceSheet[maxSumIndexI, maxSumIndexJ + 1] = -2;
                break;
            case "Rectangle":
                for (int i = 0; i < 2; i++)
                {
                    for (int j =0;j <2; j++)
                    {
                        obj.transform.GetChild(maxSumIndexI+i).GetChild(maxSumIndexJ+j).GetComponent<Tile>().GoliathRange = 1;
                        ReferenceSheet[maxSumIndexI+i, maxSumIndexJ + j] = -2;
                    }
                }
                break;
            case "X":
                obj.transform.GetChild(maxSumIndexI + 1).GetChild(maxSumIndexJ - 1).GetComponent<Tile>().GoliathRange = 1;
                ReferenceSheet[maxSumIndexI + 1, maxSumIndexJ -1] = -2;
                obj.transform.GetChild(maxSumIndexI - 1).GetChild(maxSumIndexJ + 1).GetComponent<Tile>().GoliathRange = 1;
                ReferenceSheet[maxSumIndexI - 1, maxSumIndexJ + 1] = -2;
                for (int i = -1; i < 2; i++)
                {
                    obj.transform.GetChild(maxSumIndexI + i).GetChild(maxSumIndexJ+i).GetComponent<Tile>().GoliathRange = 1;
                    ReferenceSheet[maxSumIndexI + i, maxSumIndexJ+i] = -2;
                }
                break;
            case "Vertical":
                for(int a = 0; a < 6; a++)
                {
                    obj.transform.GetChild(a).GetChild(maxSumIndexJ).GetComponent<Tile>().GoliathRange = 1;
                    ReferenceSheet[a, maxSumIndexJ] = -2;
                }
                break;
            case "Horizontal":
                for (int a = 0; a < 6; a++)
                {
                    obj.transform.GetChild(maxSumIndexI).GetChild(a).GetComponent<Tile>().GoliathRange = 1;
                    ReferenceSheet[maxSumIndexI, a] = -2;
                }
                break;

        }
    }

    private void GoliathPattern()
    {
        System.Random random = new System.Random();
        switch (gimmick)
        {
            case "DoubleAttack":
                if (attackTurn % 4 == 0)
                {
                    int randomIndexI = maxSumIndexI;
                    int randomIndexJ = maxSumIndexJ;
                    while (randomIndexI == maxSumIndexI || randomIndexJ == maxSumIndexJ)
                    {
                        randomIndexI = random.Next(1,4);
                        randomIndexJ = random.Next(1,4);
                    }
                    maxSumIndexI = randomIndexI;
                    maxSumIndexJ = randomIndexJ;
                    ShowGoliathAttackRange();
                }
                break;
            case "Vine":
                int VineI = random.Next(0, 5);
                int VineJ = random.Next(0, 5);
                while (BoardManagement.sheet[VineI, VineJ] !=0 || GimmickSheet[VineI,VineJ] !=0 )
                {
                    VineI = random.Next(0, 5);
                    VineJ = random.Next(0, 5);
                }
                GimmickSheet[VineI, VineJ] = 1;
                GameObject VineModel = Instantiate(VinePrefab);
                VineModel.name = $"{VineI}-{VineJ}";
                VineModel.transform.position = new Vector3(-20 + (10*VineJ), 2, 50 - (10*VineI));
                board.transform.GetChild(VineI).GetChild(VineJ).GetComponent<Tile>().Gimmick = 1;
                break;
        }
    }

    public void CalTurn()
    {
        moveTurn = AbleMove;
        TurnTable.transform.GetChild(0).GetComponent<Text>().text = $"남은 턴 수 : {attackTurn}";
        DirectionBoard.SetActive(false);
        SliderScript.isCounting = true;
        GameObject.Find("SliderScript").GetComponent<SliderScript>().timeRemaining = 15f;
    }

    private void GoliathAttack()
    {
        for(int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (ReferenceSheet[i, j] == -2)
                {
                    obj.transform.GetChild(i).GetChild(j).GetComponent<Tile>().DestroyPiece();
                }
            }
        }
    }

    private void CalGoliathHP() // 골리앗의 체력을 얼마나 감소시킬지 계산
    {
        int turndamage = DamageSum();
        GoliathHP -= turndamage;
        if (GoliathHP < GoliathMaxHP*0.5f && isRage == false)
        {
            isRage = true;
            TurnTable.transform.GetChild(4).GetComponent<Text>().text = "골리앗 광폭화";
        }
        Debug.Log($"총 {turndamage} 의 피해를 골리앗에게 주어 골리앗의 체력이 {GoliathHP} 이 되었습니다");
    }

    private bool SkipRequested()
    {
        return false;
        // Skip 요청 확인 로직 구현
    }

    private int DamageSum() //공격력 계산 함수
    {
        int[,] damage_sheet = new int[6, 6];
        damage_sheet = BoardManagement.sheet;
        GameObject[] AllRook = GameObject.FindGameObjectsWithTag("Rook");
        GameObject[] AllBishop = GameObject.FindGameObjectsWithTag("Bishop");
        int attack = 0;

        if (AllRook != null)
        {
            foreach(GameObject RookPiece in AllRook)
            {
                RookPiece.GetComponent<RookScript>().RookBuffSkill();
            }
        }
        if (AllBishop != null)
        {
            foreach (GameObject BishopPiece in AllBishop)
            {
                BishopPiece.GetComponent<BishopScript>().BishopBuffSkill();
            }
        }

        AllRook = null;
        AllBishop = null;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (damage_sheet[i, j] > 0)
                { 
                    attack += Mathf.FloorToInt((float)damage_sheet[i,j] * BuffSheet[i,j]);
                }
            }
        }
        /*
        for (int k = 0; k < 6; k++)
        {
            Debug.Log($"{BuffSheet[k, 0]}    {BuffSheet[k, 1]}   {BuffSheet[k, 2]}   {BuffSheet[k, 3]}   {BuffSheet[k, 4]}   {BuffSheet[k, 5]}");
        }
        */
        return attack; //공격력 총합 반환
    }



    //아래 내용 전부 추가
    /*public void WhenBattleIsOver()
    {
        gameObject.SetActive(true)
    }*/


    //Create two button and show button of Restart and Map when Battle is over.
    public void GameWin()
    {
        //게임 승리 UI 활성화 일단 텍스트만 뜨게함
        if (MapIntroduce.currentStage > PlayerPrefs.GetInt("ClearedStage"))
        {
            PlayerPrefs.SetInt("ClearedStage") = MapIntroduce.currentStage;
            MapIntroduce.clearedStage = MapIntroduce.currentStage;
            PlayerPrefs.Save();
        }           
        wintxt.SetActive(true);
        board.SetActive(false);
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;


            // 게임 오버 UI 활성화

            overtxt.SetActive(true);//일단 텍스트만 뜨게 구현함
            board.SetActive(false);
        }
    }
    public void GameRestart()// 다시하기했을 경우를 위해 만듬
    {

        isGameOver = false;
        KingAlive = true;
        Tile.IsQueenAbility = true;
        //GotoBattleScene()
    }

    public void CheckKing()//킹이 살아있는지를 확인하는 코드 없으면 GameOver 호출
    {
        /*
        GameObject FindKing = GameObject.FindWithTag("King");
        Debug.Log(FindKing);
        
        if (BoardManagement.sheet[FindKing.GetComponent<KingScript>().posRow, FindKing.GetComponent<KingScript>().posCol] ==0)
        {
            KingAlive = false;
            GameOver();
        }

        if (KingAlive == false)
        {
            //queen 능력 체크 구간

            GameOver();
        }
        */

    }
 
    public void CheckGoliath()
    {
        if (GoliathHP < 0)
        {
            isGameOver = true;
            GameWin();
        }
    }

    public void CheckTrun()
    {
        if (attackTurn <= 0)
        {
            GameOver();
        }
    }


}