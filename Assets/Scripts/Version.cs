using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Version : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Version: " + Application.version);
        GetComponent<TMP_Text>().text = "v." + Application.version;
    }
}
