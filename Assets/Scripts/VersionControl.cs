using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class VersionControl : MonoBehaviour
{
    public string versionForeText;
    public string versionBackText;
    public TMP_Text GameVersionText;
    public TMP_Text UnityVersionText;
    public TMP_Text TesterDataText;
    public GameObject TesterDataPanel;
    public bool useTesterData;

    void Start()
    {
        Debug.Log("Version: " + Application.version);
        Debug.Log("Unity: " + Application.unityVersion);
        GameVersionText.text = versionForeText + " v." + Application.version + " " + versionBackText;
        UnityVersionText.text = "Unity " + Application.unityVersion + " Personal";
        if (useTesterData)
        {
            TesterDataPanel.SetActive(true);
            Debug.Log("Machine Name: " + Environment.MachineName);
            Debug.Log("OS Version: " + Environment.OSVersion + " / " + SystemInfo.operatingSystem);
            Debug.Log("User Name: " + Environment.UserName);
            Debug.Log("Graphics Card: " + SystemInfo.graphicsDeviceName);
            Debug.Log("Proccessor: " + SystemInfo.processorType);
            Debug.Log("Proccessor Threads: " + SystemInfo.processorCount);
            TesterDataText.text =
                "Testing Build, DO NOT SHARE\n" +
                "User Name :" + Environment.UserName + "\n" +
                "Machine Name: " + Environment.MachineName + "\n" +
                "OS Version: " + Environment.OSVersion + "\n" +
                SystemInfo.operatingSystem + "\n" +
                "Graphics Card: " + SystemInfo.graphicsDeviceName + "\n" +
                "Proccessor: " + SystemInfo.processorType + "\n" +
                "Proccessor Threads: " + SystemInfo.processorCount;
        }
    }


}
