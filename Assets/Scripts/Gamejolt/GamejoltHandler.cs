using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.API;
using GameJolt.UI;

public class GamejoltHandler : MonoBehaviour
{
    public static GamejoltHandler GJH;

    private void Awake()
    {
        DDOL();
    }

    void DDOL()
    {
        if (GJH == null)
        {
            DontDestroyOnLoad(gameObject);
            GJH = this;
        }
        else
        {
            if (GJH != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void UnlockTrophy(int trophyID)
    {
        GameJolt.API.Trophies.Unlock(trophyID, (bool success) => {
            if (success)
            {
                Debug.Log("Success!");
            }
            else
            {
                Debug.Log("Something went wrong");
            }
        });
    }

    public void AddScore(int scoreValue, string scoreText, int tableID, string extraData)
    {
        GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => 
        {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
            if(success)
            {
                GameJoltUI.Instance.QueueNotification(string.Format("Score sent."));
            }
            else
            {
                GameJoltUI.Instance.QueueNotification(string.Format("Something went wrong."));
            }
        });
    }
}
