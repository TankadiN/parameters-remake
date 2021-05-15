using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordController : MonoBehaviour
{
    public static DiscordController instance;

    public DiscordPresence presence;

    public DiscordPresence emptyPresence;

    public bool discordRP;

    public long cur_time;

    void Awake()
    {
        DDOL();
    }

    void DDOL()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            cur_time = (long)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    private void Start()
    {

    }

    public void SetRichPresence(string newDetails, string newState)
    {
        presence.details = newDetails;
        presence.state = newState;
        presence.startTime = cur_time;
        presence.endTime = 0;
        presence.largeAsset = new DiscordAsset()
        {
            image = "game_icon",
            tooltip = "Developed by WolfTeam Studios"
        };
        presence.buttons.Clear();
        presence.buttons.Add(new DiscordButton()
        {
            label = "Funny Video",
            url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
        });
        presence.buttons.Add(new DiscordButton()
        {
            label = "Look At This!",
            url = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/14782447523043.588bc13528650.jpg"
        });

        UpdatePresence();
    }

    public void UpdatePresence()
    {
        if (discordRP)
        {
           DiscordManager.current.SetPresence(presence);
        }
        else
        {
            DiscordManager.current.SetPresence(emptyPresence);
        }
    }

    public void DisableDiscordRichPresence()
    {
        if(discordRP)
        {
            discordRP = false;
        }
        else
        {
            discordRP = true;
        }

        UpdatePresence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
