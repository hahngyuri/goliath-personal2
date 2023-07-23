using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceInfoScript : MonoBehaviour
{
    public Image PiecePortrait;
    public Text BasicInfo;
    public Image MoveInfo;
    public Text SubInfo;

    public Sprite PawnPortrait;
    public Sprite BishopPortrait;
    public Sprite KnightPortrait;
    public Sprite RookPortrait;

    public Sprite PawnMove;
    public Sprite BishopMove;
    public Sprite KnightMove;
    public Sprite RookMove;

    public void PawnInfo()
    {
        gameObject.SetActive(true);
        PiecePortrait.sprite = PawnPortrait;
        BasicInfo.text = "Class\n일반병";
        MoveInfo.sprite = PawnMove;
        PawnUpgradeRefresh();
    }

    public void PawnUpgradeRefresh()
    {
        switch (PawnScript.PawnUpgrade)
        {
            case 0:
                SubInfo.text = "ATK : 20\nCost : 1\nSkill\n없음";
                break;
            case 1:
                SubInfo.text = "ATK : 20<color=orange>(+5)</color>\nCost : 1\nSkill\n없음";
                break;
            case 2:
                SubInfo.text = "ATK : 20<color=orange>(+5)</color>\nCost : 1\nSkill\n<color=orange>골리앗의 공격을 1회 방어</color>";
                break;
            case 3:
                SubInfo.text = "ATK : 20<color=orange>(+15)</color>\nCost : 1\nSkill\n<color=orange>골리앗의 공격을 1회 방어</color>";
                break;
        }
    }

    public void BishopInfo()
    {
        gameObject.SetActive(true);
        PiecePortrait.sprite = BishopPortrait;
        BasicInfo.text = "Class\n음유시인";
        MoveInfo.sprite = BishopMove;
        BishopUpgradeRefresh();
    }

    public void BishopUpgradeRefresh()
    {
        switch (BishopScript.BishopUpgrade)
        {
            case 0:
                SubInfo.text = "ATK : 30\nCost : 2\nSkill\n자신의 앞쪽 대각선 아군 유닛 공격력 증가";
                break;
            case 1:
                SubInfo.text = "ATK : 30<color=orange>(+5)</color>\nCost : 2\nSkill\n자신의 앞쪽 대각선 아군 유닛 공격력 증가";
                break;
            case 2:
                SubInfo.text = "ATK : 30<color=orange>(+5)</color>\nCost : 2\nSkill\n<color=orange>자신의 이동 경로상의 아군 유닛 공격력 증가</color>";
                break;
            case 3:
                SubInfo.text = "ATK : 30<color=orange>(+15)</color>\nCost : 2\nSkill\n<color=orange>자신의 이동 경로상의 아군 유닛 공격력 증가</color>";
                break;
        }
    }

    public void KnightInfo()
    {
        gameObject.SetActive(true);
        PiecePortrait.sprite = KnightPortrait;
        BasicInfo.text = "Class\n마검사";
        MoveInfo.sprite = KnightMove;
        KnightUpgradeRefresh();
    }

    public void KnightUpgradeRefresh()
    {
        switch (KnightScript.KnightUpgrade)
        {
            case 0:
                SubInfo.text = "ATK : 40\nCost : 3\nSkill\n없음";
                break;
            case 1:
                SubInfo.text = "ATK : 40<color=orange>(+5)</color>\nCost : 3\nSkill\n없음";
                break;
            case 2:
                SubInfo.text = "ATK : 40<color=orange>(+5)</color>\nCost : 3\nSkill\n<color=orange>이동 방해 효과 무시</color>";
                break;
            case 3:
                SubInfo.text = "ATK : 40<color=orange>(+15)</color>\nCost : 3\nSkill\n<color=orange>이동 방해 효과 무시</color>";
                break;
        }
    }

    public void RookInfo()
    {
        gameObject.SetActive(true);
        PiecePortrait.sprite = RookPortrait;
        BasicInfo.text = "Class\n수호자";
        MoveInfo.sprite = RookMove;
        RookUpgradeRefresh();
    }

    public void RookUpgradeRefresh()
    {
        switch (RookScript.RookUpgrade)
        {
            case 0:
                SubInfo.text = "ATK : 40\nCost : 3\nSkill\n자신의 뒤쪽 아군 유닛의 공격력 증가";
                break;
            case 1:
                SubInfo.text = "ATK : 40<color=orange>(+5)</color>\nCost : 3\nSkill\n자신의 뒤쪽 아군 유닛의 공격력 증가";
                break;
            case 2:
                SubInfo.text = "ATK : 40<color=orange>(+5)</color>\nCost : 3\nSkill\n<color=orange>자신의 이동 경로상의 아군 유닛 공격력 증가</color>";
                break;
            case 3:
                SubInfo.text = "ATK : 40<color=orange>(+15)</color>\nCost : 3\nSkill\n<color=orange>자신의 이동 경로상의 아군 유닛 공격력 증가</color>";
                break;
        }
    }
    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }
}
