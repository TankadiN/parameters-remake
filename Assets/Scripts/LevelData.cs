using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData LD;

    public string levelName;

    public List<PlaceInfo> Places;
    public List<EntityInfo> placesV2;
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
                o.GetComponent<Place>().MinimalExpOngoing,
                o.GetComponent<Place>().MaximumExpOngoing,
                o.GetComponent<Place>().MinimalGoldOngoing,
                o.GetComponent<Place>().MaximumGoldOngoing,
                o.GetComponent<Place>().MinimalGoldCompleted,
                o.GetComponent<Place>().MaximumGoldCompleted,
                0,
                0,
                0,
                0,
                0,
                0,
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
                o.GetComponent<Enemy>().Boss,
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
                false,
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
                false,
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
                false,
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
        //V2
        foreach (GameObject o in targetplace)
        {
            if (o.GetComponent<Place>())
            {
                placesV2.Add(new EntityInfo(
                    o.GetComponent<RectTransform>().position.x,
                    o.GetComponent<RectTransform>().position.y,
                    o.GetComponent<RectTransform>().rect.width,
                    o.GetComponent<RectTransform>().rect.height,
                    EntityInfo.PlaceType.Place,
                    new PlaceEnt(
                        o.GetComponent<Place>().SilverLock,
                        o.GetComponent<Place>().GoldLock,
                        o.GetComponent<Place>().ProgressNeeded,
                        o.GetComponent<Place>().EnergyNeeded,
                        o.GetComponent<Place>().ExpOngoing,
                        o.GetComponent<Place>().GoldOngoing,
                        o.GetComponent<Place>().GoldCompleted
                    ),
                    null,
                    null,
                    null,
                    null));
            }
            if (o.GetComponent<Enemy>())
            {
                placesV2.Add(new EntityInfo(
                    o.GetComponent<RectTransform>().position.x,
                    o.GetComponent<RectTransform>().position.y,
                    o.GetComponent<RectTransform>().rect.width,
                    o.GetComponent<RectTransform>().rect.height,
                    EntityInfo.PlaceType.Enemy,
                    null,
                    new EnemyEnt(
                        o.GetComponent<Enemy>().Boss,
                        o.GetComponent<Enemy>().SilverLock,
                        o.GetComponent<Enemy>().GoldLock,
                        o.GetComponent<Enemy>().MaxHealth,
                        o.GetComponent<Enemy>().EnemyAttack,
                        o.GetComponent<Enemy>().Exp,
                        o.GetComponent<Enemy>().Gold,
                        o.GetComponent<Enemy>().SKeys,
                        o.GetComponent<Enemy>().GKeys
                    ),
                    null,
                    null,
                    null));
            }
            if (o.GetComponent<Shop>())
            {
                placesV2.Add(new EntityInfo(
                    o.GetComponent<RectTransform>().position.x,
                    o.GetComponent<RectTransform>().position.y,
                    o.GetComponent<RectTransform>().rect.width,
                    o.GetComponent<RectTransform>().rect.height,
                    EntityInfo.PlaceType.Shop,
                    null,
                    null,
                    new ShopEnt(
                        o.GetComponent<Shop>().ShopType,
                        o.GetComponent<Shop>().Cost,
                        o.GetComponent<Shop>().Value),
                    null,
                    null));

            }
            if (o.GetComponent<Jackpot>())
            {
                placesV2.Add(new EntityInfo(
                    o.GetComponent<RectTransform>().position.x,
                    o.GetComponent<RectTransform>().position.y,
                    o.GetComponent<RectTransform>().rect.width,
                    o.GetComponent<RectTransform>().rect.height,
                    EntityInfo.PlaceType.Jackpot,
                    null,
                    null,
                    null,
                    new JackpotEnt(
                        o.GetComponent<Jackpot>().condition,
                        o.GetComponent<Jackpot>().conditionValue,
                        o.GetComponent<Jackpot>().Cost),
                    null));
            }
            if (o.GetComponent<Treasure>())
            {
                placesV2.Add(new EntityInfo(
                    o.GetComponent<RectTransform>().position.x,
                    o.GetComponent<RectTransform>().position.y,
                    o.GetComponent<RectTransform>().rect.width,
                    o.GetComponent<RectTransform>().rect.height,
                    EntityInfo.PlaceType.Treasure,
                    null,
                    null,
                    null,
                    null,
                    new TreasureEnt(
                        o.GetComponent<Treasure>().treasureMode,
                        o.GetComponent<Treasure>().Item,
                        o.GetComponent<Treasure>().condition,
                        o.GetComponent<Treasure>().conditionValue,
                        o.GetComponent<Treasure>().GoldLock,
                        o.GetComponent<Treasure>().Cost,
                        o.GetComponent<Treasure>().Value)));
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
                p.SilverLock,
                p.GoldLock,
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
                p.Boss,
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
            Debug.Log("Loaded place");
        }
    }
    //V2
    public void LoadLevelV2(List<EntityInfo> saveplace)
    {
        foreach(EntityInfo e in saveplace)
        {
            placesV2.Add(new EntityInfo(
                e.posX,
                e.posY,
                e.posWidth,
                e.posHeight,

                e.type,
                
                e.place,
                e.enemy,
                e.shop,
                e.jackpot,
                e.treasure));
        }
    }

    void Save()
    {
        //SaveLoad.Save<List<PlaceInfo>>(Places, levelName);
        //V2
        SaveLoad.Save<List<EntityInfo>>(placesV2, levelName);
    }

    void Load()
    {
#if UNITY_EDITOR
        if(SaveLoad.SaveExists(levelName))
        {
            //LoadLevel(SaveLoad.Load<List<PlaceInfo>>(levelName));
            //V2
            LoadLevelV2(SaveLoad.Load<List<EntityInfo>>(levelName));
        }
#else
        if(SaveLoad.SaveExistsInDataPath(levelName))
        {
            LoadLevel(SaveLoad.LoadFromDataPath<List<PlaceInfo>>(levelName));
        }
#endif
    }

    public void CollectLevelData()
    {
        StartCoroutine(GetPlaces());
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