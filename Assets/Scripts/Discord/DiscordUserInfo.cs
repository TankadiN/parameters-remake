using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscordUserInfo : MonoBehaviour
{
    public Toggle userToggle;

    public Sprite initialized;
    public Sprite deinitialized;

    public Image discordStatus;
    public TMP_Text username;
    public TMP_Text userID;

    private void Update()
    {
        discordStatus.sprite = DiscordController.instance.initialized ? initialized : deinitialized;
    }

    public void UpdateText()
    {
        if (DiscordController.instance.initialized)
        {
            if(DiscordController.instance.discriminator == "0")
            {
                username.text = userToggle.isOn ? "[HIDDEN]#0000" : DiscordController.instance.username;
            }
            else
            {
                username.text = userToggle.isOn ? "[HIDDEN]#0000" : DiscordController.instance.username +"#"+ DiscordController.instance.discriminator;
            }
        }
        else
        {
            username.text = userToggle.isOn ? "[HIDDEN]#0000" : "null";
        }
        userID.text = userToggle.isOn ? "ID: [HIDDEN]" : "ID: " + DiscordController.instance.id;
    }
}
