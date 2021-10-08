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
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (treasureMode == TreasureType.Paid)
        {
            ChestGameobject.color = new Color32(255, 255, 255, 0);
            UpperTextGameobject.text = "$" + Cost.ToString("0");
            if (Item == ItemType.LifeUP)
            {
                LowerTextGameobject.text = "Life++";
            }
            else if (Item == ItemType.LifeRecovery)
            {
                LowerTextGameobject.text = "Life Rcv.";
            }
            else if (Item == ItemType.EnergyUP)
            {
                LowerTextGameobject.text = "Energy++";
            }
            else if (Item == ItemType.AddUpgradePoint)
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
        if (treasureMode == TreasureType.Free)
        {
            if (Active)
            {
                if(!alreadyClaimed)
                {
                    BackgroundGameobject.color = new Color32(0, 128, 255, 255);
                }
                //BackgroundGameobject.color = new Color32(0, 128, 255, 255);
                if (Item == ItemType.LifeUP)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Life";
                }
                else if (Item == ItemType.LifeRecovery)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Life Rcv.";
                }
                else if (Item == ItemType.EnergyUP)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Energy";
                }
                else if (Item == ItemType.AddUpgradePoint)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "Upgr.";
                }
                else if (Item == ItemType.Money)
                {
                    UpperTextGameobject.text = "+" + Value + " " + "$";
                }
                else if (Item == ItemType.Attack)
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
                BackgroundGameobject.color = new Color32(0, 0, 0, 255);
                LockGameobject.color = new Color32(255, 255, 255, 0);
                LowerTextGameobject.text = "";
                ChestGameobject.color = new Color32(255, 255, 255, 255);
                UpperTextGameobject.text = "?";
            }
        }
    }
    private void Update()
    {
        if (condition == Player.Condition.Combo)
        {
            if (PLR.CurrentCombo >= conditionValue)
            {
                Active = true;
                UpdateVisual();
            }
        }
        if (condition == Player.Condition.PlacesCompleted)
        {
            if (GoalChecker.GC.placeCount >= conditionValue)
            {
                Active = true;
                UpdateVisual();
            }
        }
        if (condition == Player.Condition.EnemiesDefeated)
        {
            if (GoalChecker.GC.enemyCount >= conditionValue)
            {
                Active = true;
                UpdateVisual();
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
                    else if (Item == ItemType.LifeRecovery)
                    {
                        PLR.Money -= Cost;
                        PLR.CurrentHealth = PLR.MaxHealth;
                    }
                    else if (Item == ItemType.EnergyUP)
                    {
                        PLR.Money -= Cost;
                        PLR.MaxEnergy += Value;
                        Cost += 5;
                    }
                    else if (Item == ItemType.AddUpgradePoint)
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
            if (Active)
            {
                if (!alreadyClaimed)
                {
                    switch (Item)
                    {
                        case ItemType.LifeUP:
                            PLR.MaxHealth += Value;
                            break;

                        case ItemType.LifeRecovery:
                            PLR.CurrentHealth = PLR.MaxHealth;
                            break;

                        case ItemType.EnergyUP:
                            PLR.MaxEnergy += Value;
                            break;

                        case ItemType.AddUpgradePoint:
                            PLR.UpgradePoints += Value;
                            break;

                        case ItemType.Money:
                            PLR.Money += Value;
                            break;

                        case ItemType.Attack:
                            PLR.MaxAttack += Value;
                            break;
                    }

                    alreadyClaimed = true;
                }
            }
        }
        UpdateVisual();
    }
}
