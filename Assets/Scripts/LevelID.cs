using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelID : MonoBehaviour
{
    public Image Lock;
    public string levelName;
    public bool unlocked;
    public bool available;
    public int ID;
    public int tableID;
    public int Minutes;
    public int Seconds;

    void Update()
    {
        if(unlocked)
        {
            Lock.color = new Color(255, 255, 255, 0);
        }
        else
        {
            Lock.color = new Color(255, 255, 255, 255);
        }
    }

    public void CheckLevel()
    {
        if (GlobalLevels.GL.Levels[ID] == true)
        {
            unlocked = true;
            if (available)
            {
                GetComponent<Button>().interactable = true;
            }
            else
            {
                GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            unlocked = false;
            GetComponent<Button>().interactable = false;

        }
    }

    public void EnterLevel()
    {
        GlobalData.GD.levelString = levelName;
        GlobalData.GD.tableID = tableID;
        if(CustomModes.CM.isTimeAttack)
        {
            GlobalData.GD.minutes = Minutes;
            GlobalData.GD.seconds = Seconds;
            GlobalData.GD.isTimeAttack = true;
        }
        SceneManager.LoadScene(1);
    }
}
