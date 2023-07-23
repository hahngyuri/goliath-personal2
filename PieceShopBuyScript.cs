using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceShopBuyScript : MonoBehaviour
{
    private GameObject SelectedButton;
    public GameObject PawnButton;
    public GameObject BishopButton;
    public GameObject KnightButton;
    public GameObject RookButton;

    public Text MyPiece;

    public void PieceShopBuy()
    {
        switch (PieceShopMovePage.PieceShopPageNum)
        {
            case 1:
                if (PieceShopMoney.Money >= 20)
                {
                    PieceShopMoney.Money -= 20;
                    SelectedButton = PawnButton;
                    SelectedButton.GetComponent<ArrButtonScript>().BuyPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().PieceDefault();
                    SelectedButton.transform.GetChild(0).GetComponent<Text>().text = $"X{SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    MyPiece.text = $"보유 수 : {SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    PlayerPrefs.SetInt("PawnNumber") = SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum;
                    PlayerPrefs.Save();
                }
                
                break;

            case 2:
                if (PieceShopMoney.Money >= 30)
                {
                    PieceShopMoney.Money -= 30;
                    SelectedButton = BishopButton;
                    SelectedButton.GetComponent<ArrButtonScript>().BuyPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().PieceDefault();
                    SelectedButton.transform.GetChild(0).GetComponent<Text>().text = $"X{SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    MyPiece.text = $"보유 수 : {SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    PlayerPrefs.SetInt("BishopNumber") = SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum;
                    PlayerPrefs.Save();
                }
                break;

            case 3:
                if (PieceShopMoney.Money >= 30)
                {
                    PieceShopMoney.Money -= 30;
                    SelectedButton = KnightButton;
                    SelectedButton.GetComponent<ArrButtonScript>().BuyPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().PieceDefault();
                    SelectedButton.transform.GetChild(0).GetComponent<Text>().text = $"X{SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    MyPiece.text = $"보유 수 : {SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    PlayerPrefs.SetInt("KnightNumber") = SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum;
                    PlayerPrefs.Save();
                }
                break;

            case 4:
                if (PieceShopMoney.Money >= 35)
                {
                    PieceShopMoney.Money -= 35;
                    SelectedButton = RookButton;
                    SelectedButton.GetComponent<ArrButtonScript>().BuyPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum++;
                    SelectedButton.GetComponent<ArrButtonScript>().PieceDefault();
                    SelectedButton.transform.GetChild(0).GetComponent<Text>().text = $"X{SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    MyPiece.text = $"보유 수 : {SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum}";
                    PlayerPrefs.SetInt("RookNumber") = SelectedButton.GetComponent<ArrButtonScript>().NowPieceNum;
                    PlayerPrefs.Save();
                }
                break;
        }
    }
    
}
