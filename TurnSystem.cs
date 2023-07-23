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

    public static int attackTurn = 20; // ���� Ƚ��
    private bool isRage = false; // ����ȭ ����
    public static int GoliathMaxHP; // �ִ� ü��
    public static int GoliathHP; // ���� ü��

    public int AbleMove = 0;
    public static int moveTurn = 0; // �̵� ���� Ƚ��
    public static string attackShape; // ������ ����

    public int sumPiece; // �⹰�� ���� ��
    public int maxSumIndexI; // ���� ����� �߽� ��
    public int maxSumIndexJ; // ���� ����� �߽� ��

    private bool isGameOver = false;
    public static bool KingAlive = true;

    public static int[,] ReferenceSheet = new int[6,6]; // ���� sheet (���ݹ��� ����� sheet)
    public static float[,] BuffSheet = new float[6,6];
    public static int[,] GimmickSheet = new int[6, 6]; //��� sheet ���� Ÿ�� ����� ���⼭ ó���� ����;
    int previousAttack = -1; //0 : horizontal, 1 : vertical, 2 : X, 3: cross, 4 : rectangle
    int random_number = -1; //AttackShape ����


    

    //�߰� 
    //������Ʈ�� TMP���� �ؽ�Ʈ ui�߰� �ʿ���
    public GameObject wintxt;//����� �¸� �ؽ�Ʈ �����
    public GameObject overtxt;//����� ���ӿ��� �ؽ�Ʈ ���� ��
    public GameObject board;// ����� ���带 ���ּ� �߰� ���� ���¿뵵
    public GameObject DirectionBoard; // ���� ���� ����
    public GameObject obj; // BoardManagement
    public GameObject TurnTable; // ��/�̵�Ƚ��/Ư�̻��� ����ִ� �̹���

    public GameObject ForStage; //stage ui canvas �Ҵ�
    public GameObject ForReady; //readyphase ui canvs �Ҵ�

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



    //�⺻���� �� ������ ����ٰ� �غ���...
    // Battle �� �� ū ��������..
    // ���� ReadyPhase -> BattlePhase (StartTurn ~ EndTurn ����) -> EndPhase

    
    private void StageSetting() //�������� �⺻ ����...
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

    private void UISetting() // UI ����... ReadyPhase UI ���·� �������� ��....
    {
        wintxt.SetActive(false);
        overtxt.SetActive(false);
        DirectionBoard.SetActive(false);
        ForStage.SetActive(false);
    }

    private void StageCheck() //�������� ���� ���� �Է�...
    {
        switch (stage)
        {
            case 0: // Ʃ�丮��
                GoliathMaxHP = 2500;
                GoliathHP = 2500;
                CostCheck.MaxCost = 17;
                attackTurn = 20;
                gimmick = null;
                ReadyPhase();
                StartTutorial();

                break;
            case 1: // ��1 �񸮾� ����
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

    // 1. ReadyPhase : ���� ����, ��ġ, ��ȭ�ϴ� ������
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
        //Ʃ�丮�� ��ȭ ��ũ��Ʈ ����.
        ReadyPhase();
    }

    public void StartBattlePhase()
    {
        // Ʃ�丮�� ���� ���� ����
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
        attackTurn--;// ���� Ƚ�� ����
        AttackPosition(); // ���� ����
        ShowGoliathAttackRange(); // ���� ǥ��
        //GoliathPattern();
        CalTurn(); // �� ���
        SliderScript.isCounting = true;

    }

    private void StartTurn()
    {
        if (attackTurn > 0) // ���� Ƚ���� 0�� �� ������
        {

            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    ReferenceSheet[i, j] = 0;
                    BuffSheet[i, j] = 1;
                }
            }
            attackTurn--;// ���� Ƚ�� ����
            AttackPosition(); // ���� ����
            ShowGoliathAttackRange(); // ���� ǥ��
            GoliathPattern();
            DirectionBoard.SetActive(true);
        }

    }

    private void AttackPosition() //���� ��ġ ����
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
                    if (sumPiece > maxSum) // ���� ���� �ִ� �պ��� ũ�ٸ�
                    {
                        maxSum = sumPiece; // �ִ� ���� ������Ʈ
                        maxSumIndexI = i; // �ִ� ���� �߻��� �ε���(i)�� ����
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
                    if (sumPiece > maxSum) // ���� ���� �ִ� �պ��� ũ�ٸ�
                    {
                        maxSum = sumPiece; // �ִ� ���� ������Ʈ
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
        

        if (attackShape == "Cross" || attackShape == "X") // ���� ��� ���ݰ� X���� ������ ���
        {
            AbleMove = 3;
        }
        else if (attackShape == "Vertical" || attackShape == "Horizontal" || attackShape == "Rectangle")
        {
            AbleMove = 4;
        }
        if (isRage) // �񸮾� ����ȭ �� moveTurn 1 ����.
        {
            AbleMove -= 1;
        }
        TurnTable.transform.GetChild(2).GetComponent<Text>().text = $"{AbleMove}";
    }


    public void EndTurn() //End Turn ���� (�⹰���� ������ �� ������...)
    {

        GoliathAttack(); //�񸮾��� ���� ���� �Լ� ȣ��

        CalGoliathHP(); // �񸮾��� ü�� ���� ��� �Լ� ȣ��

        //�߰�
        CheckGoliath();//�񸮾��� ����Ȯ��

        Debug.Log($"���� ����Ǿ����ϴ�. {attackTurn} ���� ���ҽ��ϴ�.");

        //�߰�
        CheckTrun();//�����ϼ� Ȯ��

        if (KingAlive==true && isGameOver == false)
        {
            StartTurn(); //�������� ���� Ȱ��ȭ
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
        TurnTable.transform.GetChild(0).GetComponent<Text>().text = $"���� �� �� : {attackTurn}";
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

    private void CalGoliathHP() // �񸮾��� ü���� �󸶳� ���ҽ�ų�� ���
    {
        int turndamage = DamageSum();
        GoliathHP -= turndamage;
        if (GoliathHP < GoliathMaxHP*0.5f && isRage == false)
        {
            isRage = true;
            TurnTable.transform.GetChild(4).GetComponent<Text>().text = "�񸮾� ����ȭ";
        }
        Debug.Log($"�� {turndamage} �� ���ظ� �񸮾ѿ��� �־� �񸮾��� ü���� {GoliathHP} �� �Ǿ����ϴ�");
    }

    private bool SkipRequested()
    {
        return false;
        // Skip ��û Ȯ�� ���� ����
    }

    private int DamageSum() //���ݷ� ��� �Լ�
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
        return attack; //���ݷ� ���� ��ȯ
    }



    //�Ʒ� ���� ���� �߰�
    /*public void WhenBattleIsOver()
    {
        gameObject.SetActive(true)
    }*/


    //Create two button and show button of Restart and Map when Battle is over.
    public void GameWin()
    {
        //���� �¸� UI Ȱ��ȭ �ϴ� �ؽ�Ʈ�� �߰���
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


            // ���� ���� UI Ȱ��ȭ

            overtxt.SetActive(true);//�ϴ� �ؽ�Ʈ�� �߰� ������
            board.SetActive(false);
        }
    }
    public void GameRestart()// �ٽ��ϱ����� ��츦 ���� ����
    {

        isGameOver = false;
        KingAlive = true;
        Tile.IsQueenAbility = true;
        //GotoBattleScene()
    }

    public void CheckKing()//ŷ�� ����ִ����� Ȯ���ϴ� �ڵ� ������ GameOver ȣ��
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
            //queen �ɷ� üũ ����

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