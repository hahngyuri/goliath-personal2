using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : Piece
{
    public int movetime = 0;
    public static int PawnUpgrade;
    public bool PawnArmor = false;
    public static string direction = "forward";
    private void Awake()
    {
        movetime = 0;
        if (PawnUpgrade >= 2)
        {
            PawnArmor = true;
        }
    }

    private void OnMouseDown()
    {
        if (TurnSystem.moveTurn > 0 && !NowMove)
        {
            if (BoardManagement.selectedRow == posRow && BoardManagement.selectedCol == posCol) //���õ� �⹰�� �� �⹰�� ���� ��ǥ�϶�(���� �⹰�� ��)
            {
                BoardManagement.HideRange();
                BoardManagement.selectedRow = -1; //���õ� �⹰�� �ʱ�ȭ
                BoardManagement.selectedCol = -1;
            }
            else
            {
                BoardManagement.HideRange();
                BoardManagement.PawnRange(posRow, posCol, movetime, direction); //���õ� �⹰ ����
                BoardManagement.selectedRow = posRow;
                BoardManagement.selectedCol = posCol;
            }
        }
    }
}
