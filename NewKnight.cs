using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewKnight : ArrButtonScript
{
    private void Awake()
    {
        PieceDamage = 40;
        PieceCost = 3;
        colorblock = ArrButton.colors;
        BuyPieceNum = 1;
        NowPieceNum = 1;
        ButtonName = "knight";
    }


    public void PieceSelected()
    {
        if (PieceType == "knight")
        {
            PieceType = null;
            PieceDefault();
            PieceInfo.GetComponent<PieceInfoScript>().CloseInfo();
        }
        else if (NowPieceNum >= 1)
        {
            PieceType = "knight";
            ArrImage.sprite = SelectedSprite;
            PieceInfo.GetComponent<PieceInfoScript>().KnightInfo();
            ArrButtonScript[] AllPieceButton = transform.parent.GetComponentsInChildren<ArrButtonScript>();
            foreach (ArrButtonScript OtherPieceButton in AllPieceButton)
            {
                if (OtherPieceButton.ButtonName != "knight" && OtherPieceButton.NowPieceNum != 0)
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
