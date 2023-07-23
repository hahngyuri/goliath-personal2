using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class RookUpgradeManagement : PieceUpgradeManagement
{
    NewRook NewRookScript;
    PieceInfoScript ReInfo;

    private void Awake()
    {
        NewRookScript = GameObject.Find("rook_button").GetComponent<NewRook>();
        ReInfo = GameObject.Find("PieceInfo").GetComponent<PieceInfoScript>();
        UISetting();
    }
    public void RookUpgradeLv1()
    {
        BuyButton.interactable = false;
        Price.text = "���� �Ϸ�";
        RookScript.RookUpgrade = 1;
        NewRookScript.PieceDamage = 50;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Rook");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<RookScript>().damage = 50;
        }

        UP1Image.sprite = UpgradeComplete[0];
        UP2Image.sprite = UpgradeDefault[1];
        UP2.SetActive(true);

        ReInfo.RookUpgradeRefresh();

    }

    public void RookUpgradeLv2()
    {
        BuyButton.interactable = false;
        Price.text = "���� �Ϸ�";
        RookScript.RookUpgrade = 2;

        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);

        ReInfo.RookUpgradeRefresh();
    }

    public void RookUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "���� �Ϸ�";
        RookScript.RookUpgrade = 3;
        NewRookScript.PieceDamage = 60;

        GameObject[] OldPieces = GameObject.FindGameObjectsWithTag("Rook");
        foreach (GameObject OldPiece in OldPieces)
        {
            OldPiece.GetComponent<RookScript>().damage = 60;
        }

        UP3Image.sprite = UpgradeComplete[2];

        ReInfo.RookUpgradeRefresh();
    }

    public void UpCode10()
    {
        UpgradeBuy.UpgradeCode = 10;
        UpgradeIcon.sprite = UpgradeDefault[0];
        if (RookScript.RookUpgrade >= 1)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "400G";
        }
        UpgradeName.text = "���� ��ö";
        UpgradeInfo.text = "��ȣ���� ���ݷ��� �������ϴ�. ���� ������ ���� �ʰڴٳ׿�.\n\n\n\n\n\n\nATK 45  ��  <color=green>ATK 50 (��5)</color>";
    }

    public void UpCode11()
    {
        UpgradeBuy.UpgradeCode = 11;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (RookScript.RookUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "400G";
        }
        UpgradeName.text = "��ȣ�� �Լ�";
        UpgradeInfo.text = "��ȣ���� ���ݷ� ���� ������ �þ�ϴ�. \r\n���� ��������������.\r\n\r\n\r\n\r\n<color=green>Skill Upgrade\r\n���ݷ� ������ �� ���⿡ ����˴ϴ�.</color>";
    }

    public void UpCode12()
    {
        UpgradeBuy.UpgradeCode = 12;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (RookScript.RookUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "400G";
        }
        UpgradeName.text = "���ǵ� ��ȣ��";
        UpgradeInfo.text = "��ȣ���� ���ݷ��� ���� �������ϴ�. �� ������ ���� �ʰھ��.\n\n\n\n\n\nATK 50  ��  <color=green>ATK 60 (��10)</color>";
    }
}
