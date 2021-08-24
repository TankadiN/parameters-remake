using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadManager : MonoBehaviour
{
    public GameObject Container;
    public GameObject Place;
    public GameObject Enemy;
    public GameObject Shop;
    public GameObject Jackpot;
    public GameObject Treasure;

    public GameObject LoadPanel;

    private LevelData Level;
    private OutputLog OL;

    void Start()
    {
        LoadPanel.SetActive(true);
        Level = GetComponent<LevelData>();
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
        LoadGame();
    }

    public void LoadGame()
    {
        Level.ExecuteLoad(GlobalData.GD.levelString);
        foreach(EntityInfo e in Level.places)
        {
            if (e.type == EntityInfo.PlaceType.Place)
            {
                GameObject go = Instantiate(Place) as GameObject;
                go.name = "Place";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(e.posX, e.posY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(e.posWidth, e.posHeight);

                go.GetComponent<Place>().SilverLock = e.place.SilverLock;
                go.GetComponent<Place>().GoldLock = e.place.GoldLock;

                go.GetComponent<Place>().ProgressNeeded = e.place.ProgressNeeded;
                go.GetComponent<Place>().EnergyNeeded = e.place.EnergyNeeded;

                go.GetComponent<Place>().ExpOngoing = e.place.ExpOngoing;
                go.GetComponent<Place>().GoldOngoing = e.place.GoldOngoing;
                go.GetComponent<Place>().GoldCompleted = e.place.GoldCompleted;

                Debug.Log("Place Loaded");
            }
            if (e.type == EntityInfo.PlaceType.Enemy)
            {
                GameObject go = Instantiate(Enemy) as GameObject;
                go.name = "Enemy";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(e.posX, e.posY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(e.posWidth, e.posHeight);

                go.GetComponent<Enemy>().Boss = e.enemy.Boss;

                go.GetComponent<Enemy>().SilverLock = e.enemy.SilverLock;
                go.GetComponent<Enemy>().GoldLock = e.enemy.GoldLock;

                go.GetComponent<Enemy>().MaxHealth = e.enemy.MaxHealth;
                go.GetComponent<Enemy>().EnemyAttack = e.enemy.EnemyAttack;

                go.GetComponent<Enemy>().Exp = e.enemy.Exp;
                go.GetComponent<Enemy>().Gold = e.enemy.Gold;
                go.GetComponent<Enemy>().SKeys = e.enemy.SKeys;
                go.GetComponent<Enemy>().GKeys = e.enemy.GKeys;

                Debug.Log("Enemy Loaded");
            }
            if (e.type == EntityInfo.PlaceType.Shop)
            {
                GameObject go = Instantiate(Shop) as GameObject;
                go.name = "Shop";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(e.posX, e.posY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(e.posWidth, e.posHeight);

                go.GetComponent<Shop>().ShopType = e.shop.ShopType;
                go.GetComponent<Shop>().Cost = e.shop.Cost;
                go.GetComponent<Shop>().Value = e.shop.Value;

                Debug.Log("Shop Loaded");
            }
            if (e.type == EntityInfo.PlaceType.Jackpot)
            {
                GameObject go = Instantiate(Jackpot) as GameObject;
                go.name = "Jackpot";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(e.posX, e.posY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(e.posWidth, e.posHeight);

                go.GetComponent<Jackpot>().condition = e.jackpot.Condition;
                go.GetComponent<Jackpot>().conditionValue = e.jackpot.ConditionValue;
                go.GetComponent<Jackpot>().Cost = e.jackpot.Cost;

                Debug.Log("Jackpot Loaded");
            }
            if (e.type == EntityInfo.PlaceType.Treasure)
            {
                GameObject go = Instantiate(Treasure) as GameObject;
                go.name = "Treasure";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(e.posX, e.posY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(e.posWidth, e.posHeight);

                go.GetComponent<Treasure>().treasureMode = e.treasure.TreasureMode;
                go.GetComponent<Treasure>().Item = e.treasure.TreasureItem;

                go.GetComponent<Treasure>().condition = e.treasure.Condition;
                go.GetComponent<Treasure>().conditionValue = e.treasure.ConditionValue;

                go.GetComponent<Treasure>().GoldLock = e.treasure.GoldLock;

                go.GetComponent<Treasure>().Cost = e.treasure.Cost;
                go.GetComponent<Treasure>().Value = e.treasure.Value;

                Debug.Log("Treasure Loaded");
            }
        }
        GoalChecker.GC.GetPlaces();
        if (GameObject.Find("GameManager").GetComponent<Timer>().Active)
        {
            GameObject.Find("GameManager").GetComponent<Timer>().CountDown = true;
        }
        OL.Disable = false;
        LoadPanel.SetActive(false);
    }
}
