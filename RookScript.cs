using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookScript : Piece
{
    public static int RookUpgrade = 0;
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
                BoardManagement.RookRange(posRow, posCol); //���õ� �⹰ ����
                BoardManagement.selectedRow = posRow;
                BoardManagement.selectedCol = posCol;
            }
        }
    }

    public void RookBuffSkill()
    {
        for (int i = posRow + 1; i < 6; i++)
        {
            TurnSystem.BuffSheet[i, posCol] *= 1.3f;
        }
        if(RookUpgrade >= 2)
        {
            for (int i = posRow - 1; i > -1; i--)
            {
                TurnSystem.BuffSheet[i, posCol] *= 1.3f;
            }
            for (int j = posCol + 1; j < 6; j++)
            {
                TurnSystem.BuffSheet[posRow, j] *= 1.3f;
            }
            for (int j = posCol - 1; j > -1; j--)
            {
                TurnSystem.BuffSheet[posRow, j] *= 1.3f;
            }
        }

    }
}
