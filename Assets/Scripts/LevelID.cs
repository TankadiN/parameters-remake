using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelID : MonoBehaviour
{
    public string levelName;
    public int tableID;
    public int Minutes;
    public int Seconds;

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
