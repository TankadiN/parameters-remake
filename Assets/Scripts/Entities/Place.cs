using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Place : MonoBehaviour
{
    public Image ProgressBar;
    public TMP_Text PercentText;
    public Image Lock;
    public bool Completed;

    private OutputLog OL;
    private Player PLR;
    [Header("Variables")]
    public bool SilverLock;
    public Color SilverColor;
    public bool GoldLock;
    public Color GoldColor;
    public float CurrentProgress;
    public float ProgressNeeded;
    public float EnergyNeeded;
    public float DisplayPercent;
    [Header("Experience & Gold Ongoing")]
    public string ExpOngoing;
    [Space(10)]
    public string GoldOngoing;
    [Header("Gold Completed")]
    public string GoldCompleted;
    [Header("Settings")]
    public float lerpTime;

    private float minExpOng, maxExpOng;
    private float minGoldOng, maxGoldOng;
    private float minGoldComp, maxGoldComp;

    void Start()
    {
        ProgressBar.color = new Color32(255, 0, 255, 255);
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
        PLR = GameObject.Find("GameManager").GetComponent<Player>();

        string[] splitArrExpOng = ExpOngoing.Split(char.Parse("/"));
        minExpOng = float.Parse(splitArrExpOng[0]);
        maxExpOng = float.Parse(splitArrExpOng[1]);

        string[] splitArrGoldOng = GoldOngoing.Split(char.Parse("/"));
        minGoldOng = float.Parse(splitArrGoldOng[0]);
        maxGoldOng = float.Parse(splitArrGoldOng[1]);

        string[] splitArrGoldComp = GoldCompleted.Split(char.Parse("/"));
        minGoldComp = float.Parse(splitArrGoldComp[0]);
        maxGoldComp = float.Parse(splitArrGoldComp[1]);
    }

    void Update()
    {
        float calcPercent = CurrentProgress * 100 / ProgressNeeded;
        float calcBarPercent = CurrentProgress / ProgressNeeded;
        DisplayPercent = Mathf.Lerp(DisplayPercent, calcPercent, lerpTime * Time.deltaTime);
        PercentText.text = DisplayPercent.ToString("0") + "%";
        ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, calcBarPercent, lerpTime * Time.deltaTime);
        if(CurrentProgress == ProgressNeeded)
        {
            ProgressBar.color = new Color32(140, 63, 0, 255);
        }
        if (SilverLock == true)
        {
            Lock.color = SilverColor;
        }
        if (GoldLock == true)
        {
            Lock.color = GoldColor;
        }
        if(SilverLock == false && GoldLock == false)
        {
            Lock.color = new Color32(255, 255, 255, 0);
        }
    }

    public void AddProgress()
    {
        if (this.enabled)
        {
            if (!SilverLock && !GoldLock)
            {
                if (PLR.CurrentEnergy > EnergyNeeded)
                {
                    if (Completed)
                    {
                        float GOLD_GAIN = Mathf.Round(Random.Range(minGoldComp, maxGoldComp));
                        PLR.Money += GOLD_GAIN;
                        OL.AddLog("<color=#FFFF00>Got " + GOLD_GAIN + "$ !</color>");
                    }
                    else
                    {
                        float EXP_GAIN = Mathf.Round(Random.Range(minExpOng, maxExpOng));
                        float GOLD_GAIN = Mathf.Round(Random.Range(minGoldOng, maxGoldOng));
                        CurrentProgress++;
                        PLR.CurrentExperience += EXP_GAIN;
                        PLR.Money += GOLD_GAIN;
                        OL.AddLog("Run>Success - <color=#00FF00>+" + EXP_GAIN + " EXP</color>, <color=#FFFF00>+" + GOLD_GAIN + " $</color>");
                        if (CurrentProgress == ProgressNeeded)
                        {
                            OL.AddLog("Mission completed! <color=#FF0000>+1 Upgrade Point</color>");
                            Completed = true;
                            PLR.UpgradePoints++;
                        }
                    }
                    PLR.CurrentEnergy -= EnergyNeeded;
                    PLR.AddCOMBO();
                }
                else
                {
                    OL.AddLog("<color=#808080>Run>Attempted - Not enough Energy</color>");
                }
            }
            if (SilverLock)
            {
                if (PLR.SilverKeys > 0)
                {
                    SilverLock = false;
                    PLR.SilverKeys--;
                    OL.AddLog("Mission Unlocked");
                }
                else
                {
                    OL.AddLog("<color=#808080>I need Silver Key</color>");
                }
            }
            if (GoldLock)
            {
                if (PLR.GoldenKeys > 0)
                {
                    GoldLock = false;
                    PLR.GoldenKeys--;
                    OL.AddLog("Mission Unlocked");
                }
                else
                {
                    OL.AddLog("<color=#808080>I need Golden Key</color>");
                }
            }
            GoalChecker.GC.CheckWin();
        }
        else
        {

        }
    }
}
