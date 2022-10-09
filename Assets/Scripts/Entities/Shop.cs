using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    private OutputLog OL;
    private Player PLR;
    [Header("GameObjects")]
    public Image BackgroundGameobject;
    public TMP_Text CostTextGameobject;
    public TMP_Text CountTextGameobject;
    [Space(10)]
    public GameObject KeyGameobject;
    public GameObject SwordGameobject;
    public GameObject ShieldGameobject;
    public enum ShopList {Keys, Attack, Defense};
    public ShopList ShopType;
    [Header("Variables")]
    public float Cost;
    public float ItemCount;
    public float Value;

    void Start()
    {
        OL = GameObject.Find("GameManager").GetComponent<OutputLog>();
        PLR = GameObject.Find("GameManager").GetComponent<Player>();
        KeyGameobject.SetActive(false);
        SwordGameobject.SetActive(false);
        ShieldGameobject.SetActive(false);
        if(ShopType == ShopList.Keys)
        {
            KeyGameobject.SetActive(true);
        }
        if(ShopType == ShopList.Attack)
        {
            SwordGameobject.SetActive(true);
        }
        if(ShopType == ShopList.Defense)
        {
            ShieldGameobject.SetActive(true);
        }
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        CostTextGameobject.text = "$" + Cost.ToString("0");
        if(ShopType == ShopList.Keys)
        {
            CountTextGameobject.text = "";
            BackgroundGameobject.color = new Color32(146, 195, 78, 255);
        }
        if (ShopType == ShopList.Attack || ShopType == ShopList.Defense)
        {
            CountTextGameobject.text = "x" + ItemCount;
        }
        if(ItemCount == 9)
        {
            gameObject.GetComponent<Button>().interactable = false;
            if (ShopType == ShopList.Defense)
            {
                BackgroundGameobject.color = new Color32(80, 128, 80, 255);
            }
            if(ShopType == ShopList.Attack)
            {
                BackgroundGameobject.color = new Color32(159, 0, 80, 255);
            }
        }
    }

    public void Buy()
    {
        if (PLR.Money >= Cost)
        {
            PLR.Money -= Cost;
            if (ShopType == ShopList.Keys)
            {
                PLR.SilverKeys += 1;
                OL.AddLog("Bought the key!");
            }
            if (ShopType == ShopList.Attack)
            {
                PLR.MaxAttack += Value;
                ItemCount++;
                BackgroundGameobject.color = new Color32(255, 0, 128, 255);
                OL.AddLog("<color=#FF0080>Bought Weapon > ATK. +" + Value + "</color>");
            }
            if (ShopType == ShopList.Defense)
            {
                PLR.MaxDefense += Value;
                ItemCount++;
                BackgroundGameobject.color = new Color32(80, 255, 80, 255);
                OL.AddLog("<color=#FF0080>Bought Armor > DEF. +" + Value + "</color>");
            }
            UpdateVisuals();
            PLR.ResetTimer();
        }
        else
        {
            OL.AddLog("<color=#808080>Not enough money</color>");
        }
    }
}
