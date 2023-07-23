using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class PawnUpgradeManagement : PieceUpgradeManagement
{
    NewPawn NewPawnScript;
    PieceInfoScript ReInfo;

    private void Awake()
    {
        NewPawnScript = GameObject.Find("pawn_button").GetComponent<NewPawn>();
        ReInfo = GameObject.Find("PieceInfo").GetComponent<PieceInfoScript>();
        UISetting();
        UISetting();
    }
    public void PawnUpgradeLv1()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        PawnScript.PawnUpgrade = 1;
        NewPawnScript.PieceDamage = 25;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Pawn");
        foreach(GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<PawnScript>().damage = 25;
        }
        UP1Image.sprite = UpgradeComplete[0];
        UP2Image.sprite = UpgradeDefault[1];
        UP2.SetActive(true);

        ReInfo.PawnUpgradeRefresh();

    }

    public void PawnUpgradeLv2()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        PawnScript.PawnUpgrade = 2;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Pawn");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<PawnScript>().PawnArmor = true;
        }
        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);

        ReInfo.PawnUpgradeRefresh();
    }

    public void PawnUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "구매 완료";
        PawnScript.PawnUpgrade = 3;
        NewPawnScript.PieceDamage = 35;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Pawn");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<PawnScript>().damage = 35;
        }
        UP3Image.sprite = UpgradeComplete[2];
        ReInfo.PawnUpgradeRefresh();
    }

    public void UpCode1()
    {
        UpgradeBuy.UpgradeCode = 1;
        UpgradeIcon.sprite = UpgradeDefault[0];
        if (PawnScript.PawnUpgrade >= 1)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "200G";
        }
        UpgradeName.text = "전투 훈련";
        UpgradeInfo.text = "일반병의 공격력이 강해집니다. 좀 더 강하게 골리앗을 공격할 수 있겠네요.\n\n\n\n\n\nATK 20  ▶  <color=green>ATK 25 (▲5)</color>";
    }

    public void UpCode2()
    {
        UpgradeBuy.UpgradeCode = 2;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (PawnScript.PawnUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "200G";
        }
        UpgradeName.text = "중장갑 착용";
        UpgradeInfo.text = "일반병의 방어력이 강해집니다. 이제 골리앗의 공격을 \r\n한 번 막을 수 있습니다. 엄청 대단한 거라고요.\r\n\r\n\r\n<color=green>New Skill\r\n골리앗의 공격을 1회 방어</color>";
    }

    public void UpCode3()
    {
        UpgradeBuy.UpgradeCode = 3;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (PawnScript.PawnUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "구매 완료";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "200G";
        }
        UpgradeName.text = "혹독한 전투 훈련";
        UpgradeInfo.text = "일반병의 공격력이 더 강해집니다. 무시했다간 큰코다칠지도 몰라요.\n\n\n\n\n\nATK 25  ▶  <color=green>ATK 35 (▲10)</color>";
    }
}
