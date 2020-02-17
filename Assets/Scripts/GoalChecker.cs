using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameJolt.API;

public class GoalChecker : MonoBehaviour
{
    public static GoalChecker GC;
    private OutputLog OL;
    private Player PLR;

    [Header("Places")]
    public bool PlacesCompleted;
    public int placeCount;
    public List<Place> Places;
    [Header("Enemies")]
    public bool EnemiesCompleted;
    public int enemyCount;
    public List<Enemy> Enemies;
    [Header("Cheat Measurements")]
    public Button SendScoreButton;
    public GameObject PlayerPanelTextWarning;
    public GameObject EndPanelTextWarning;
    public GameObject CheatsEnabledPanel;
    [Header("Other")]
    public bool FullComplete;
    public GameObject EndPanel;
    public TMP_Text TimeObject;
    public string Time;
    public int TimeScore;
    public int tableID;
    [TextArea]
    public string DebugData;

    private void Awake()
    {
        GC = this;
        OL = gameObject.GetComponent<OutputLog>();
        PLR = gameObject.GetComponent<Player>();
    }

    private void Start()
    {
        if(Console.CMD)
        {
            if (Console.CMD.EnabledCheats)
            {
                CheatsEnabledPanel.SetActive(true);
            }
        }
        tableID = GlobalData.GD.tableID;
    }

    public void GetPlaces()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Place");
        foreach(GameObject o in go)
        {
            if(o.GetComponent<Place>())
            {
                Places.Add(o.GetComponent<Place>());
            }
            if (o.GetComponent<Enemy>())
            {
                Enemies.Add(o.GetComponent<Enemy>());
            }
        }
    }

    private void Update()
    {
        TimeObject.text = Time;
        if(FullComplete)
        {
            EndPanel.SetActive(true);
        }
        if(Console.CMD)
        {
            if (Console.CMD.EnabledCheats)
            {
                SendScoreButton.interactable = false;
                PlayerPanelTextWarning.SetActive(true);
                EndPanelTextWarning.SetActive(true);
            }
        }
        /*if(GameJoltAPI.Instance && GameJoltAPI.Instance.HasSignedInUser == false)
        {
            SendScoreButton.interactable = false;
        }*/
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetPlaces();
        }
    }

    public void UploadScore()
    {
        GamejoltHandler.GJH.AddScore(TimeScore, Time, tableID, DebugData);
    }

    public void CheckWin()
    {
        placeCount = 0;
        enemyCount = 0;
        for(int i = 0; i <= Places.Count - 1; i++)
        {
            if(Places[i].Completed)
            {
                placeCount++;
            }
        }
        for (int i = 0; i <= Enemies.Count - 1; i++)
        {
            if (Enemies[i].Dead)
            {
                enemyCount++;
            }
        }
        if(placeCount == Places.Count )
        {
            PlacesCompleted = true;
        }
        if(enemyCount == Enemies.Count)
        {
            EnemiesCompleted = true;
        }
        if(PlacesCompleted && EnemiesCompleted)
        {
            FullComplete = true;
            Time = OL.Hours.ToString("00") + ":" + OL.Minutes.ToString("00") + ":" + OL.Seconds.ToString("00");
            TimeScore = OL.Hours * 3600 + OL.Minutes * 60 + OL.Seconds;
            DebugData =
                "Level: " + PLR.Level +
                ", Money: " + PLR.Money +
                ", SilverKeys: " + PLR.SilverKeys +
                ", GoldenKeys: " + PLR.GoldenKeys +
                ", Upgrades: " + PLR.UpgradePoints +
                ", Exp: " + PLR.CurrentExperience + "/" + PLR.NeededExperience +
                ", Life: " + PLR.CurrentHealth + "/" + PLR.MaxHealth +
                ", Energy: " + PLR.CurrentEnergy + "/" + PLR.MaxEnergy +
                ", Recovery: " + PLR.CurrentRecovery + "/" + PLR.MaxRecovery + 
                ", Attack: " + PLR.CurrentAttack + "/" + PLR.MaxAttack +
                ", Defense: " + PLR.CurrentDefense + "/" + PLR.MaxDefense;
        }
        Debug.Log("Places Completed: " + placeCount + "/" + Places.Count + ", Enemies Defeated: " + enemyCount + "/" + Enemies.Count + ", Quota met?: " + FullComplete);
    }
    
    public void CloseCheatPanel()
    {
        CheatsEnabledPanel.SetActive(false);
    }
}
