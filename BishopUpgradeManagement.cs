using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopUpgradeManagement : PieceUpgradeManagement 
{
    NewBishop NewBishopScript;
    PieceInfoScript ReInfo;

    private void Awake()
    {
        NewBishopScript = GameObject.Find("bishop_button").GetComponent<NewBishop>();
        ReInfo = GameObject.Find("PieceInfo").GetComponent<PieceInfoScript>();
        UISetting();
    }
    public void BishopUpgradeLv1()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        BishopScript.BishopUpgrade = 1;
        NewBishopScript.PieceDamage = 35;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Bishop");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<BishopScript>().damage = 35;
        }

        UP1Image.sprite = UpgradeComplete[0];
        UP2Image.sprite = UpgradeDefault[1];
        UP2.SetActive(true);
        ReInfo.BishopUpgradeRefresh();

    }

    public void BishopUpgradeLv2()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        BishopScript.BishopUpgrade = 2;

        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);
        ReInfo.BishopUpgradeRefresh();
    }

    public void BishopUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        BishopScript.BishopUpgrade = 3;
        NewBishopScript.PieceDamage = 45;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Bishop");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<BishopScript>().damage = 45;
        }

        UP3Image.sprite = UpgradeComplete[2];
        ReInfo.BishopUpgradeRefresh();
    }

    public void UpCode4()
    {
        UpgradeBuy.UpgradeCode = 4;
        UpgradeIcon.sprite = UpgradeDefault[0];
        if (BishopScript.BishopUpgrade >= 1)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "150G";
        }
        UpgradeName.text = "하프(물리)";
        UpgradeInfo.text = "음유시인의 공격력이 강해집니다. 버퍼에게도 공격력은 중요하죠.\n\n\n\n\n\nATK 30  ▶  <color=green>ATK 35 (▲5)</color>";
    }

    public void UpCode5()
    {
        UpgradeBuy.UpgradeCode = 5;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (BishopScript.BishopUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "150G";
        }
        UpgradeName.text = "천상의 연주";
        UpgradeInfo.text = "음유시인의 공격력 증가 효과가 강화됩니다. 새로운 스킬을 익히고 왔나봐요.\r\n\r\n\r\n\r\n<color=green>Skill Upgrade\r\n음유시인의 버프가 뒤쪽에도 적용됩니다.\r\n공격력 증가 50% (20%▲)</color>";
    }

    public void UpCode6()
    {
        UpgradeBuy.UpgradeCode = 6;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (BishopScript.BishopUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "150G";
        }
        UpgradeName.text = "더 단단한 하프";
        UpgradeInfo.text = "음유시인의 공격력이 더욱 강해집니다. 골리앗도 무시할 순 없을 거에요.\n\n\n\n\n\nATK 35  ▶  <color=green>ATK 45 (▲10)</color>";
    }
}
