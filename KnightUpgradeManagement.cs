using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUpgradeManagement : PieceUpgradeManagement
{
    NewKnight NewKnightScript;
    PieceInfoScript ReInfo;

    private void Awake()
    {
        NewKnightScript = GameObject.Find("knight_button").GetComponent<NewKnight>();
        ReInfo = GameObject.Find("PieceInfo").GetComponent<PieceInfoScript>();


        UISetting();
    }
    public void KnightUpgradeLv1()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        KnightScript.KnightUpgrade = 1;
        NewKnightScript.PieceDamage = 45;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Knight");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<KnightScript>().damage = 45;
        }

        UP1Image.sprite = UpgradeComplete[0];
        UP2Image.sprite = UpgradeDefault[1];
        UP2.SetActive(true);
        ReInfo.KnightUpgradeRefresh();
    }

    public void KnightUpgradeLv2()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        KnightScript.KnightUpgrade = 2;

        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);
        ReInfo.KnightUpgradeRefresh();
    }

    public void KnightUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        KnightScript.KnightUpgrade = 3;
        NewKnightScript.PieceDamage = 55;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Knight");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<KnightScript>().damage = 55;
        }

        UP3Image.sprite = UpgradeComplete[2];
        ReInfo.KnightUpgradeRefresh();
    }

    public void UpCode7()
    {
        UpgradeBuy.UpgradeCode = 7;
        UpgradeIcon.sprite = UpgradeDefault[0];
        if (KnightScript.KnightUpgrade >= 1)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "100G";
        }
        UpgradeName.text = "마검술 수련";
        UpgradeInfo.text = "마검사의 공격력이 강해집니다. 이 정도는 되어야 마검사라고 부를 수 있죠.\n\n\n\n\n\nATK 40  ▶  <color=green>ATK 45 (▲5)</color>";
    }

    public void UpCode8()
    {
        UpgradeBuy.UpgradeCode = 8;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (KnightScript.KnightUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "100G";
        }
        UpgradeName.text = "블링크";
        UpgradeInfo.text = "이동 방해 효과를 받지 않습니다. 마검사는 재빠른 발걸음을 가지고 있거든요.\r\n\r\n\r\n\r\n<color=green>New Skill\r\n이동 방해 효과 무시</color>";
    }

    public void UpCode9()
    {
        UpgradeBuy.UpgradeCode = 9;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (KnightScript.KnightUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "100G";
        }
        UpgradeName.text = "마검술 숙련";
        UpgradeInfo.text = "마검사의 공격력이 더욱 강해집니다. 견습일 때는 이미 한참 지났다고요.\n\n\n\n\n\nATK 45  ▶  <color=green>ATK 55 (▲10)</color>";
    }
}
