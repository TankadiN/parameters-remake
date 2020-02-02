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
        StartCoroutine(Load());
    }

    void Update()
    {

    }

    public IEnumerator Load()
    {
        Level.ExecuteLoad(GlobalData.GD.levelString);
        yield return new WaitForSeconds(1f);
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

                go.GetComponent<Place>().MinGoldCompleted = p.MaxGoldCompleted;
                go.GetComponent<Place>().MaxGoldCompleted = p.MaxGoldCompleted;
                Debug.Log("Place Loaded");
                yield return new WaitForSeconds(0.1f);
            }
            if (p.PlaceType == PlaceInfo.Type.Enemy)
            {
                GameObject go = Instantiate(Enemy) as GameObject;
                go.name = "Enemy";
                go.transform.SetParent(Container.transform);
            }
            if (p.PlaceType == PlaceInfo.Type.Shop)
            {
                GameObject go = Instantiate(Shop) as GameObject;
                go.name = "Shop";
                go.transform.SetParent(Container.transform);
            }
            if (p.PlaceType == PlaceInfo.Type.Jackpot)
            {
                GameObject go = Instantiate(Jackpot) as GameObject;
                go.name = "Jackpot";
                go.transform.SetParent(Container.transform);
            }
            if (p.PlaceType == PlaceInfo.Type.Treasure)
            {
                GameObject go = Instantiate(Treasure) as GameObject;
                go.name = "Treasure";
                go.transform.SetParent(Container.transform);
            }
            yield return new WaitForSeconds(0.5f);
            LoadPanel.SetActive(false);
            GoalChecker.GC.GetPlaces();
            OL.Disable = false;
        }
    }
}
