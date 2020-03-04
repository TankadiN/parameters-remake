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
        foreach (PlaceInfo p in Level.Places)
        {
            if (p.PlaceType == PlaceInfo.Type.Place)
            {
                GameObject go = Instantiate(Place) as GameObject;
                go.name = "Place";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(p.rectX, p.rectY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(p.rectWidth, p.rectHeight);


                go.GetComponent<Place>().SilverLock = p.SilverLock;
                go.GetComponent<Place>().GoldLock = p.GoldLock;

                go.GetComponent<Place>().ProgressNeeded = p.ProgressNeeded;
                go.GetComponent<Place>().EnergyNeeded = p.EnergyNeeded;

                go.GetComponent<Place>().MinExpOngoing = p.MinExpOngoing;
                go.GetComponent<Place>().MaxExpOngoing = p.MaxExpOngoing;

                go.GetComponent<Place>().MinGoldOngoing = p.MinGoldOngoing;
                go.GetComponent<Place>().MaxGoldOngoing = p.MaxGoldOngoing;

                go.GetComponent<Place>().MinGoldCompleted = p.MinGoldCompleted;
                go.GetComponent<Place>().MaxGoldCompleted = p.MaxGoldCompleted;
                Debug.Log("Place Loaded");
            }
            if (p.PlaceType == PlaceInfo.Type.Enemy)
            {
                GameObject go = Instantiate(Enemy) as GameObject;
                go.name = "Enemy";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(p.rectX, p.rectY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(p.rectWidth, p.rectHeight);

                go.GetComponent<Enemy>().SilverLock = p.SilverLock;
                go.GetComponent<Enemy>().GoldLock = p.GoldLock;

                go.GetComponent<Enemy>().MaxHealth = p.MaxHealth;
                go.GetComponent<Enemy>().EnemyAttack = p.EnemyAttack;

                go.GetComponent<Enemy>().MinExp = p.MinExp;
                go.GetComponent<Enemy>().MaxExp = p.MaxExp;

                go.GetComponent<Enemy>().MinGold = p.MinGold;
                go.GetComponent<Enemy>().MaxGold = p.MaxGold;

                go.GetComponent<Enemy>().Boss = p.Boss;

                go.GetComponent<Enemy>().MinSKeys = p.MinSKeys;
                go.GetComponent<Enemy>().MaxSKeys = p.MaxSKeys;

                go.GetComponent<Enemy>().MinGKeys = p.MinGKeys;
                go.GetComponent<Enemy>().MaxGKeys = p.MaxGKeys;
                Debug.Log("Enemy Loaded");
            }
            if (p.PlaceType == PlaceInfo.Type.Shop)
            {
                GameObject go = Instantiate(Shop) as GameObject;
                go.name = "Shop";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(p.rectX, p.rectY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(p.rectWidth, p.rectHeight);

                go.GetComponent<Shop>().ShopType = p.StoreType;
                go.GetComponent<Shop>().Cost = p.Cost;
                go.GetComponent<Shop>().Value = p.Value;
                Debug.Log("Shop Loaded");
            }
            if (p.PlaceType == PlaceInfo.Type.Jackpot)
            {
                GameObject go = Instantiate(Jackpot) as GameObject;
                go.name = "Jackpot";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(p.rectX, p.rectY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(p.rectWidth, p.rectHeight);

                go.GetComponent<Jackpot>().Cost = p.Cost;
                go.GetComponent<Jackpot>().condition = p.Condition;
                go.GetComponent<Jackpot>().conditionValue = p.ConditionValue;
                Debug.Log("Jackpot Loaded");
            }
            if (p.PlaceType == PlaceInfo.Type.Treasure)
            {
                GameObject go = Instantiate(Treasure) as GameObject;
                go.name = "Treasure";
                go.transform.SetParent(Container.transform);

                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().position = new Vector2(p.rectX, p.rectY);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(p.rectWidth, p.rectHeight);

                go.GetComponent<Treasure>().Cost = p.Cost;
                go.GetComponent<Treasure>().condition = p.Condition;
                go.GetComponent<Treasure>().conditionValue = p.ConditionValue;

                go.GetComponent<Treasure>().treasureMode = p.TreasureMode;
                go.GetComponent<Treasure>().Item = p.TreasureItem;
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
