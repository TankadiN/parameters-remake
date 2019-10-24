using System.Collections;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData LD;

    public GameObject[] targetplace;

    public float[] rectX, rectY, rectWidth, rectHeight;

    public enum Type { Place, Enemy, Shop, Jackpot, Treasure }
    public Type[] PlaceType;

    public enum Shops { Keys, Attack, Defense }
    public Shops[] ShopType;

    public bool[] GoldLock, SilverLock;

    public float[] ProgressNeeded;
    public float[] EnergyNeeded;

    public float[] MinExpOngoing, MaxExpOngoing;
    public float[] MinGoldOngoing, MaxGoldOngoing;
    public float[] MinGoldCompleted, MaxGoldCompleted;

    public float[] MinExp, MaxExp;
    public float[] MinGold, MaxGold;

    public float[] MaxHealth, EnemyAttack;

    public float[] MinSKeys, MaxSKeys, MinGKeys, MaxGKeys;

    public bool[] Active, Free, FreeBought;
    public string[] Method;

    public float[] Cost, Value;

    private int i = 0;

    public void GetInstPlaces()
    {
        targetplace = GameObject.FindGameObjectsWithTag("Place");
    }

    public IEnumerator GetPlaces()
    {
        targetplace = GameObject.FindGameObjectsWithTag("Place");
        rectX = new float[targetplace.Length];
        rectY = new float[targetplace.Length];
        rectWidth = new float[targetplace.Length];
        rectHeight = new float[targetplace.Length];

        PlaceType = new Type[targetplace.Length];
        ShopType = new Shops[targetplace.Length];

        GoldLock = new bool[targetplace.Length];
        SilverLock = new bool[targetplace.Length];

        ProgressNeeded = new float[targetplace.Length];
        EnergyNeeded = new float[targetplace.Length];

        MinExpOngoing = new float[targetplace.Length];
        MaxExpOngoing = new float[targetplace.Length];
        MinGoldOngoing = new float[targetplace.Length];
        MaxGoldOngoing = new float[targetplace.Length];
        MinGoldCompleted = new float[targetplace.Length];
        MaxGoldCompleted = new float[targetplace.Length];

        MaxHealth = new float[targetplace.Length];
        EnemyAttack = new float[targetplace.Length];

        MinExp = new float[targetplace.Length];
        MaxExp = new float[targetplace.Length];
        MinGold = new float[targetplace.Length];
        MaxGold = new float[targetplace.Length];

        MinSKeys = new float[targetplace.Length];
        MaxSKeys = new float[targetplace.Length];
        MinGKeys = new float[targetplace.Length];
        MaxGKeys = new float[targetplace.Length];

        Active = new bool[targetplace.Length];
        Free = new bool[targetplace.Length];
        FreeBought = new bool[targetplace.Length];
        Method = new string[targetplace.Length];

        Cost = new float[targetplace.Length];
        Value = new float[targetplace.Length];

        yield return new WaitForSeconds(0.5f);
        GetData();
    }

    public void GetData()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Place"))
        {
            rectX[i] = o.GetComponent<RectTransform>().anchoredPosition.x;
            rectY[i] = o.GetComponent<RectTransform>().anchoredPosition.y;
            rectWidth[i] = o.GetComponent<RectTransform>().rect.width;
            rectHeight[i] = o.GetComponent<RectTransform>().rect.height;
            if (o.GetComponent<Place>())
            {
                PlaceType[i] = Type.Place;

                SilverLock[i] = o.GetComponent<Place>().SilverLock;
                GoldLock[i] = o.GetComponent<Place>().GoldLock;

                ProgressNeeded[i] = o.GetComponent<Place>().ProgressNeeded;
                EnergyNeeded[i] = o.GetComponent<Place>().EnergyNeeded;

                MinExpOngoing[i] = o.GetComponent<Place>().MinExpOngoing;
                MaxExpOngoing[i] = o.GetComponent<Place>().MaxExpOngoing;

                MinGoldOngoing[i] = o.GetComponent<Place>().MinGoldOngoing;
                MaxGoldOngoing[i] = o.GetComponent<Place>().MaxGoldOngoing;

                MinGoldCompleted[i] = o.GetComponent<Place>().MinGoldCompleted;
                MaxGoldCompleted[i] = o.GetComponent<Place>().MaxGoldCompleted;
                Debug.Log("Place Fetched");
            }
            if (o.GetComponent<Enemy>())
            {
                PlaceType[i] = Type.Enemy;

                MaxHealth[i] = o.GetComponent<Enemy>().MaxHealth;
                EnemyAttack[i] = o.GetComponent<Enemy>().EnemyAttack;

                MinExp[i] = o.GetComponent<Enemy>().MinExp;
                MaxExp[i] = o.GetComponent<Enemy>().MaxExp;

                MinGold[i] = o.GetComponent<Enemy>().MinGold;
                MaxGold[i] = o.GetComponent<Enemy>().MaxGold;

                MinSKeys[i] = o.GetComponent<Enemy>().MinSKeys;
                MaxSKeys[i] = o.GetComponent<Enemy>().MaxSKeys;
                MinGKeys[i] = o.GetComponent<Enemy>().MinGKeys;
                MaxGKeys[i] = o.GetComponent<Enemy>().MaxGKeys;
                Debug.Log("Enemy Fetched");
            }
            if(o.GetComponent<Shop>())
            {
                PlaceType[i] = Type.Shop;
                if(o.GetComponent<Shop>().ShopType == Shop.ShopList.Keys)
                {
                    ShopType[i] = Shops.Keys;
                }
                if (o.GetComponent<Shop>().ShopType == Shop.ShopList.Attack)
                {
                    ShopType[i] = Shops.Attack;
                }
                if (o.GetComponent<Shop>().ShopType == Shop.ShopList.Defense)
                {
                    ShopType[i] = Shops.Defense;
                }
                Cost[i] = o.GetComponent<Shop>().Cost;
                Value[i] = o.GetComponent<Shop>().Value;
                Debug.Log("Shop Fetched");
            }
            if(o.GetComponent<Jackpot>())
            {
                PlaceType[i] = Type.Jackpot;
                Cost[i] = o.GetComponent<Jackpot>().Cost;
                Debug.Log("Jackpot Fetched");
            }
            if (o.GetComponent<Treasure>())
            {
                PlaceType[i] = Type.Treasure;
                Active[i] = o.GetComponent<Treasure>().Active;
                GoldLock[i] = o.GetComponent<Treasure>().GoldLock;
                Method[i] = o.GetComponent<Treasure>().Method;
                Cost[i] = o.GetComponent<Treasure>().Cost;
                Value[i] = o.GetComponent<Treasure>().Value;
                Free[i] = o.GetComponent<Treasure>().Free;
                FreeBought[i] = o.GetComponent<Treasure>().FreeBought;
                Debug.Log("Treasure Fetched");
            }
            i++;
        }
        i = 0;
        Debug.Log("Done Succesfully.");
    }

    public IEnumerator LoadPlaces(string levelName)
    {
        SaveLevelData ldata = SaveLoadSystem.LoadLevel(levelName);
        rectX = new float[ldata.srectX.Length];
        rectY = new float[ldata.srectY.Length];
        rectWidth = new float[ldata.srectWidth.Length];
        rectHeight = new float[ldata.srectHeight.Length];

        PlaceType = new Type[ldata.sPlaceType.Length];
        ShopType = new Shops[ldata.sShopType.Length];

        GoldLock = new bool[ldata.sGoldLock.Length];
        SilverLock = new bool[ldata.sSilverLock.Length];

        ProgressNeeded = new float[ldata.sProgressNeeded.Length];
        EnergyNeeded = new float[ldata.sEnergyNeeded.Length];

        MinExpOngoing = new float[ldata.sMinExpOngoing.Length];
        MaxExpOngoing = new float[ldata.sMaxExpOngoing.Length];
        MinGoldOngoing = new float[ldata.sMinGoldOngoing.Length];
        MaxGoldOngoing = new float[ldata.sMaxGoldOngoing.Length];
        MinGoldCompleted = new float[ldata.sMaxGoldCompleted.Length];
        MaxGoldCompleted = new float[ldata.sMaxGoldCompleted.Length];

        MaxHealth = new float[ldata.sMaxHealth.Length];
        EnemyAttack = new float[ldata.sEnemyAttack.Length];

        MinExp = new float[ldata.sMinExp.Length];
        MaxExp = new float[ldata.sMaxExp.Length];
        MinGold = new float[ldata.sMinGold.Length];
        MaxGold = new float[ldata.sMaxGold.Length];

        MinSKeys = new float[ldata.sMinSKeys.Length];
        MaxSKeys = new float[ldata.sMaxSKeys.Length];
        MinGKeys = new float[ldata.sMinGKeys.Length];
        MaxGKeys = new float[ldata.sMaxGKeys.Length];

        Active = new bool[ldata.sActive.Length];
        Free = new bool[ldata.sFree.Length];
        FreeBought = new bool[ldata.sFreeBought.Length];
        Method = new string[ldata.sMethod.Length];

        Cost = new float[ldata.sCost.Length];
        Value = new float[ldata.sValue.Length];

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= rectX.Length - 1; i++)
        {
            rectX[i] = ldata.srectX[i];
            rectY[i] = ldata.srectY[i];
            rectWidth[i] = ldata.srectWidth[i];
            rectHeight[i] = ldata.srectHeight[i];

            if (ldata.sPlaceType[i] == SaveLevelData.Type.Place)
            {
                PlaceType[i] = Type.Place;
            }
            if (ldata.sPlaceType[i] == SaveLevelData.Type.Enemy)
            {
                PlaceType[i] = Type.Enemy;
            }
            if (ldata.sPlaceType[i] == SaveLevelData.Type.Shop)
            {
                PlaceType[i] = Type.Shop;
            }
            if (ldata.sPlaceType[i] == SaveLevelData.Type.Jackpot)
            {
                PlaceType[i] = Type.Jackpot;
            }

            GoldLock[i] = ldata.sGoldLock[i];
            SilverLock[i] = ldata.sSilverLock[i];

            //Place
            ProgressNeeded[i] = ldata.sProgressNeeded[i];
            EnergyNeeded[i] = ldata.sEnergyNeeded[i];
            MinExpOngoing[i] = ldata.sMinExpOngoing[i];
            MaxExpOngoing[i] = ldata.sMaxExpOngoing[i];
            MinGoldOngoing[i] = ldata.sMinGoldOngoing[i];
            MaxGoldOngoing[i] = ldata.sMaxGoldOngoing[i];
            MinGoldCompleted[i] = ldata.sMinGoldCompleted[i];
            MaxGoldCompleted[i] = ldata.sMaxGoldCompleted[i];

            //Enemy
            MinExp[i] = ldata.sMinExp[i];
            MaxExp[i] = ldata.sMaxExp[i];
            MinGold[i] = ldata.sMinGold[i];
            MaxGold[i] = ldata.sMaxGold[i];
            MaxHealth[i] = ldata.sMaxHealth[i];
            EnemyAttack[i] = ldata.sEnemyAttack[i];

            //Place & Enemy
            MinSKeys[i] = ldata.sMinSKeys[i];
            MaxSKeys[i] = ldata.sMaxSKeys[i];
            MinGKeys[i] = ldata.sMinGKeys[i];
            MaxGKeys[i] = ldata.sMaxGKeys[i];

            //Shop
            if (ldata.sShopType[i] == SaveLevelData.Shops.Keys)
            {
                ShopType[i] = Shops.Keys;
            }
            if (ldata.sShopType[i] == SaveLevelData.Shops.Attack)
            {
                ShopType[i] = Shops.Attack;
            }
            if (ldata.sShopType[i] == SaveLevelData.Shops.Defense)
            {
                ShopType[i] = Shops.Defense;
            }

            //Treasure
            Active = ldata.sActive;
            Free = ldata.sFree;
            FreeBought = ldata.sFreeBought;
            Method = ldata.sMethod;

            //Shop & Jackpot & Treasure
            Cost[i] = ldata.sCost[i];
            Value[i] = ldata.sValue[i];
        }
        Debug.Log("Loaded Succesfully.");
    }

    public void SendData()
    {
        foreach (GameObject o in targetplace)
        {
            o.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            o.GetComponent<RectTransform>().anchoredPosition = new Vector2(rectX[i], rectY[i]);
            o.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth[i], rectHeight[i]);
            if (o.GetComponent<Place>())
            {
                o.GetComponent<Place>().SilverLock = SilverLock[i];
                o.GetComponent<Place>().GoldLock = GoldLock[i];

                o.GetComponent<Place>().ProgressNeeded = ProgressNeeded[i];
                o.GetComponent<Place>().EnergyNeeded = EnergyNeeded[i];

                o.GetComponent<Place>().MinExpOngoing = MinExpOngoing[i];
                o.GetComponent<Place>().MaxExpOngoing = MaxExpOngoing[i];

                o.GetComponent<Place>().MinGoldOngoing = MinGoldOngoing[i];
                o.GetComponent<Place>().MaxGoldOngoing = MaxGoldOngoing[i];

                o.GetComponent<Place>().MinGoldCompleted = MaxGoldCompleted[i];
                o.GetComponent<Place>().MaxGoldCompleted = MaxGoldCompleted[i];
                Debug.Log("Place Loaded");
            }
            if (o.GetComponent<Enemy>())
            {
                o.GetComponent<Enemy>().MaxHealth = MaxHealth[i];
                o.GetComponent<Enemy>().EnemyAttack = EnemyAttack[i];

                o.GetComponent<Enemy>().MinExp = MinExp[i];
                o.GetComponent<Enemy>().MaxExp = MaxExp[i];

                o.GetComponent<Enemy>().MinGold = MinGold[i];
                o.GetComponent<Enemy>().MaxGold = MaxGold[i];

                o.GetComponent<Enemy>().MinSKeys = MinSKeys[i];
                o.GetComponent<Enemy>().MaxSKeys = MaxSKeys[i];
                o.GetComponent<Enemy>().MinGKeys = MinGKeys[i];
                o.GetComponent<Enemy>().MaxGKeys = MaxGKeys[i];
                Debug.Log("Enemy Loaded");
            }
            if (o.GetComponent<Shop>())
            {
                if (ShopType[i] == Shops.Keys)
                {
                    o.GetComponent<Shop>().ShopType = Shop.ShopList.Keys;
                }
                if (ShopType[i] == Shops.Attack)
                {
                    o.GetComponent<Shop>().ShopType = Shop.ShopList.Attack;
                }
                if (ShopType[i] == Shops.Defense)
                {
                    o.GetComponent<Shop>().ShopType = Shop.ShopList.Defense;
                }
                o.GetComponent<Shop>().Cost = Cost[i];
                o.GetComponent<Shop>().Value = Value[i];
                Debug.Log("Shop Loaded");
            }
            if (o.GetComponent<Jackpot>())
            {
                o.GetComponent<Jackpot>().Cost = Cost[i];
                Debug.Log("Jackpot Loaded");
            }
            if (o.GetComponent<Treasure>())
            {
                o.GetComponent<Treasure>().Active = Active[i];
                o.GetComponent<Treasure>().GoldLock = GoldLock[i];
                o.GetComponent<Treasure>().Method = Method[i];
                o.GetComponent<Treasure>().Cost = Cost[i];
                o.GetComponent<Treasure>().Value = Value[i];
                o.GetComponent<Treasure>().Free = Free[i];
                o.GetComponent<Treasure>().FreeBought = FreeBought[i];
                Debug.Log("Treasure Loaded");
            }
            i++;
        }
        i = 0;
    }

    public void GetPlacesButton()
    {
        StartCoroutine(GetPlaces());
    }

    public void SavePlacesButton()
    {
        SaveLoadSystem.SaveLevel(this);
    }

    public void LoadPlacesButton(string levelName)
    {
        StartCoroutine(LoadPlaces(levelName));
    }
}
