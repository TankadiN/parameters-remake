using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Diagnostics;

public class DevTools : MonoBehaviour
{
    [Header("DiscordRP")]
    public TMP_InputField customDetail;
    public TMP_InputField customState;
    public TMP_InputField customImageTooltip;
    public TMP_InputField customButtonLabel1;
    public TMP_InputField customButtonUrl1;
    public TMP_InputField customButtonLabel2;
    public TMP_InputField customButtonUrl2;

    void Start()
    {
        
    }

    public void UseCustom(bool state)
    {
        
    }

    public void SetCustomPresence()
    {
        
    }

    public void ForceCrash()
    {
        Utils.ForceCrash(ForcedCrashCategory.FatalError);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
