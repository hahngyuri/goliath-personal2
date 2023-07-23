using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenUpgradeManagement : PieceUpgradeManagement
{
    private void Awake()
    {
        UISetting();
    }

    public void QueenUpgradeLv1()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        QueenScript.QueenUpgrade = 1;

        UP1Image.sprite = UpgradeComplete[0];
        UP2Image.sprite = UpgradeDefault[1];
        UP2.SetActive(true);
    }

    public void QueenUpgradeLv2()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        QueenScript.QueenUpgrade = 2;

        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);
    }

    public void QueenUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        QueenScript.QueenUpgrade = 3;

        UP3Image.sprite = UpgradeComplete[2];
    }

    public void UpCode13()
    {
        UpgradeBuy.UpgradeCode = 13;
        UpgradeIcon.sprite = UpgradeDefault[0];
        if (QueenScript.QueenUpgrade >= 1)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "300G";
        }
        UpgradeName.text = "전장 정보 수집";
        UpgradeInfo.text = "보좌관의 공격력이 강해집니다. 이 정도는 되어야 보좌를 할 수 있죠.\n\n\n\n\n\nATK 20  ▶  <color=green>ATK 30 (▲10)</color>";
    }

    public void UpCode14()
    {
        UpgradeBuy.UpgradeCode = 14;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (QueenScript.QueenUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "300G";
        }
        UpgradeName.text = "전장 분석";
        UpgradeInfo.text = "보좌관의 공격력이 더욱 강해집니다. 열심히 수련 중이군요!\n\n\n\n\n\nATK 30  ▶  <color=green>ATK 45 (▲15)</color>";
    }

    public void UpCode15()
    {
        UpgradeBuy.UpgradeCode = 15;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (QueenScript.QueenUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "300G";
        }
        UpgradeName.text = "지피지기 백전백승";
        UpgradeInfo.text = "보좌관의 공격력이 더더욱 강해집니다. \r\n무슨 일이 있었던 건가요?! \r\n사상 최강의 보좌관이네요\r\n\r\n\r\n\r\n\r\nATK 45  ▶  <color=green>ATK 65 (▲20)</color>";
    }
}
