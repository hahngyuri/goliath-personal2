using System;

public class LoadDatas
{
	Start()
	{
        //Cleared stage
        if (PlayerPrefs.HasKey("ClearedStage"))
        {
            MapIntroduce.currentStage = PlayerPrefs.GetInt("ClearedStage");
        }
        else
        {
            MapIntroduce.currentstage = -1;
        }

        //Money
        if (PlayerPrefs.HasKey("Money"))
        {
            PieceShopMoney.Money= PlayerPrefs.GetInt("Money");
        }
        //Upgraded
        if (PlayerPrefs.HasKey("PawnUpgrade"))
        {
            PawnScript.PawnUpgrade = PlayerPrefs.GetInt("PawnUpgrade");
            switch (PawnScript.PawnUpgrade)
            {
                case 0:
                    {
                        NewPawn.PieceDamage = 20;
                        break;
                    }
                case 1:
                    {
                        PawnUpgradeManagement.PawnUpgradeLv1();
                        break;
                    }
                case 2:
                    {
                        PawnUpgradeManagement.PawnUpgradeLv2();
                        break;
                    }
                case 3:
                    {
                        PawnUpgradeManagement.PawnUpgradeLv3();
                        break;
                    }
            }

            if (PlayerPrefs.HasKey("BishopUpgrade"))
            {
                BishopScript.BishopUpgrade = PlayerPrefs.GetInt("BishopUpgrade");
                switch (BishopScript.BishopUpgrade)
                {
                    case 0:
                        {
                            NewBishop.PieceDamage = 30;
                            break;
                        }
                    case 1:
                        {
                            BishopUpgradeManagement.BishopUpgradeLv1();
                            break;
                        }
                    case 2:
                        {
                            BishopUpgradeManagement.BishopUpgradeLv2();
                            break;
                        }
                    case 3:
                        {
                            BishopUpgradeManagement.BishopUpgradeLv3();
                            break;
                        }
                }
            }
            if (PlayerPrefs.HasKey("KnightUpgrade"))
            {
                KnightScript.KnightUpgrade = PlayerPrefs.GetInt("KnightUpgrade");
                switch (KnightScript.KnightUpgrade)
                {
                    case 0:
                        {
                            NewKnight.PieceDamage = 40;
                            break;
                        }
                    case 1:
                        {
                            KnightUpgradeManagement.KnightUpgradeLv1();
                            break;
                        }
                    case 2:
                        {
                            KnightUpgradeManagement.KnightUpgradeLv2();
                            break;
                        }
                    case 3:
                        {
                            KnightUpgradeManagement.KnightUpgradeLv3();
                            break;
                        }
                }
            }
            if (PlayerPrefs.HasKey("RookUpgrade"))
            {
                RookScript.RookUpgrade = PlayerPrefs.GetInt("RookUpgrade");
                switch (RookScript.RookUpgrade)
                {
                    case 0:
                        {
                            NewRook.PieceDamage = 45;
                            break;
                        }
                    case 1:
                        {
                            RookUpgradeManagement.RookUpgradeLv1();
                            break;
                        }
                    case 2:
                        {
                            RookUpgradeManagement.RookUpgradeLv2();
                            break;
                        }
                    case 3:
                        {
                            RookUpgradeManagement.RookUpgradeLv3();
                            break;
                        }
                }
            }
            if (PlayerPrefs.HasKey("QueenUpgrade"))
            {
                QueenScript.QueenUpgrade = PlayerPrefs.GetInt("QueenUpgrade");
                switch (QueenScript.QueenUpgrade)
                {
                    case 0:
                        {
                            NewQueen.PieceDamage = 20;
                            break;
                        }
                    case 1:
                        {
                            QueenUpgradeManagement.QueenUpgradeLv1();
                            break;
                        }
                    case 2:
                        {
                            QueenUpgradeManagement.QueenUpgradeLv2();
                            break;
                        }
                    case 3:
                        {
                            QueenUpgradeManagement.QueenUpgradeLv3();
                            break;
                        }
                }
            }


