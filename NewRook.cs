using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRook : ArrButtonScript
{
    private void Awake()
    {
        //PieceDamage = 45;
        PieceCost = 3;
        colorblock = ArrButton.colors;
        BuyPieceNum = 2;
        NowPieceNum = 2;
        ButtonName = "rook";
        if (PlayerPrefs.HasKey("RookNumber"))
        {
            BuyPieceNum = NowPieceNum = PlayerPrefs.GetInt("RookNumber");
        }
    }


    public void PieceSelected()
    {
        if (PieceType == "rook")
        {
            PieceType = null;
            PieceDefault();
            PieceInfo.GetComponent<PieceInfoScript>().CloseInfo();
        }
        else if (NowPieceNum >= 1)
        {
            PieceType = "rook";
            ArrImage.sprite = SelectedSprite;
            PieceInfo.GetComponent<PieceInfoScript>().RookInfo();
            ArrButtonScript[] AllPieceButton = transform.parent.GetComponentsInChildren<ArrButtonScript>();
            foreach (ArrButtonScript OtherPieceButton in AllPieceButton)
            {
                if (OtherPieceButton.ButtonName != "rook" && OtherPieceButton.NowPieceNum != 0)
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
