using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuy : MonoBehaviour
{
    public static int UpgradeCode = 0;

    private PawnUpgradeManagement PawnUpgradeBuy;
    private BishopUpgradeManagement BishopUpgradeBuy;
    private KnightUpgradeManagement KnightUpgradeBuy;
    private RookUpgradeManagement RookUpgradeBuy;
    private QueenUpgradeManagement QueenUpgradeBuy;

    private void Awake()
    {
        PawnUpgradeBuy = GameObject.Find("PawnUpgrade").GetComponent<PawnUpgradeManagement>();
        BishopUpgradeBuy = GameObject.Find("BishopUpgrade").GetComponent<BishopUpgradeManagement>();
        KnightUpgradeBuy = GameObject.Find("KnightUpgrade").GetComponent<KnightUpgradeManagement>();
        RookUpgradeBuy = GameObject.Find("RookUpgrade").GetComponent<RookUpgradeManagement>();
        QueenUpgradeBuy = GameObject.Find("QueenUpgrade").GetComponent<QueenUpgradeManagement>();
    }

    public void UpgradeProcess()
    {
        switch (UpgradeCode)
        {
            case 1:
                PawnUpgradeBuy.PawnUpgradeLv1();
                break;
            case 2:
                PawnUpgradeBuy.PawnUpgradeLv2();
                break;
            case 3:
                PawnUpgradeBuy.PawnUpgradeLv3();
                break;
            case 4:
                BishopUpgradeBuy.BishopUpgradeLv1();
                break;
            case 5:
                BishopUpgradeBuy.BishopUpgradeLv2();
                break;
            case 6:
                BishopUpgradeBuy.BishopUpgradeLv3();
                break;
            case 7:
                KnightUpgradeBuy.KnightUpgradeLv1();
                break;
            case 8:
                KnightUpgradeBuy.KnightUpgradeLv2();
                break;
            case 9:
                KnightUpgradeBuy.KnightUpgradeLv3();
                break;
            case 10:
                RookUpgradeBuy.RookUpgradeLv1();
                break;
            case 11:
                RookUpgradeBuy.RookUpgradeLv2();
                break;
            case 12:
                RookUpgradeBuy.RookUpgradeLv3();
                break;
            case 13:
                QueenUpgradeBuy.QueenUpgradeLv1();
                break;
            case 14:
                QueenUpgradeBuy.QueenUpgradeLv2();
                break;
            case 15:
                QueenUpgradeBuy.QueenUpgradeLv3();
                break;

            default:
                break;

        }
    }

}
