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

    public bool Active;
    public bool GoldLock;

    public string Method;
    public float Cost;
    public float Value;

    public bool Free;
    public bool FreeBought;

    void Start()
    {
        PLR = GameObject.Find("GameManager").GetComponent<Player>();
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
    }

    private void Update()
    {
        if(Active)
        {
            if(GoldLock)
            {
                BackgroundGameobject.color = new Color32(0, 0, 0, 255);
                LockGameobject.color = new Color32(255, 200, 0, 255);
            }
            if(!GoldLock)
            {
                BackgroundGameobject.color = new Color32(165, 93, 255, 255);
                LockGameobject.color = new Color32(255, 255, 255, 0);
            }
            if (Free)
            {
                UpperTextGameobject.text = "+" + Value + "" + Method;
                LowerTextGameobject.text = "";
                ChestGameobject.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                UpperTextGameobject.text = "$" + Cost.ToString("0");
                if (Method == "life")
                {
                    LowerTextGameobject.text = "Life++";
                }
                if (Method == "lifeRCV")
                {
                    LowerTextGameobject.text = "Life Rcv.";
                }
                if (Method == "energy")
                {
                    LowerTextGameobject.text = "Energy++";
                }
                if (Method == "upgrade")
                {
                    LowerTextGameobject.text = "Add Upgr.";
                }
            }
            if(FreeBought)
            {
                BackgroundGameobject.color = new Color32(101, 56, 156, 255);
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            BackgroundGameobject.color = new Color32(0, 0, 0, 255);
            if (Free)
            {
                UpperTextGameobject.text = "?";
                LowerTextGameobject.text = "";
                ChestGameobject.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                ChestGameobject.color = new Color32(255, 255, 255, 0);
                UpperTextGameobject.text = "$" + Cost.ToString("0");
                if (Method == "life")
                {
                    LowerTextGameobject.text = "Life++";
                }
                if (Method == "lifeRCV")
                {
                    LowerTextGameobject.text = "Life Rcv.";
                }
                if (Method == "energy")
                {
                    LowerTextGameobject.text = "Energy++";
                }
                if (Method == "upgrade")
                {
                    LowerTextGameobject.text = "Add Upgr.";
                }
            }
        }
    }

    public void OnClick()
    {
        if (Active)
        {
            if (!GoldLock)
            {
                if (PLR.Money >= Cost || Free)
                {
                    if (Method == "life")
                    {
                        PLR.Money -= Cost;
                        PLR.MaxHealth += Value;
                        Cost += 5;
                    }
                    if (Method == "lifeRCV")
                    {
                        PLR.Money -= Cost;
                        PLR.CurrentHealth = PLR.MaxHealth;
                    }
                    if (Method == "energy")
                    {
                        PLR.Money -= Cost;
                        PLR.CurrentHealth = PLR.MaxHealth;
                        Cost += 5;
                    }
                    if (Method == "upgrade")
                    {
                        PLR.Money -= Cost;
                        PLR.UpgradePoints = Value;
                    }
                    if(Free)
                    {
                        FreeBought = true;
                    }
                }
            }
            else
            {
                if(PLR.GoldenKeys > 0)
                {
                    GoldLock = false;
                    PLR.GoldenKeys -= 1;
                }
            }
        }
        else
        {

        }
    }
}
