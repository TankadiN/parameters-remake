using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionControl : MonoBehaviour
{
    public TMP_Text GameVersionText;
    public TMP_Text UnityVersionText;

    void Start()
    {
        Debug.Log("Version: " + Application.version);
        Debug.Log("Unity: " + Application.unityVersion);
        GameVersionText.text = "v." + Application.version;
        UnityVersionText.text = "Unity " + Application.unityVersion + " Personal";
    }
}
