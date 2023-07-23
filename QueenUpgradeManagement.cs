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
        Price.text = "���� �Ϸ�";
        QueenScript.QueenUpgrade = 1;

        UP1Image.sprite = UpgradeComplete[0];
        UP2Image.sprite = UpgradeDefault[1];
        UP2.SetActive(true);
    }

    public void QueenUpgradeLv2()
    {
        BuyButton.interactable = false;
        Price.text = "���� �Ϸ�";
        QueenScript.QueenUpgrade = 2;

        UP2Image.sprite = UpgradeComplete[1];
        UP3Image.sprite = UpgradeDefault[2];
        UP3.SetActive(true);
    }

    public void QueenUpgradeLv3()
    {
        BuyButton.interactable = false;
        Price.text = "���� �Ϸ�";
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
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "300G";
        }
        UpgradeName.text = "���� ���� ����";
        UpgradeInfo.text = "���°��� ���ݷ��� �������ϴ�. �� ������ �Ǿ�� ���¸� �� �� ����.\n\n\n\n\n\nATK 20  ��  <color=green>ATK 30 (��10)</color>";
    }

    public void UpCode14()
    {
        UpgradeBuy.UpgradeCode = 14;
        UpgradeIcon.sprite = UpgradeDefault[1];
        if (QueenScript.QueenUpgrade >= 2)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "300G";
        }
        UpgradeName.text = "���� �м�";
        UpgradeInfo.text = "���°��� ���ݷ��� ���� �������ϴ�. ������ ���� ���̱���!\n\n\n\n\n\nATK 30  ��  <color=green>ATK 45 (��15)</color>";
    }

    public void UpCode15()
    {
        UpgradeBuy.UpgradeCode = 15;
        UpgradeIcon.sprite = UpgradeDefault[2];
        if (QueenScript.QueenUpgrade >= 3)
        {
            BuyButton.interactable = false;
            Price.text = "���� �Ϸ�";
        }
        else
        {
            BuyButton.interactable = true;
            Price.text = "300G";
        }
        UpgradeName.text = "�������� �������";
        UpgradeInfo.text = "���°��� ���ݷ��� ������ �������ϴ�. \r\n���� ���� �־��� �ǰ���?! \r\n��� �ְ��� ���°��̳׿�\r\n\r\n\r\n\r\n\r\nATK 45  ��  <color=green>ATK 65 (��20)</color>";
    }
}
