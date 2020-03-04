using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Level & Experience & Upgrade Points")]
    public TMP_Text LevelTextGameobject;
    public Image ExperienceImageGameobject;
    public TMP_Text ExperienceTextGameobject;
    public TMP_Text UPointsTextGameobject; 
    public float Level;
    public float NeededExperience;
    public float CurrentExperience;
    public float UpgradePoints;
    public List<Button> UpgradeButtons;
    [Header("Money & Keys")]
    public TMP_Text MoneyTextGameobject;
    public TMP_Text SKeysTextGameobject;
    public TMP_Text GKeysTextGameobject;
    public float Money;
    public float SilverKeys;
    public float GoldenKeys;
    [Header("Health")]
    public Image HealthImageGameobject;
    public TMP_Text HealthTextGameobject;
    public float MaxHealth;
    public float CurrentHealth;
    [Header("Energy")]
    public Image EnergyImageGameobject;
    public TMP_Text EnergyTextGameobject;
    public float MaxEnergy;
    public float CurrentEnergy;
    [Header("Combo")]
    public Image ComboImageGameobject;
    public TMP_Text ComboTextGameobject;
    public int CurrentCombo;
    public float MaxComboTimer;
    public float CurrentComboTimer;
    [Header("Recovery")]
    public Image RecoveryImageGameobject;
    public TMP_Text RecoveryTextGameobject;
    public float MaxRecovery;
    public float CurrentRecovery;
    public float RCVModifier;
    [Header("Attack")]
    public Image AttackImageGameobject;
    public TMP_Text AttackTextGameobject;
    public float MaxAttack;
    public float CurrentAttack;
    [Header("Defense")]
    public Image DefenseImageGameobject;
    public TMP_Text DefenseTextGameobject;
    public float MaxDefense;
    public float CurrentDefense;

    private OutputLog OL;

    public enum Condition {PlacesCompleted, EnemiesDefeated, Combo, Level}

    void Start()
    {
        OL = GetComponent<OutputLog>();
        if(AudioManager.instance)
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("LevelTheme");
        }
    }

    void Update()
    {
        //Calculate Values
        float calcEXP = CurrentExperience / NeededExperience;
        float calcHP = CurrentHealth / MaxHealth;
        float calcEN = CurrentEnergy / MaxEnergy;
        float calcCOMBO = CurrentComboTimer / MaxComboTimer;
        float calcRCV = CurrentRecovery / MaxRecovery;
        float calcATK = CurrentAttack / MaxAttack;
        float calcDEF = CurrentDefense / MaxDefense;
        //Variables
        float RCV_Multiplier = CurrentRecovery / RCVModifier;
        //Set to bars
        ExperienceImageGameobject.fillAmount = calcEXP;
        HealthImageGameobject.fillAmount = calcHP;
        EnergyImageGameobject.fillAmount = calcEN;
        ComboImageGameobject.fillAmount = calcCOMBO;
        RecoveryImageGameobject.fillAmount = calcRCV;
        AttackImageGameobject.fillAmount = calcATK;
        DefenseImageGameobject.fillAmount = calcDEF;
        //Set to texts
        LevelTextGameobject.text = "Level " + Level.ToString("0");
        ExperienceTextGameobject.text = CurrentExperience.ToString("0") + "/" + NeededExperience.ToString("0");
        HealthTextGameobject.text = CurrentHealth.ToString("0") + "/" + MaxHealth.ToString("0");
        EnergyTextGameobject.text = CurrentEnergy.ToString("0") + "/" + MaxEnergy.ToString("0");
        ComboTextGameobject.text = "x" + CurrentCombo.ToString("0");
        RecoveryTextGameobject.text = CurrentRecovery.ToString("0");
        AttackTextGameobject.text = CurrentAttack.ToString("0") + "/" + MaxAttack.ToString("0");
        DefenseTextGameobject.text = CurrentDefense.ToString("0") + "/" + MaxDefense.ToString("0");
        MoneyTextGameobject.text = "$ " + Money.ToString("0");
        SKeysTextGameobject.text = SilverKeys.ToString("0");
        GKeysTextGameobject.text = GoldenKeys.ToString("0");
        UPointsTextGameobject.text = "+" + UpgradePoints.ToString("0");
        //Time Related code
        if(CurrentEnergy <= MaxEnergy)
        {
            CurrentEnergy += Time.deltaTime * RCV_Multiplier;
        }
        if(CurrentHealth <= MaxHealth)
        {
            CurrentHealth += Time.deltaTime * RCV_Multiplier;
        }
        if (CurrentAttack <= MaxAttack)
        {
            CurrentAttack += Time.deltaTime;
        }
        if (CurrentDefense <= MaxDefense)
        {
            CurrentDefense += Time.deltaTime;
        }
        if(CurrentComboTimer >= 0)
        {
            CurrentComboTimer -= Time.deltaTime;
        }
        //Overflow restoring
        if (CurrentEnergy > MaxEnergy)
        {
            CurrentEnergy = MaxEnergy;
        }
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (CurrentAttack > MaxAttack)
        {
            CurrentAttack = MaxAttack;
        }
        if (CurrentDefense > MaxDefense)
        {
            CurrentDefense = MaxDefense;
        }
        //Gameplay Mechanics
        if(CurrentExperience > NeededExperience)
        {
            float EXP_VALUE_UP = Random.Range(250, 500);
            OL.AddLog("<color=#FF00FF>LEVEL UP!</color> <color=#FF0000>+3 Upgrade Points</color>");
            CurrentExperience = 0;
            NeededExperience += EXP_VALUE_UP;
            Level += 1;
            UpgradePoints += 3;
            CurrentHealth = MaxHealth;
            CurrentEnergy = MaxEnergy;
        }
        if (CurrentComboTimer <= 0)
        {
            CurrentCombo = 0;
            CurrentComboTimer = 0;
        }
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            CurrentAttack = 0;
            CurrentDefense = 0;
        }
        //Upgrade Mechanics
        if (UpgradePoints == 0)
        {
            for(int i = 0; i <= UpgradeButtons.Count - 1; i++)
            {
                UpgradeButtons[i].interactable = false;
            }
        }
        if (UpgradePoints > 0)
        {
            for (int i = 0; i <= UpgradeButtons.Count - 1; i++)
            {
                UpgradeButtons[i].interactable = true;
            }
        }
    }

    public void AddRCV()
    {
        if(UpgradePoints > 0)
        {
            CurrentRecovery += 1;
            UpgradePoints -= 1;
            OL.AddLog("<color=#00FFFF>Recovery Upgraded: +1</color>");
        }
        else
        {
            OL.AddLog("<color=#FF0000>ERROR: Not enough points</color>");
        }
    }
    public void AddATK()
    {
        if (UpgradePoints > 0)
        {
            float ATK_VALUE_UP = Random.Range(1, 5);
            MaxAttack += ATK_VALUE_UP;
            UpgradePoints -= 1;
            OL.AddLog("<color=#FF0000>Attack Upgraded: +" + ATK_VALUE_UP + "</color>");
        }
        else
        {
            OL.AddLog("<color=#FF0000>ERROR: Not enough points</color>");
        }
    }
    public void AddDEF()
    {
        if (UpgradePoints > 0)
        {
            float DEF_VALUE_UP = Random.Range(1, 5);
            MaxDefense += DEF_VALUE_UP;
            UpgradePoints -= 1;
            OL.AddLog("<color=#8000FF>Defense Upgraded: +" + DEF_VALUE_UP + "</color>");
        }
        else
        {
            OL.AddLog("<color=#FF0000>ERROR: Not enough points</color>");
        }
    }

    public void AddCOMBO()
    {
        CurrentCombo++;
        CurrentComboTimer = MaxComboTimer;
    }
}
