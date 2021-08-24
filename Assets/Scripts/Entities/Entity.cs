using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceEnt
{
    public bool SilverLock;
    public bool GoldLock;
    public float ProgressNeeded;
    public float EnergyNeeded;
    public string ExpOngoing;
    public string GoldOngoing;
    public string GoldCompleted;

    public PlaceEnt(
        bool nSilverLock,
        bool nGoldLock,
        float nProgressNeeded,
        float nEnergyNeeded,
        string nExpOngoing,
        string nGoldOngoing,
        string nGoldCompleted)
    {
        SilverLock = nSilverLock;
        GoldLock = nGoldLock;
        ProgressNeeded = nProgressNeeded;
        EnergyNeeded = nEnergyNeeded;
        ExpOngoing = nExpOngoing;
        GoldOngoing = nGoldOngoing;
        GoldCompleted = nGoldCompleted;
    }
}

[System.Serializable]
public class EnemyEnt
{
    public bool Boss;
    public bool SilverLock;
    public bool GoldLock;
    public float MaxHealth;
    public float EnemyAttack;
    public string Exp;
    public string Gold;
    public string SKeys;
    public string GKeys;

    public EnemyEnt(
        bool nBoss,
        bool nSilverLock,
        bool nGoldLock,
        float nMaxHealth,
        float nEnemyAttack,
        string nExp,
        string nGold,
        string nSKeys,
        string nGKeys)
    {
        Boss = nBoss;
        SilverLock = nSilverLock;
        GoldLock = nGoldLock;
        MaxHealth = nMaxHealth;
        EnemyAttack = nEnemyAttack;
        Exp = nExp;
        Gold = nGold;
        SKeys = nSKeys;
        GKeys = nGKeys;
    }
}

[System.Serializable]
public class ShopEnt
{
    public Shop.ShopList ShopType;
    public float Cost;
    public float Value;

    public ShopEnt(
        Shop.ShopList nShopType,
        float nCost,
        float nValue)
    {
        ShopType = nShopType;
        Cost = nCost;
        Value = nValue;
    }
}

[System.Serializable]
public class JackpotEnt
{
    public Player.Condition Condition;
    public int ConditionValue;
    public float Cost;

    public JackpotEnt(
        Player.Condition nCondition,
        int nConditionValue,
        float nCost)
    {
        Condition = nCondition;
        ConditionValue = nConditionValue;
        Cost = nCost;
    }
}

[System.Serializable]
public class TreasureEnt
{
    public Treasure.TreasureType TreasureMode;
    public Treasure.ItemType TreasureItem;
    public Player.Condition Condition;
    public int ConditionValue;
    public bool GoldLock;
    public float Cost;
    public float Value;

    public TreasureEnt(
        Treasure.TreasureType nTreasureMode,
        Treasure.ItemType nTreasureItem,
        Player.Condition nCondition,
        int nConditionValue,
        bool nGoldLock,
        float nCost,
        float nValue)
    {
        TreasureMode = nTreasureMode;
        TreasureItem = nTreasureItem;
        Condition = nCondition;
        ConditionValue = nConditionValue;
        GoldLock = nGoldLock;
        Cost = nCost;
        Value = nValue;
    }
}
