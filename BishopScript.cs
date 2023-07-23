using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BishopScript : Piece
{
    public static int BishopUpgrade = 0;
    private void OnMouseDown()
    {
        if (TurnSystem.moveTurn > 0 && !NowMove)
        {
            if (BoardManagement.selectedRow == posRow && BoardManagement.selectedCol == posCol) //선택된 기물과 이 기물이 같은 좌표일때(같은 기물일 때)
            {
                BoardManagement.HideRange();
                BoardManagement.selectedRow = -1; //선택된 기물을 초기화
                BoardManagement.selectedCol = -1;
            }
            else
            {
                BoardManagement.HideRange();
                BoardManagement.BishopRange(posRow, posCol); //선택된 기물 변경
                BoardManagement.selectedRow = posRow;
                BoardManagement.selectedCol = posCol;
            }
        }
    }
    public void BishopBuffSkill()
    {
        float rate = 1.3f;
        if (BishopUpgrade >= 2)
        {
            rate = 1.5f;
            for (int i = 1; posRow + i < 6 && posCol + i < 6; i++)
            {
                TurnSystem.BuffSheet[posRow + i, posCol + i] *= rate;
            }
            for (int j = 1; posRow + j < 6 && posCol - j > -1; j++)
            {
                TurnSystem.BuffSheet[posRow + j, posCol - j] *= rate;
            }
        }
        for (int i = 1; posRow - i > -1 && posCol - i > -1; i++)
        {
            TurnSystem.BuffSheet[posRow - i, posCol - i] *= rate;
        }
        for (int j = 1; posRow - j > -1 && posCol + j < 6; j++)
        {
            TurnSystem.BuffSheet[posRow - j, posCol + j] *= rate;
        }
        
    }
}
