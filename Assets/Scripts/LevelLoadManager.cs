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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Load());
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            Level.SendData();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            Level.GetInstPlaces();
        }
    }

    public IEnumerator Load()
    {
        Level.LoadPlacesButton(GlobalData.GD.levelString);
        yield return new WaitForSeconds(1f);
        for(int i = 0; i <= Level.rectX.Length -1; i++)
        {
            if(Level.PlaceType[i] == LevelData.Type.Place)
            {
                GameObject go = Instantiate(Place) as GameObject;
                go.name = "Place"+i;
                go.transform.SetParent(Container.transform);
            }
            if (Level.PlaceType[i] == LevelData.Type.Enemy)
            {
                GameObject go = Instantiate(Enemy) as GameObject;
                go.name = "Enemy"+i;
                go.transform.SetParent(Container.transform);
            }
            if (Level.PlaceType[i] == LevelData.Type.Shop)
            {
                GameObject go = Instantiate(Shop) as GameObject;
                go.name = "Shop"+i;
                go.transform.SetParent(Container.transform);
            }
            if (Level.PlaceType[i] == LevelData.Type.Jackpot)
            {
                GameObject go = Instantiate(Jackpot) as GameObject;
                go.name = "Jackpot"+i;
                go.transform.SetParent(Container.transform);
            }
            if (Level.PlaceType[i] == LevelData.Type.Treasure)
            {
                GameObject go = Instantiate(Treasure) as GameObject;
                go.name = "Treasure" + i;
                go.transform.SetParent(Container.transform);
            }
        }
        yield return new WaitForSeconds(1f);
        Level.GetInstPlaces();
        yield return new WaitForSeconds(1f);
        Level.SendData();
        yield return new WaitForSeconds(0.5f);
        LoadPanel.SetActive(false);
        GoalChecker.GC.GetPlaces();
        OL.Disable = false;
    }
}
