using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityInfo
{
    public enum PlaceType
    {
        None,
        Place,
        Enemy,
        Shop,
        Jackpot,
        Treasure,
    }

    public float posX;
    public float posY;
    public float posWidth;
    public float posHeight;

    public PlaceType type;

    public PlaceEnt place;
    public EnemyEnt enemy;
    public ShopEnt shop;
    public JackpotEnt jackpot;
    public TreasureEnt treasure;

    public EntityInfo(
        float nPosX,
        float nPosY,
        float nPosWidth,
        float nPosHeight,
        PlaceType nType,
        PlaceEnt nPlace,
        EnemyEnt nEnemy,
        ShopEnt nShop,
        JackpotEnt nJackpot,
        TreasureEnt nTreasure)
    {
        posX = nPosX;
        posY = nPosY;
        posWidth = nPosWidth;
        posHeight = nPosHeight;

        type = nType;

        place = nPlace;
        enemy = nEnemy;
        shop = nShop;
        jackpot = nJackpot;
        treasure = nTreasure;
    }
}
