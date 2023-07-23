using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : Piece
{
    public static int KnightUpgrade;
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
                BoardManagement.KnightRange(posRow, posCol); //���õ� �⹰ ����
                BoardManagement.selectedRow = posRow;
                BoardManagement.selectedCol = posCol;
            }
        }
    }
}
