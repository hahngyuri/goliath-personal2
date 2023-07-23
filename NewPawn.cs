using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewPawn : ArrButtonScript
{
    private void Awake()
    {
        PieceDamage = 20;
        PieceCost = 1;
        colorblock = ArrButton.colors;
        BuyPieceNum = 6;
        NowPieceNum = 6;
        ButtonName = "pawn";
        
    }


    public void PieceSelected()
    {
        if (PieceType == "pawn")
        {
            PieceType = null;
            PieceDefault();
            PieceInfo.GetComponent<PieceInfoScript>().CloseInfo();
        }
        else if (NowPieceNum >= 1)
        {
            PieceType = "pawn";
            ArrImage.sprite = SelectedSprite;
            PieceInfo.GetComponent<PieceInfoScript>().PawnInfo();
            ArrButtonScript[] AllPieceButton = transform.parent.GetComponentsInChildren<ArrButtonScript>();
            foreach (ArrButtonScript OtherPieceButton in AllPieceButton)
            {
                if (OtherPieceButton.ButtonName != "pawn" && OtherPieceButton.NowPieceNum != 0)
                {
                    OtherPieceButton.PieceDefault();
                }
                else
                {
                    continue;
                }
                
            }
        }
    }


}
