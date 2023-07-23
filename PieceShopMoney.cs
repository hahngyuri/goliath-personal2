using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceShopMoney : MonoBehaviour
{
    public Text MoneyText;
    public static int Money = 100000;

    private void LateUpdate()
    {
        if (Money < 1000)
        {
            MoneyText.text = Money.ToString();
            //
        }
        else if (Money >= 1000)
        {
            MoneyText.text = string.Format("{0:#,0}", Money);
        }
    }
}
