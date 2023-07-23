using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
업그레이드의 적용방식이 damage를 수정하기 때문에 BoardManagement에 적용이 되지 않는다. (Refresh를 해주지 않으므로)
하지만 0~2행만 데미지 계산에 들어가므로 문제는 없다. 언젠가 3~5행에 있더라도 데미지를 주는 기물이
나온다면 코드를 수정해야한다.
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
