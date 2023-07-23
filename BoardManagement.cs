using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.UI;

/*
sheet의 존재

2차원 배열인 sheet를 도입.

양수 : 기물의 데미지, 기물이 존재하는지 체크
0 : 빈공간
-1 : 이동 가능 공간 
-2 이하의 음수 : 타일에 영향을 주는 보스 기믹


오브젝트의 관계

BoardManage {BoardManagement.cs} (보드 전체를 총괄, 이동 계산)
    상속 : 0~5 (행의 역할 수행, 실체는 존재하지 않음)
        상속 : 0~5 {Tile.cs} (열의 역할 수행, 체스 판 하나하나를 담당, 이동 범위 표시)
            상속 : rook,pawn etc. {piece.cs} (기물, 기물의 이동 원리는 타일에 상속 시키는 것)
 */

public class BoardManagement : MonoBehaviour
{ 
    public static int[,] sheet = new int[6, 6]; // 2차원 배열 추가 (가상의 체스판이라고 생각)
    public static int selectedRow = -1; // 선택된 기물의 좌표
    public static int selectedCol = -1; 
    static GameObject obj;
    public static GameObject RemainMove;



    private void Awake()
    {
        obj = GameObject.Find("BoardManagement");
        for (int a = 0; a < 6; a++)
        {
            for (int b = 0; b < 6; b++)
            {
                sheet[a, b] = 0;
            }
        }
    }

