using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData LD;

    public string levelName;

    public List<EntityInfo> places;
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
                places.Add(new EntityInfo(
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
                places.Add(new EntityInfo(
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
                places.Add(new EntityInfo(
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
                places.Add(new EntityInfo(
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
                places.Add(new EntityInfo(
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

    public void LoadLevel(List<EntityInfo> saveplace)
    {
        foreach(EntityInfo e in saveplace)
        {
            places.Add(new EntityInfo(
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
        SaveLoad.Save<List<EntityInfo>>(places, levelName);
    }

    void Load()
    {
#if UNITY_EDITOR
        if(SaveLoad.SaveExists(levelName))
        {
            LoadLevel(SaveLoad.Load<List<EntityInfo>>(levelName));
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