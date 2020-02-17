using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomModes : MonoBehaviour
{
    public static CustomModes CM;

    public bool isTimeAttack;

    private void Awake()
    {
        CM = this;
    }

    public void enableTimeAttack(bool state)
    {
        isTimeAttack = state;
    }
}
