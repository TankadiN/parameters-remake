using System;
using System.Collections;

[Serializable]
public class SaveLevelData
{
    public float[] srectX, srectY, srectWidth, srectHeight;

    public enum Type { Place, Enemy, Shop, Jackpot }
    public Type[] sPlaceType;

    public enum Shops { Keys, Attack, Defense }
    public Shops[] sShopType;

    public bool[] sGoldLock, sSilverLock;

    public float[] sProgressNeeded;
    public float[] sEnergyNeeded;

    public float[] sMinExpOngoing, sMaxExpOngoing;
    public float[] sMinGoldOngoing, sMaxGoldOngoing;
    public float[] sMinGoldCompleted, sMaxGoldCompleted;

    public float[] sMinExp, sMaxExp;
    public float[] sMinGold, sMaxGold;
    public float[] sMaxHealth, sEnemyAttack;
    public float[] sMinSKeys, sMaxSKeys, sMinGKeys, sMaxGKeys;

    public bool[] sActive, sFree, sFreeBought;
    public string[] sMethod;

    public float[] sCost, sValue;

    public SaveLevelData(LevelData Level)
    {
        srectX = new float[Level.targetplace.Length];
        srectY = new float[Level.targetplace.Length];
        srectWidth = new float[Level.targetplace.Length];
        srectHeight = new float[Level.targetplace.Length];

        sPlaceType = new Type[Level.targetplace.Length];
        sShopType = new Shops[Level.targetplace.Length];

        sGoldLock = new bool[Level.targetplace.Length];
        sSilverLock = new bool[Level.targetplace.Length];

        sProgressNeeded = new float[Level.targetplace.Length];
        sEnergyNeeded = new float[Level.targetplace.Length];

        sMinExpOngoing = new float[Level.targetplace.Length];
        sMaxExpOngoing = new float[Level.targetplace.Length];
        sMinGoldOngoing = new float[Level.targetplace.Length];
        sMaxGoldOngoing = new float[Level.targetplace.Length];
        sMinGoldCompleted = new float[Level.targetplace.Length];
        sMaxGoldCompleted = new float[Level.targetplace.Length];

        sMaxHealth = new float[Level.targetplace.Length];
        sEnemyAttack = new float[Level.targetplace.Length];

        sMinExp = new float[Level.targetplace.Length];
        sMaxExp = new float[Level.targetplace.Length];
        sMinGold = new float[Level.targetplace.Length];
        sMaxGold = new float[Level.targetplace.Length];

        sMinSKeys = new float[Level.targetplace.Length];
        sMaxSKeys = new float[Level.targetplace.Length];
        sMinGKeys = new float[Level.targetplace.Length];
        sMaxGKeys = new float[Level.targetplace.Length];

        sActive = new bool[Level.targetplace.Length];
        sFree = new bool[Level.targetplace.Length];
        sFreeBought = new bool[Level.targetplace.Length];
        sMethod = new string[Level.targetplace.Length];

        sCost = new float[Level.targetplace.Length];
        sValue = new float[Level.targetplace.Length];
        for (int i = 0; i <= Level.targetplace.Length - 1; i++)
        {
            srectX[i] = Level.rectX[i];
            srectY[i] = Level.rectY[i];
            srectWidth[i] = Level.rectWidth[i];
            srectHeight[i] = Level.rectHeight[i];

            if(Level.PlaceType[i] == LevelData.Type.Place)
            {
                sPlaceType[i] = Type.Place;
            }
            if (Level.PlaceType[i] == LevelData.Type.Enemy)
            {
               sPlaceType[i] = Type.Enemy;
            }
            if (Level.PlaceType[i] == LevelData.Type.Shop)
            {
                sPlaceType[i] = Type.Shop;
            }
            if (Level.PlaceType[i] == LevelData.Type.Jackpot)
            {
                sPlaceType[i] = Type.Jackpot;
            }

            sGoldLock[i] = Level.GoldLock[i];
            sSilverLock[i] = Level.SilverLock[i];

            sProgressNeeded[i] = Level.ProgressNeeded[i];
            sEnergyNeeded[i] = Level.EnergyNeeded[i];

            sMinExpOngoing[i] = Level.MinExpOngoing[i];
            sMaxExpOngoing[i] = Level.MaxExpOngoing[i];
            sMinGoldOngoing[i] = Level.MinGoldOngoing[i];
            sMaxGoldOngoing[i] = Level.MaxGoldOngoing[i];
            sMinGoldCompleted[i] = Level.MinGoldCompleted[i];
            sMaxGoldCompleted[i] = Level.MaxGoldCompleted[i];

            sMaxHealth[i] = Level.MaxHealth[i];
            sEnemyAttack[i] = Level.EnemyAttack[i];

            sMinExp[i] = Level.MinExp[i];
            sMaxExp[i] = Level.MaxExp[i];
            sMinGold[i] = Level.MinGold[i];
            sMaxGold[i] = Level.MaxGold[i];

            sMinSKeys[i] = Level.MinSKeys[i];
            sMaxSKeys[i] = Level.MaxSKeys[i];
            sMinGKeys[i] = Level.MinGKeys[i];
            sMaxGKeys[i] = Level.MaxGKeys[i];

            if(Level.ShopType[i] == LevelData.Shops.Keys)
            {
                sShopType[i] = Shops.Keys;
            }
            if (Level.ShopType[i] == LevelData.Shops.Attack)
            {
                sShopType[i] = Shops.Attack;
            }
            if (Level.ShopType[i] == LevelData.Shops.Defense)
            {
                sShopType[i] = Shops.Defense;
            }

            sCost[i] = Level.Cost[i];
            sValue[i] = Level.Value[i];
        }
    }
}
