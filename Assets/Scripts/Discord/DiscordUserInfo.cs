using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscordUserInfo : MonoBehaviour
{
    public Toggle userToggle;

    public RawImage avatarImage;

    public Sprite initialized;
    public Sprite deinitialized;

    public Image discordStatus;
    public TMP_Text username;
    public TMP_Text userID;

    private void Update()
    {
        //discordStatus.sprite = DiscordManager.current.isInitialized ? initialized : deinitialized;
    }

    public void UpdateText()
    {
        /*avatarImage.texture = userToggle.isOn ? null : DiscordManager.current.CurrentUser.avatar;
        if (DiscordManager.current.isInitialized)
        {
            username.text = userToggle.isOn ? "[HIDDEN]#0000" : DiscordManager.current.CurrentUser.username + DiscordManager.current.CurrentUser.discrim;
        }
        else
        {
            username.text = userToggle.isOn ? "[HIDDEN]#0000" : "null";
        }
        userID.text = userToggle.isOn ? "ID: [HIDDEN]" : "ID: " + DiscordManager.current.CurrentUser.ID.ToString();*/
    }
}
