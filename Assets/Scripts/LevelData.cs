using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData LD;

    public string levelName;

    public List<PlaceInfo> Places;
    public GameObject[] targetplace;

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
    }

    private IEnumerator GetPlaces()
    {
        targetplace = GameObject.FindGameObjectsWithTag("Place");
        yield return new WaitForSeconds(0.5f);
        GetPlacesData();
    }

    public void GetPlacesData()
    {
        foreach (GameObject o in targetplace)
        {
            if (o.GetComponent<Place>())
            {
                Places.Add(new PlaceInfo(
                o.GetComponent<RectTransform>().position.x,
                o.GetComponent<RectTransform>().position.y,
                o.GetComponent<RectTransform>().rect.width,
                o.GetComponent<RectTransform>().rect.height,
                PlaceInfo.Type.Place,
                o.GetComponent<Place>().SilverLock,
                o.GetComponent<Place>().GoldLock,
                o.GetComponent<Place>().ProgressNeeded,
                o.GetComponent<Place>().EnergyNeeded,
                o.GetComponent<Place>().MinExpOngoing,
                o.GetComponent<Place>().MaxExpOngoing,
                o.GetComponent<Place>().MinGoldOngoing,
                o.GetComponent<Place>().MaxGoldOngoing,
                o.GetComponent<Place>().MinGoldCompleted,
                o.GetComponent<Place>().MaxGoldCompleted,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
                ));
                Debug.Log("Saved Place");
            }
            if (o.GetComponent<Enemy>())
            {
                Places.Add(new PlaceInfo(
                o.GetComponent<RectTransform>().position.x,
                o.GetComponent<RectTransform>().position.y,
                o.GetComponent<RectTransform>().rect.width,
                o.GetComponent<RectTransform>().rect.height,
                PlaceInfo.Type.Enemy,
                o.GetComponent<Enemy>().SilverLock,
                o.GetComponent<Enemy>().GoldLock,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                o.GetComponent<Enemy>().MaxHealth,
                o.GetComponent<Enemy>().EnemyAttack,
                o.GetComponent<Enemy>().MinExp,
                o.GetComponent<Enemy>().MaxExp,
                o.GetComponent<Enemy>().MinGold,
                o.GetComponent<Enemy>().MaxGold,
                o.GetComponent<Enemy>().MinSKeys,
                o.GetComponent<Enemy>().MaxSKeys,
                o.GetComponent<Enemy>().MinGKeys,
                o.GetComponent<Enemy>().MaxGKeys,
                0,
                0,
                0,
                0,
                0,
                0,
                0
                ));
                Debug.Log("Saved Enemy");
            }
            if (o.GetComponent<Shop>())
            {
                Places.Add(new PlaceInfo(
                o.GetComponent<RectTransform>().position.x,
                o.GetComponent<RectTransform>().position.y,
                o.GetComponent<RectTransform>().rect.width,
                o.GetComponent<RectTransform>().rect.height,
                PlaceInfo.Type.Shop,
                false,
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                o.GetComponent<Shop>().Cost,
                o.GetComponent<Shop>().Value,
                o.GetComponent<Shop>().ShopType,
                0,
                0,
                0,
                0
                ));
            }
            if (o.GetComponent<Jackpot>())
            {
                Places.Add(new PlaceInfo(
                o.GetComponent<RectTransform>().position.x,
                o.GetComponent<RectTransform>().position.y,
                o.GetComponent<RectTransform>().rect.width,
                o.GetComponent<RectTransform>().rect.height,
                PlaceInfo.Type.Jackpot,
                false,
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                o.GetComponent<Jackpot>().Cost,
                0,
                0,
                o.GetComponent<Jackpot>().condition,
                o.GetComponent<Jackpot>().conditionValue,
                0,
                0
                ));
            }
            if (o.GetComponent<Treasure>())
            {
                Places.Add(new PlaceInfo(
                o.GetComponent<RectTransform>().position.x,
                o.GetComponent<RectTransform>().position.y,
                o.GetComponent<RectTransform>().rect.width,
                o.GetComponent<RectTransform>().rect.height,
                PlaceInfo.Type.Treasure,
                false,
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                o.GetComponent<Treasure>().Cost,
                o.GetComponent<Treasure>().Value,
                0,
                o.GetComponent<Treasure>().condition,
                o.GetComponent<Treasure>().conditionValue,
                o.GetComponent<Treasure>().treasureMode,
                o.GetComponent<Treasure>().Item
                ));
            }
        }
    }

    public void LoadLevel(List<PlaceInfo> saveplace)
    {
        foreach(PlaceInfo p in saveplace)
        {
            Places.Add(new PlaceInfo(
                p.rectX,
                p.rectY,
                p.rectWidth,
                p.rectHeight,
                p.PlaceType,
                p.GoldLock,
                p.SilverLock,
                p.ProgressNeeded,
                p.EnergyNeeded,
                p.MinExpOngoing,
                p.MaxExpOngoing,
                p.MinGoldOngoing,
                p.MaxGoldOngoing,
                p.MinGoldCompleted,
                p.MaxGoldCompleted,
                p.MaxHealth,
                p.EnemyAttack,
                p.MinExp,
                p.MaxExp,
                p.MinGold,
                p.MaxGold,
                p.MinSKeys,
                p.MaxSKeys,
                p.MinGKeys,
                p.MaxGKeys,
                p.Cost,
                p.Value,
                p.StoreType,
                p.Condition,
                p.ConditionValue,
                p.TreasureMode,
                p.TreasureItem
                ));
            Debug.Log("Loaded entity");
        }
    }

    void Save()
    {
        SaveLoad.Save<List<PlaceInfo>>(Places, levelName);
    }

    void Load()
    {
        if(SaveLoad.SaveExists(levelName))
        {
            LoadLevel(SaveLoad.Load<List<PlaceInfo>>(levelName));
        }
    }

    public void CollectLevelData()
    {
        StartCoroutine(GetPlaces());
    }

    public void CollectInstantiatedPlaces()
    {
        targetplace = GameObject.FindGameObjectsWithTag("Place");
    }

    public void SaveToFile()
    {
        GameEvents.OnSaveInitiated();
    }

    public void ExecuteLoad(string level)
    {
        levelName = level;
        Load();
    }
}