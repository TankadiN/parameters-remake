using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Treasure : MonoBehaviour
{
    private Player PLR;
    private OutputLog OL;

    public TMP_Text UpperTextGameobject;
    public TMP_Text LowerTextGameobject;
    public Image BackgroundGameobject;
    public Image ChestGameobject;
    public Image LockGameobject;

    public enum TreasureType {Paid, Free}

    public TreasureType treasureMode;

    public bool Active;
    public bool GoldLock;

    public enum ItemType {LifeUP, LifeRecovery, EnergyUP, AddUpgradePoint, Money, Attack, Defense, Recovery}

    public ItemType Item;
    public float Cost;
    public float Value;

    public bool alreadyClaimed;

    [Header("Unlock Conditions")]
    public Player.Condition condition;
    public int conditionValue;

    void Start()
    {
        PLR = GameObject.Find("GameManager").GetComponent<Player>();
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
    }

    private void Update()
    {
        if (treasureMode == TreasureType.Paid)
        {
            ChestGameobject.color = new Color32(255, 255, 255, 0);
            UpperTextGameobject.text = "$" + Cost.ToString("0");
            if (Item == ItemType.LifeUP)
            {
                LowerTextGameobject.text = "Life++";
            }
            if (Item == ItemType.LifeRecovery)
            {
                LowerTextGameobject.text = "Life Rcv.";
            }
            if (Item == ItemType.EnergyUP)
            {
                LowerTextGameobject.text = "Energy++";
            }
            if (Item == ItemType.AddUpgradePoint)
            {
                LowerTextGameobject.text = "Add Upgr.";
            }
            if (GoldLock)
            {
                BackgroundGameobject.color = new Color32(0, 0, 0, 255);
                LockGameobject.color = new Color32(255, 200, 0, 255);
            }
            else
            {
                BackgroundGameobject.color = new Color32(165, 93, 255, 255);
                LockGameobject.color = new Color32(255, 255, 255, 0);
            }
        }
        if(treasureMode == TreasureType.Free)
        {
            BackgroundGameobject.color = new Color32(0, 0, 0, 255);
            LockGameobject.color = new Color32(255, 255, 255, 0);
            LowerTextGameobject.text = "";
            ChestGameobject.color = new Color32(255, 255, 255, 255);
            if (Active)
            {
                BackgroundGameobject.color = new Color32(0, 128, 255, 255);
                if (Item == ItemType.LifeUP)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Life";
                }
                if (Item == ItemType.LifeRecovery)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Life Rcv.";
                }
                if (Item == ItemType.EnergyUP)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Energy";
                }
                if (Item == ItemType.AddUpgradePoint)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Upgr.";
                }
                if (Item == ItemType.Money)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "$";
                }
                if (Item == ItemType.Attack)
                {
                    UpperTextGameobject.text = "x2 Atk.";
                }
                if (alreadyClaimed)
                {
                    BackgroundGameobject.color = new Color32(0, 128, 180, 255);
                    gameObject.GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                UpperTextGameobject.text = "?";
            }
        }

        if (condition == Player.Condition.Combo)
        {
            if (PLR.CurrentCombo >= conditionValue)
            {
                Active = true;
            }
        }
        if (condition == Player.Condition.PlacesCompleted)
        {
            if (GoalChecker.GC.placeCount >= conditionValue)
            {
                Active = true;
            }
        }
        if (condition == Player.Condition.EnemiesDefeated)
        {
            if (GoalChecker.GC.enemyCount >= conditionValue)
            {
                Active = true;
            }
        }
    }

    public void OnClick()
    {
        if(treasureMode == TreasureType.Paid)
        {
            if (!GoldLock)
            {
                if (PLR.Money >= Cost)
                {
                    if (Item == ItemType.LifeUP)
                    {
                        PLR.Money -= Cost;
                        PLR.MaxHealth += Value;
                        Cost += 5;
                    }
                    if (Item == ItemType.LifeRecovery)
                    {
                        PLR.Money -= Cost;
                        PLR.CurrentHealth = PLR.MaxHealth;
                    }
                    if (Item == ItemType.EnergyUP)
                    {
                        PLR.Money -= Cost;
                        PLR.MaxEnergy += Value;
                        Cost += 5;
                    }
                    if (Item == ItemType.AddUpgradePoint)
                    {
                        PLR.Money -= Cost;
                        PLR.UpgradePoints += Value;
                    }
                }
            }
            else
            {
                if (PLR.GoldenKeys > 0)
                {
                    GoldLock = false;
                    PLR.GoldenKeys -= 1;
                }
            }
        }

        if (treasureMode == TreasureType.Free)
        {
            if (Item == ItemType.LifeUP)
            {
                PLR.MaxHealth += Value;
            }
            if (Item == ItemType.LifeRecovery)
            {
                PLR.CurrentHealth = PLR.MaxHealth;
            }
            if (Item == ItemType.EnergyUP)
            {
                PLR.MaxEnergy += Value;
            }
            if (Item == ItemType.AddUpgradePoint)
            {
                PLR.UpgradePoints += Value;
            }
            if (Item == ItemType.Money)
            {
                PLR.Money += Value;
            }
            if (Item == ItemType.Attack)
            {
                PLR.MaxAttack += PLR.MaxAttack;
            }
            alreadyClaimed = true;
        }
    }
}
