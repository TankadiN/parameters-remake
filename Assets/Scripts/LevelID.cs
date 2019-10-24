using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelID : MonoBehaviour
{
    public string LevelName;

    public void EnterLevel()
    {
        GlobalData.GD.levelString = LevelName;
        SceneManager.LoadScene(1);
    }
}
