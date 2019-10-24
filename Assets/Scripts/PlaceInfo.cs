using System;

[Serializable]
public class PlaceInfo
{
    public float rectX, rectY, rectWidth, rectHeight;

    public enum Type {Place, Enemy, Shop, Jackpot}
    public Type PlaceType;

    public bool GoldLock, SilverLock;
    
    public float ProgressNeeded;
    public float EnergyNeeded;

    public float MinExpOngoing, MaxExpOngoing;
    public float MinGoldOngoing, MaxGoldOngoing;
    public float MinGoldCompleted, MaxGoldCompleted;

    public float MaxHealth, EnemyAttack;
    public float MinSKeys, MaxSKeys, MinGKeys, MaxGKeys;

    public float Cost, Value;
}
