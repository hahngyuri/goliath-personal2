using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
���׷��̵��� �������� damage�� �����ϱ� ������ BoardManagement�� ������ ���� �ʴ´�. (Refresh�� ������ �����Ƿ�)
������ 0~2�ุ ������ ��꿡 ���Ƿ� ������ ����. ������ 3~5�࿡ �ִ��� �������� �ִ� �⹰��
���´ٸ� �ڵ带 �����ؾ��Ѵ�.
 */
public class PieceUpgradeManagement : MonoBehaviour
{
    public GameObject UP1;
    public GameObject UP2;
    public GameObject UP3;
    public Image UP1Image;
    public Image UP2Image;
    public Image UP3Image;
    public Sprite[] UpgradeDefault = new Sprite[3];
    public Sprite[] UpgradeComplete = new Sprite[3];

    public Image UpgradeIcon;
    public Text UpgradeName;
    public Text UpgradeInfo;
    public Button BuyButton;
    public Text Price;

    public void UISetting()
    {
        UP1 = transform.GetChild(0).gameObject;
        UP2 = transform.GetChild(1).gameObject;
        UP3 = transform.GetChild(2).gameObject;
        UP1Image = UP1.GetComponent<Image>();
        UP2Image = UP2.GetComponent<Image>();
        UP3Image = UP3.GetComponent<Image>();
        UP1Image.sprite = UpgradeDefault[0];
        UP1.SetActive(true);
        UP2.SetActive(false);
        UP3.SetActive(false);
    }
}