    public static void HideRange() //이동 가능 범위 숨기기 함수
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if (sheet[i, j] == -1)
                {
                    sheet[i, j] = 0;
                    obj.transform.GetChild(i).GetChild(j).GetComponent<Tile>().value = 0;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public static void Movement(int posRow, int posCol) //이동 함수 (목적지 행, 목적지 열)
    {
        HideRange();
        GameObject MovePiece = obj.transform.GetChild(selectedRow).GetChild(selectedCol).GetChild(0).gameObject;
        MovePiece.transform.parent = obj.transform.GetChild(posRow).GetChild(posCol); //기물을 목적지 타일에 상속


        SliderScript.isCounting = false;
        int RowDiff = selectedRow - posRow; // 전진 : 양수 / 후진 : 음수 
        int ColDiff = selectedCol - posCol; // 왼쪽 : 양수 / 오른쪽 : 음수 

        if (RowDiff == 0)
        {
            if (ColDiff > 0)
            {
                MovePiece.GetComponent<unit_animation>().setmove(1, ColDiff);
            }
            else
            {
                MovePiece.GetComponent<unit_animation>().setmove(2, -ColDiff);
            }
        }
        else if (ColDiff == 0)
        {
            if (RowDiff > 0)
            {
                MovePiece.GetComponent<unit_animation>().setmove(0, RowDiff);
            }
            else
            {
                MovePiece.GetComponent<unit_animation>().setmove(3, -RowDiff);
            }
        }
        else
        {
            if (RowDiff > 0)
            {
                if (ColDiff > 0)
                {
                    MovePiece.GetComponent<unit_animation>().setmove(5, RowDiff);
                }

                else
                {
                    MovePiece.GetComponent<unit_animation>().setmove(4, RowDiff);
                }
            }

            else
            {
                if (ColDiff > 0)
                {
                    MovePiece.GetComponent<unit_animation>().setmove(7, -RowDiff);
                }

                else
                {
                    MovePiece.GetComponent<unit_animation>().setmove(6, -RowDiff);
                }
            }
        }

        sheet[selectedRow, selectedCol] = 0; //가상 체스판에서 이동 처리
        //MovePiece.GetComponent<Piece>().RefreshPosition(); //위치 새로고침 함수 호출

        RemainMove.GetComponent<Text>().text = TurnSystem.moveTurn.ToString();
        
        for(int i = 0; i < 6; i++)
        {
            Debug.Log($"{sheet[i, 0]}   {sheet[i, 1]}   {sheet[i, 2]}   {sheet[i, 3]}   {sheet[i, 4]}   {sheet[i, 5]}");
        }
    }

    public static void RookRange(int posRow,int posCol) //rook의 이동 범위 표시 함수
    {
        for (int a = 1; a + posRow < 6; a++)
        {
            if (sheet[a + posRow, posCol] == 0) // 빈 공간인지 확인
            {
                sheet[a + posRow, posCol] = -1; //가상 체스판에서 상태 변화 처리
                obj.transform.GetChild(a + posRow).GetChild(posCol).GetComponent<Tile>().value = 1; //Tile 부분의 value 호출 (이동 가능 범위 표시)
            }
            else //기물이 있을 경우 더 이상 전진하지 않음
            {

                break;
            }
        }
        for (int a = 1; posRow - a > -1; a++)
        {
            if (sheet[posRow - a, posCol] == 0)
            {
                sheet[posRow - a, posCol] = -1;
                obj.transform.GetChild(posRow - a).GetChild(posCol).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
        for (int a = 1; a + posCol < 6; a++)
        {
            if (sheet[posRow, a + posCol] == 0)
            {
                sheet[posRow, a + posCol] = -1;
                obj.transform.GetChild(posRow).GetChild(a + posCol).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
        for (int a = 1; posCol - a > -1; a++)
        {
            if (sheet[posRow, posCol - a] == 0)
            {
                sheet[posRow, posCol - a] = -1;
                obj.transform.GetChild(posRow).GetChild(posCol - a).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
    }

    public static void PawnRange(int posRow,int posCol,int movetime, string ahead) //pawn의 이동 범위 표시 함수
    {
        if (ahead == "forward") //지정 방향 : 앞으로
        {
            if (posRow >= 1 && sheet[posRow - 1, posCol] == 0)
            {
                sheet[posRow - 1, posCol] = -1;
                obj.transform.GetChild(posRow - 1).GetChild(posCol).GetComponent<Tile>().value = 1;

                if (posRow >= 2 && sheet[posRow - 2, posCol] == 0 && movetime == 0) //첫 이동은 2칸 이동 가능
                {
                    sheet[posRow - 2, posCol] = -1;
                    obj.transform.GetChild(posRow - 2).GetChild(posCol).GetComponent<Tile>().value = 1;
                }
            }
        }
        if (ahead == "back")
        {
            if (posRow <= 4 && sheet[posRow + 1, posCol] == 0)
            {
                sheet[posRow + 1, posCol] = -1;
                obj.transform.GetChild(posRow + 1).GetChild(posCol).GetComponent<Tile>().value = 1;

                if (posRow <= 3 && sheet[posRow + 2, posCol] == 0 && movetime == 0) //첫 이동은 2칸 이동 가능
                {
                    sheet[posRow + 2, posCol] = -1;
                    obj.transform.GetChild(posRow + 2).GetChild(posCol).GetComponent<Tile>().value = 1;
                }
            }
        }
        if (ahead == "right")
        {
            if (posCol <= 4 && sheet[posRow, posCol + 1] == 0)
            {
                sheet[posRow, posCol + 1] = -1;
                obj.transform.GetChild(posRow).GetChild(posCol + 1).GetComponent<Tile>().value = 1;

                if (posCol <= 3 && sheet[posRow, posCol + 2] == 0 && movetime == 0) //첫 이동은 2칸 이동 가능
                {
                    sheet[posRow, posCol + 2] = -1;
                    obj.transform.GetChild(posRow).GetChild(posCol + 2).GetComponent<Tile>().value = 1;
                }
            }
        }
        if (ahead == "left")
        {
            if (posCol >= 1 && sheet[posRow, posCol - 1] == 0)
            {
                sheet[posRow, posCol - 1] = -1;
                obj.transform.GetChild(posRow).GetChild(posCol - 1).GetComponent<Tile>().value = 1;

                if (posCol >= 2 && sheet[posRow, posCol - 2] == 0 && movetime == 0) //첫 이동은 2칸 이동 가능
                {
                    sheet[posRow, posCol - 2] = -1;
                    obj.transform.GetChild(posRow).GetChild(posCol - 2).GetComponent<Tile>().value = 1;
                }
            }
        }
    }

    public static void BishopRange(int posRow, int posCol) //bishop의 이동 범위 표시 함수
    {
        for(int a = 1; posRow + a < 6 && posCol + a < 6; a++)
        {
            if (sheet[posRow + a, posCol + a] == 0)
            {
                sheet[posRow + a, posCol + a] = -1;
                obj.transform.GetChild(posRow + a).GetChild(posCol + a).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
        for (int a = 1; posRow - a > -1 && posCol - a > -1; a++)
        {
            if (sheet[posRow - a, posCol - a] == 0)
            {
                sheet[posRow - a, posCol - a] = -1;
                obj.transform.GetChild(posRow - a).GetChild(posCol - a).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
        for (int a = 1; posRow + a < 6 && posCol - a > -1; a++)
        {
            if (sheet[posRow + a, posCol - a] == 0)
            {
                sheet[posRow + a, posCol - a] = -1;
                obj.transform.GetChild(posRow + a).GetChild(posCol - a).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
        for (int a = 1; posRow - a > -1 && posCol + a < 6; a++)
        {
            if (sheet[posRow - a, posCol + a] == 0)
            {
                sheet[posRow - a, posCol + a] = -1;
                obj.transform.GetChild(posRow - a).GetChild(posCol + a).GetComponent<Tile>().value = 1;
            }
            else
            {
                break;
            }
        }
    }
    public static void KnightRange(int posRow, int posCol) //knight의 이동 범위 표시 함수
    {
        if (posRow - 1 >= 0)
        {
            if (posCol + 2 <= 5 && sheet[posRow - 1, posCol + 2] == 0)
            {
                sheet[posRow - 1, posCol + 2] = -1;
                obj.transform.GetChild(posRow - 1).GetChild(posCol + 2).GetComponent<Tile>().value = 1;
            }
            if (posCol - 2 >= 0 && sheet[posRow - 1, posCol - 2] == 0)
            {
                sheet[posRow - 1, posCol - 2] = -1;
                obj.transform.GetChild(posRow - 1).GetChild(posCol - 2).GetComponent<Tile>().value = 1;
            }
            if (posRow - 2 >= 0)
            {
                if (posCol + 1 <= 5 && sheet[posRow - 2, posCol + 1] == 0)
                {
                    sheet[posRow - 2, posCol + 1] = -1;
                    obj.transform.GetChild(posRow - 2).GetChild(posCol + 1).GetComponent<Tile>().value = 1;
                }
                if (posCol - 1 >= 0 && sheet[posRow - 2, posCol - 1] == 0)
                {
                    sheet[posRow - 2 , posCol - 1] = -1;
                    obj.transform.GetChild(posRow - 2).GetChild(posCol - 1).GetComponent<Tile>().value = 1;
                }
            }
        }
        if (posRow + 1 <= 5)
        {
            if (posCol + 2 <= 5 && sheet[posRow + 1, posCol + 2] == 0)
            {
                sheet[posRow + 1, posCol + 2] = -1;
                obj.transform.GetChild(posRow + 1).GetChild(posCol + 2).GetComponent<Tile>().value = 1;
            }
            if (posCol - 2 >= 0 && sheet[posRow + 1, posCol - 2] == 0)
            {
                sheet[posRow + 1, posCol - 2] = -1;
                obj.transform.GetChild(posRow + 1).GetChild(posCol - 2).GetComponent<Tile>().value = 1;
            }
            if (posRow + 2 <= 5)
            {
                if (posCol + 1 <= 5 && sheet[posRow + 2, posCol + 1] == 0)
                {
                    sheet[posRow + 2, posCol + 1] = -1;
                    obj.transform.GetChild(posRow + 2).GetChild(posCol + 1).GetComponent<Tile>().value = 1;
                }
                if (posCol - 1 >= 0 && sheet[posRow + 2, posCol - 1] == 0)
                {
                    sheet[posRow + 2, posCol - 1] = -1;
                    obj.transform.GetChild(posRow + 2).GetChild(posCol - 1).GetComponent<Tile>().value = 1;
                }
            }
        }
    }

    public static void KingRange(int posRow,int posCol) //king의 이동 범위 표시 함수
    {
        for (int a = 1; a >= -1; a--)
        {
            for (int b = 1; b >= -1; b--)
            {
                if (a == 0 && b == 0) //자기 자신인 범위 제외
                {
                    continue;
                }
                if (posRow + a >= 0 && posRow + a <= 5 && posCol + b >= 0 && posCol + b <= 5)
                {
                    if (sheet[posRow + a, posCol + b] == 0)
                    {
                        sheet[posRow + a, posCol + b] = -1;
                        obj.transform.GetChild(posRow + a).GetChild(posCol + b).GetComponent<Tile>().value = 1;
                    } 
                }
            }
        }
    }
}
