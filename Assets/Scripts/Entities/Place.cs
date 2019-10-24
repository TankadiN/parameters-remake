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
    [Header("Experience & Gold Ongoing")]
    public float MinExpOngoing;
    public float MaxExpOngoing;
    [Space(10)]
    public float MinGoldOngoing;
    public float MaxGoldOngoing;
    [Header("Gold Completed")]
    public float MinGoldCompleted;
    public float MaxGoldCompleted;

    void Start()
    {
        ProgressBar.color = new Color32(255, 0, 255, 255);
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
        PLR = GameObject.Find("GameManager").GetComponent<Player>();
    }

    void Update()
    {
        float calcPercent = CurrentProgress * 100 / ProgressNeeded;
        float calcBarPercent = CurrentProgress / ProgressNeeded;
        string Percent = calcPercent.ToString("0");
        PercentText.text = Percent + "%";
        ProgressBar.fillAmount = calcBarPercent;
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
                        float GOLD_GAIN = Mathf.Round(Random.Range(MinGoldCompleted, MaxGoldCompleted));
                        PLR.Money += GOLD_GAIN;
                        OL.AddLog("<color=#FFFF00>Got " + GOLD_GAIN + "$ !</color>");
                    }
                    else
                    {
                        float EXP_GAIN = Mathf.Round(Random.Range(MinExpOngoing, MaxExpOngoing));
                        float GOLD_GAIN = Mathf.Round(Random.Range(MinGoldOngoing, MaxGoldOngoing));
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
