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
        Price.text = "���� �Ϸ�";
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
        Price.text = "���� �Ϸ�";
        KnightScript.KnightUpgrade = 2;

        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);
        ReInfo.KnightUpgradeRefresh();
    }

    public void KnightUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "���� �Ϸ�";
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
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "100G";
        }
        UpgradeName.text = "���˼� ����";
        UpgradeInfo.text = "���˻��� ���ݷ��� �������ϴ�. �� ������ �Ǿ�� ���˻��� �θ� �� ����.\n\n\n\n\n\nATK 40  ��  <color=green>ATK 45 (��5)</color>";
    }

    public void UpCode8()
    {
        UpgradeBuy.UpgradeCode = 8;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (KnightScript.KnightUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "100G";
        }
        UpgradeName.text = "��ũ";
        UpgradeInfo.text = "�̵� ���� ȿ���� ���� �ʽ��ϴ�. ���˻�� ����� �߰����� ������ �ְŵ��.\r\n\r\n\r\n\r\n<color=green>New Skill\r\n�̵� ���� ȿ�� ����</color>";
    }

    public void UpCode9()
    {
        UpgradeBuy.UpgradeCode = 9;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (KnightScript.KnightUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "100G";
        }
        UpgradeName.text = "���˼� ����";
        UpgradeInfo.text = "���˻��� ���ݷ��� ���� �������ϴ�. �߽��� ���� �̹� ���� �����ٰ��.\n\n\n\n\n\nATK 45  ��  <color=green>ATK 55 (��10)</color>";
    }
}
