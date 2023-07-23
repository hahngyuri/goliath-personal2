using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.UI;

/*
sheet�� ����

2���� �迭�� sheet�� ����.

��� : �⹰�� ������, �⹰�� �����ϴ��� üũ
0 : �����
-1 : �̵� ���� ���� 
-2 ������ ���� : Ÿ�Ͽ� ������ �ִ� ���� ���


������Ʈ�� ����

BoardManage {BoardManagement.cs} (���� ��ü�� �Ѱ�, �̵� ���)
    ��� : 0~5 (���� ���� ����, ��ü�� �������� ����)
        ��� : 0~5 {Tile.cs} (���� ���� ����, ü�� �� �ϳ��ϳ��� ���, �̵� ���� ǥ��)
            ��� : rook,pawn etc. {piece.cs} (�⹰, �⹰�� �̵� ������ Ÿ�Ͽ� ��� ��Ű�� ��)
 */

public class BoardManagement : MonoBehaviour
{ 
    public static int[,] sheet = new int[6, 6]; // 2���� �迭 �߰� (������ ü�����̶�� ����)
    public static int selectedRow = -1; // ���õ� �⹰�� ��ǥ
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

    public static void HideRange() //�̵� ���� ���� ����� �Լ�
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

    public static void Movement(int posRow, int posCol) //�̵� �Լ� (������ ��, ������ ��)
    {
        HideRange();
        GameObject MovePiece = obj.transform.GetChild(selectedRow).GetChild(selectedCol).GetChild(0).gameObject;
        MovePiece.transform.parent = obj.transform.GetChild(posRow).GetChild(posCol); //�⹰�� ������ Ÿ�Ͽ� ���


        SliderScript.isCounting = false;
        int RowDiff = selectedRow - posRow; // ���� : ��� / ���� : ���� 
        int ColDiff = selectedCol - posCol; // ���� : ��� / ������ : ���� 

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

        sheet[selectedRow, selectedCol] = 0; //���� ü���ǿ��� �̵� ó��
        //MovePiece.GetComponent<Piece>().RefreshPosition(); //��ġ ���ΰ�ħ �Լ� ȣ��

        RemainMove.GetComponent<Text>().text = TurnSystem.moveTurn.ToString();
        
        for(int i = 0; i < 6; i++)
        {
            Debug.Log($"{sheet[i, 0]}   {sheet[i, 1]}   {sheet[i, 2]}   {sheet[i, 3]}   {sheet[i, 4]}   {sheet[i, 5]}");
        }
    }

    public static void RookRange(int posRow,int posCol) //rook�� �̵� ���� ǥ�� �Լ�
    {
        for (int a = 1; a + posRow < 6; a++)
        {
            if (sheet[a + posRow, posCol] == 0) // �� �������� Ȯ��
            {
                sheet[a + posRow, posCol] = -1; //���� ü���ǿ��� ���� ��ȭ ó��
                obj.transform.GetChild(a + posRow).GetChild(posCol).GetComponent<Tile>().value = 1; //Tile �κ��� value ȣ�� (�̵� ���� ���� ǥ��)
            }
            else //�⹰�� ���� ��� �� �̻� �������� ����
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

    public static void PawnRange(int posRow,int posCol,int movetime, string ahead) //pawn�� �̵� ���� ǥ�� �Լ�
    {
        if (ahead == "forward") //���� ���� : ������
        {
            if (posRow >= 1 && sheet[posRow - 1, posCol] == 0)
            {
                sheet[posRow - 1, posCol] = -1;
                obj.transform.GetChild(posRow - 1).GetChild(posCol).GetComponent<Tile>().value = 1;

                if (posRow >= 2 && sheet[posRow - 2, posCol] == 0 && movetime == 0) //ù �̵��� 2ĭ �̵� ����
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

                if (posRow <= 3 && sheet[posRow + 2, posCol] == 0 && movetime == 0) //ù �̵��� 2ĭ �̵� ����
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

                if (posCol <= 3 && sheet[posRow, posCol + 2] == 0 && movetime == 0) //ù �̵��� 2ĭ �̵� ����
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

                if (posCol >= 2 && sheet[posRow, posCol - 2] == 0 && movetime == 0) //ù �̵��� 2ĭ �̵� ����
                {
                    sheet[posRow, posCol - 2] = -1;
                    obj.transform.GetChild(posRow).GetChild(posCol - 2).GetComponent<Tile>().value = 1;
                }
            }
        }
    }

    public static void BishopRange(int posRow, int posCol) //bishop�� �̵� ���� ǥ�� �Լ�
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
    public static void KnightRange(int posRow, int posCol) //knight�� �̵� ���� ǥ�� �Լ�
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

    public static void KingRange(int posRow,int posCol) //king�� �̵� ���� ǥ�� �Լ�
    {
        for (int a = 1; a >= -1; a--)
        {
            for (int b = 1; b >= -1; b--)
            {
                if (a == 0 && b == 0) //�ڱ� �ڽ��� ���� ����
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
