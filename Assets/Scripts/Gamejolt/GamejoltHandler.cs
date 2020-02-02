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
        Trophies.TryUnlock(trophyID, (TryUnlockResult success) => {
            if (success == TryUnlockResult.Unlocked)
            {
                Debug.Log("Success!");
                GameJoltUI.Instance.QueueNotification("You got a trophy!");
            }
            if(success == TryUnlockResult.AlreadyUnlocked)
            {
                Debug.Log("Trophy Already Unlocked, Notning Changed");
            }
            if(success == TryUnlockResult.Failure)
            {
                Debug.Log("Something went wrong");
                GameJoltUI.Instance.QueueNotification("An error has occured trying to award you a trophy.");
            }
        });
    }

    public void AddScore(int scoreValue, string scoreText, int tableID, string extraData)
    {
        Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => 
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
