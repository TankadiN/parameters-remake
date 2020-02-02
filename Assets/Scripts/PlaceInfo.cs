using System;
using UnityEngine;

[Serializable]
public class PlaceInfo
{
    public float rectX, rectY, rectWidth, rectHeight;

    public enum Type {Place, Enemy, Shop, Jackpot, Treasure}
    public Type PlaceType;

    public bool GoldLock, SilverLock;
    
    public float ProgressNeeded;
    public float EnergyNeeded;

    public float MinExpOngoing, MaxExpOngoing;
    public float MinGoldOngoing, MaxGoldOngoing;
    public float MinGoldCompleted, MaxGoldCompleted;

    public float MaxHealth, EnemyAttack;
    public float MinExp, MaxExp;
    public float MinGold, MaxGold;
    public float MinSKeys, MaxSKeys, MinGKeys, MaxGKeys;

    public float Cost, Value;

    public PlaceInfo(
        float newRectX,
        float newRectY,
        float newRectWidth,
        float newRectHeight,
        Type newPlaceType,
        //Locks
        bool newGoldLock,
        bool newSilverLock,
        //For Place
        float newProgressNeeded,
        float newEnergyNeeded,
        //For Enemy/Place
        float newMinExpOngoing,
        float newMaxExpOngoing,
        //For Place
        float newMinGoldOngoing,
        float newMaxGoldOngoing,
        float newMinGoldCompleted, 
        float newMaxGoldCompleted,
        //For Enemy
        float newMaxHealth, 
        float newEnemyAttack,
        float newMinExp, 
        float newMaxExp,
        float newMinGold, 
        float newMaxGold,
        //Keys
        float newMinSKeys, 
        float newMaxSKeys, 
        float newMinGKeys, 
        float newMaxGKeys,
        //Misc.
        float newCost, 
        float newValue)
    {
        rectX = newRectX;
        rectY = newRectY;
        rectWidth = newRectWidth;
        rectHeight = newRectHeight;

        PlaceType = newPlaceType;

        GoldLock = newGoldLock;
        SilverLock = newSilverLock;

        ProgressNeeded = newProgressNeeded;
        EnergyNeeded = newEnergyNeeded;

        MinExpOngoing = newMinExpOngoing;
        MaxExpOngoing = newMaxExpOngoing;
        MinGoldOngoing = newMinGoldOngoing;
        MaxGoldOngoing = newMaxGoldOngoing;
        MinGoldCompleted = newMinGoldCompleted;
        MaxGoldCompleted = newMaxGoldCompleted;

        MaxHealth = newMaxHealth;
        EnemyAttack = newEnemyAttack;
        MinExp = newMinExp;
        MaxExp = newMaxExp;
        MinGold = newMinGold;
        MaxGold = newMaxGold;

        MinSKeys = newMinSKeys;
        MaxSKeys = newMaxSKeys;
        MinGKeys = newMinGKeys;
        MaxGKeys = newMaxGKeys;

        Cost = newCost;
        Value = newValue;
    }
}
