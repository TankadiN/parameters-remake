using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamejoltTrophyID : MonoBehaviour
{
    public int trophyID;

    public void Trophy()
    {
        GamejoltHandler.GJH.UnlockTrophy(trophyID);
    }
}
