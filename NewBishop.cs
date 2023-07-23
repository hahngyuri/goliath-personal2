using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBishop : ArrButtonScript
{
    private void Awake()
    {
        //PieceDamage = 30;
        PieceCost = 2;
        colorblock = ArrButton.colors;
        BuyPieceNum = 1;
        NowPieceNum = 1;
        ButtonName = "bishop";
        if (PlayerPrefs.HasKey("BishopNumber"))
        {
            BuyPieceNum = NowPieceNum = PlayerPrefs.GetInt("BishopNumber");
        }
    }


    public void PieceSelected()
    {
        if (PieceType == "bishop")
        {
            PieceType = null;
            PieceDefault();
            PieceInfo.GetComponent<PieceInfoScript>().CloseInfo();
        }
        else if (NowPieceNum >= 1)
        {
            PieceType = "bishop";
            ArrImage.sprite = SelectedSprite;
            PieceInfo.GetComponent<PieceInfoScript>().BishopInfo();
            ArrButtonScript[] AllPieceButton = transform.parent.GetComponentsInChildren<ArrButtonScript>();
            foreach (ArrButtonScript OtherPieceButton in AllPieceButton)
            {
                if (OtherPieceButton.ButtonName != "bishop" && OtherPieceButton.NowPieceNum != 0)
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