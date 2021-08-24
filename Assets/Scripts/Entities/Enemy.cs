﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public Image HealthBar;
    public Image HealthBackground;
    public TMP_Text HealthText;
    public Image Lock;
    public Image BossIcon;

    private OutputLog OL;
    private Player PLR;
    [Header("Variables")]
    public bool Boss;
    public bool Dead;
    public bool Recover;
    public bool SilverLock;
    public Color SilverColor;
    public bool GoldLock;
    public Color GoldColor;
    public float CurrentHealth;
    public float MaxHealth;
    public float EnemyAttack;
    [Header("Experience & Gold & Keys")]
    public float MinExp;
    public float MaxExp;
    public string Exp;
    [Space(10)]
    public float MinGold;
    public float MaxGold;
    public string Gold;
    [Space(10)]
    public float MinSKeys;
    public float MaxSKeys;
    public string SKeys;
    [Space(10)]
    public float MinGKeys;
    public float MaxGKeys;
    public string GKeys;


    private float minExp;
    private float maxExp;

    private float minGold;
    private float maxGold;

    private float minSKeys;
    private float maxSKeys;

    private float minGKeys;
    private float maxGKeys;

    private Color BossColor = new Color(128, 0, 0, 255);

    void Start()
    {
        HealthBar.color = new Color32(255, 190, 0, 255);
        CurrentHealth = MaxHealth;
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
        PLR = GameObject.Find("GameManager").GetComponent<Player>();

        //Exp = MinExp + "/" + MaxExp;
        //Gold = MinGold + "/" + MaxGold;
        //SKeys = MinSKeys + "/" + MaxSKeys;
        //GKeys = minGKeys + "/" + MaxGKeys;

        string[] splitArrExp = Exp.Split(char.Parse("/"));
        minExp = float.Parse(splitArrExp[0]);
        maxExp = float.Parse(splitArrExp[1]);

        string[] splitArrGold = Gold.Split(char.Parse("/"));
        minGold = float.Parse(splitArrGold[0]);
        maxGold = float.Parse(splitArrGold[1]);

        string[] splitArrSKeys = SKeys.Split(char.Parse("/"));
        minSKeys = float.Parse(splitArrSKeys[0]);
        maxSKeys = float.Parse(splitArrSKeys[1]);

        string[] splitArrGKeys = GKeys.Split(char.Parse("/"));
        minGKeys = float.Parse(splitArrGKeys[0]);
        maxGKeys = float.Parse(splitArrGKeys[1]);
    }

    void Update()
    {
        float calcHealthBar = CurrentHealth / MaxHealth;
        HealthText.text = CurrentHealth.ToString("0") + "/" + MaxHealth.ToString("0");
        HealthBar.fillAmount = calcHealthBar;
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (Recover)
        {
            if(CurrentHealth == MaxHealth)
            {

            }
            else
            {
                CurrentHealth += Time.deltaTime;
            }
        }
        if(Dead)
        {
            HealthBackground.color = new Color32(90, 90, 90, 255);
            gameObject.GetComponent<Button>().interactable = false;
            Recover = false;
            CurrentHealth = 0;
        }
        if (SilverLock == true)
        {
            Lock.color = SilverColor;
        }
        if (GoldLock == true)
        {
            Lock.color = GoldColor;
        }
        if (SilverLock == false && GoldLock == false)
        {
            Lock.color = new Color32(255, 255, 255, 0);
        }
        if (Boss)
        {
            BossIcon.color = BossColor;
        }
        if (!Boss)
        {
            BossIcon.color = new Color32(255, 255, 255, 0);
        }
    }

    public void Attack()
    {
        if (!SilverLock && !GoldLock)
        {
            float PLAYER_ATTACK = Mathf.Round(PLR.CurrentAttack / 2);
            CurrentHealth -= PLAYER_ATTACK;
            OL.AddLog("<color=#FF00FF>Attack! - " + PLAYER_ATTACK + " damage to the enemy!</color>");
            PLR.AddCOMBO();
            float DEF_Multiplier = Mathf.Round(PLR.CurrentDefense / 2);
            float ENEMY_ATTACK = EnemyAttack - DEF_Multiplier;
            if(ENEMY_ATTACK < 0)
            {
                ENEMY_ATTACK = 0;
            }
            PLR.CurrentHealth -= ENEMY_ATTACK;
            OL.AddLog("<color=#FF0000>" + ENEMY_ATTACK + " damage taken!</color>");
            if (CurrentHealth < 0)
            {
                float EXP_GAIN = Mathf.Round(Random.Range(MinExp, MaxExp));
                float GOLD_GAIN = Mathf.Round(Random.Range(MinGold, MaxGold));
                float SKEYS_GAIN = Mathf.Round(Random.Range(MinSKeys, MaxSKeys));
                float GKEYS_GAIN = Mathf.Round(Random.Range(MinGKeys, MaxGKeys));
                Dead = true;
                PLR.CurrentExperience += EXP_GAIN;
                PLR.Money += GOLD_GAIN;
                PLR.SilverKeys += SKEYS_GAIN;
                PLR.GoldenKeys += GKEYS_GAIN;
                PLR.UpgradePoints++;
                OL.AddLog("Mission completed! <color=#00FF00>+" + EXP_GAIN + " EXP</color>, <color=#FFFF00>+" + GOLD_GAIN + " $</color>, <color=#BEBEBE>+" + SKEYS_GAIN + " Silver Keys</color>, <color=#FFC300>+" + GKEYS_GAIN + " Golden Keys</color>, <color=#FF0000>+1 Upgrade Point</color>");
            }
        }
        if (SilverLock)
        {
            if (PLR.SilverKeys > 0)
            {
                SilverLock = false;
                PLR.SilverKeys--;
                OL.AddLog("Enemy Unlocked");
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
                OL.AddLog("Enemy Unlocked");
            }
            else
            {
                OL.AddLog("<color=#808080>I need Golden Key</color>");
            }
        }
        GoalChecker.GC.CheckWin();
    }
}
