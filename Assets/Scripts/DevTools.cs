using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        DiscordController.instance.customRP = state;
        DiscordController.instance.UpdatePresence();
    }

    public void SetCustomPresence()
    {
        DiscordController.instance.customPresence.details = customDetail.text;
        DiscordController.instance.customPresence.state = customState.text;
        DiscordController.instance.customPresence.endTime = 0;
        DiscordController.instance.customPresence.largeAsset = new DiscordAsset()
        {
            image = "game_icon",
            tooltip = customImageTooltip.text
        };
        DiscordController.instance.customPresence.buttons.Clear();
        if (customButtonLabel1.text != null && customButtonUrl1.text != null)
        {
            DiscordController.instance.customPresence.buttons.Add(new DiscordButton()
            {
                label = customButtonLabel1.text,
                url = customButtonUrl1.text
            });
            if (customButtonLabel2.text != null && customButtonUrl2.text != null)
            {
                DiscordController.instance.customPresence.buttons.Add(new DiscordButton()
                {
                    label = customButtonLabel2.text,
                    url = customButtonUrl2.text
                });
            }
            else
            {

            }
        }
        else
        {

        }
        DiscordController.instance.UpdatePresence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
