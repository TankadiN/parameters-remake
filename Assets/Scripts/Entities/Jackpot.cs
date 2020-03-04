using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jackpot : MonoBehaviour
{
    [Header("Gameobjects")]
    public TMP_Text A_Text;
    public TMP_Text B_Text;
    public TMP_Text C_Text;
    public TMP_Text Cost_Text;
    public Image Background;
    [Header("Logic")]
    public string A;
    public string B;
    public string C;
    public string[] Symbols;
    [Header("Other")]
    public bool Active;
    public float Cost;
    [Header("Unlock Conditions")]
    public Player.Condition condition;
    public int conditionValue;

    int rolledA;
    int rolledB;
    int rolledC;

    bool Spinning;
    bool SpinA;
    bool SpinB;
    bool SpinC;

    private OutputLog OL;
    private Player PLR;

    void Start()
    {
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
        PLR = GameObject.Find("GameManager").GetComponent<Player>();
    }

    void Update()
    {
        if(Active)
        {
            Background.color = new Color32(255, 128, 0, 255);
            A_Text.color = new Color32(255, 255, 255, 255);
            B_Text.color = new Color32(255, 255, 255, 255);
            C_Text.color = new Color32(255, 255, 255, 255);
            Cost_Text.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            Background.color = new Color32(0, 0, 0, 255);
            A_Text.color = new Color32(255, 255, 255, 0);
            B_Text.color = new Color32(255, 255, 255, 0);
            C_Text.color = new Color32(255, 255, 255, 0);
            Cost_Text.color = new Color32(255, 255, 255, 0);
        }

        Cost_Text.text = "$" + Cost.ToString("0");

        A_Text.text = A;
        B_Text.text = B;
        C_Text.text = C;

        A = Symbols[rolledA];
        B = Symbols[rolledB];
        C = Symbols[rolledC];

        if(SpinA)
        {
            rolledA = Random.Range(0, Symbols.Length);
        }
        if(SpinB)
        {
            rolledB = Random.Range(0, Symbols.Length);
        }
        if(SpinC)
        {
            rolledC = Random.Range(0, Symbols.Length);
        }

        if(condition == Player.Condition.Combo)
        {
            if(PLR.CurrentCombo >= conditionValue)
            {
                Active = true;
            }
        }
        if(condition == Player.Condition.PlacesCompleted)
        {
            if(GoalChecker.GC.placeCount >= conditionValue)
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
        if (condition == Player.Condition.Level)
        {
            if (PLR.Level >= conditionValue)
            {
                Active = true;
            }
        }
    }

    public void Spinner()
    {
        if (Active)
        {
            if (Spinning)
            {
                if (!SpinA && !SpinB && SpinC)
                {
                    SpinC = false;
                    CheckJackpot();
                    Spinning = false;
                }
                if (!SpinA && SpinB)
                {
                    SpinB = false;
                }
                if (SpinA)
                {
                    SpinA = false;
                }
            }
            else
            {
                if (PLR.Money >= Cost)
                {
                    PLR.Money -= Cost;
                    OL.AddLog("Paid " + Cost + "$ to the jackpot");
                    SpinA = true;
                    SpinB = true;
                    SpinC = true;
                    Spinning = true;
                }
                else
                {
                    OL.AddLog("<color=#808080>Not enough money</color>");
                }
            }
        }
        else
        {

        }
    }

    public void CheckJackpot()
    {
        if (rolledA == 0 && rolledB == 0 && rolledC == 0)
        {
            Debug.Log("Nothing");
            OL.AddLog("Nothing Happened...");
        }
        if (rolledA == 1 && rolledB == 1 && rolledC == 1)
        {
            Debug.Log("Money");
            OL.AddLog("<color=#FFFF00>Gained 200$ x 5</color>");
            PLR.Money += 200 * 5;
        }
        if (rolledA == 2 && rolledB == 2 && rolledC == 2)
        {
            Debug.Log("777");
            OL.AddLog("<color=#FFFF00>JACKPOT! Gained 777$ x 7</color>");
            PLR.Money += 777 * 7; 
        }
        if (rolledA == 3 && rolledB == 3 && rolledC == 3)
        {
            Debug.Log("EXP");
            OL.AddLog("<color=#00FF00>Gained 20 x 5 EXP</color>");
            PLR.CurrentExperience += 20 * 5;
        }
    }
}
