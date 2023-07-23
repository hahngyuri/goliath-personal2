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
            if (BoardManagement.selectedRow == posRow && BoardManagement.selectedCol == posCol) //선택된 기물과 이 기물이 같은 좌표일때(같은 기물일 때)
            {
                BoardManagement.HideRange();
                BoardManagement.selectedRow = -1; //선택된 기물을 초기화
                BoardManagement.selectedCol = -1;
            }
            else
            {
                BoardManagement.HideRange();
                BoardManagement.PawnRange(posRow, posCol, movetime, direction); //선택된 기물 변경
                BoardManagement.selectedRow = posRow;
                BoardManagement.selectedCol = posCol;
            }
        }
    }
}
