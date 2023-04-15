using UnityEngine;
using System;

public class DiscordController : MonoBehaviour
{
    public static DiscordController instance;

    public long appID;

    public bool discordRP;

    public bool customRP;

    public long cur_time;

    public bool initialized;

    public Discord.Discord discord;

    public Discord.ActivityManager activityMgr;

    public Discord.UserManager userMgr;

    public string username, discriminator, id;

    void Awake()
    {
        DDOL();
        initialized = InitializeDiscord();
    }

    void DDOL()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            cur_time = (long)(DateTime.UtcNow - epochStart).TotalSeconds;
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

    public bool InitializeDiscord()
    {
        try
        {
            discord = new Discord.Discord(appID, (ulong)Discord.CreateFlags.NoRequireDiscord);
        } 
        catch (Discord.ResultException res)
        {
            if (res.Result != Discord.Result.Ok)
            {
                Debug.LogWarning("Can't initialize discord: " + res);
                return false;
            }
        }
        catch (NullReferenceException nullRef)
        {
            Debug.LogWarning("Can't initialize discord: " + nullRef);
            return false;
        }

        try
        {
            discord.SetLogHook(Discord.LogLevel.Debug, (level, message) =>
            {
                Debug.Log("Log[" + level + "] " + message);
            });
        }
        catch (NullReferenceException nullRef)
        {
            Debug.LogWarning("Can't set loghook: " + nullRef);
            return false;
        }

        activityMgr = discord.GetActivityManager();

        userMgr = discord.GetUserManager();

        GetUserInfo();

        return true;
    }

    public bool PingDiscord()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch (Discord.ResultException res)
        {
            if (res.Result != Discord.Result.Ok)
            {
                Debug.LogWarning("Error Discord: " + res);
                return false;
            }
        }
        catch (NullReferenceException nullRef)
        {
            Debug.LogWarning("Discord not initialized and hooked: " + nullRef);
            return false;
        }

        return true;
    }

    public void SetRichPresence(string newDetails, string newState)
    {
        Discord.Activity presence = new Discord.Activity
        {
            State = newState,
            Details = newDetails,
            Timestamps =
            {
                Start = cur_time,
                End = 0,
            },
            Assets =
            {
                LargeImage = "game_icon",
                LargeText = "Developed by WolfTeam Studios",
            },
            Instance = true,
        };

        activityMgr.UpdateActivity(presence, (result) =>
        {
            if(result != Discord.Result.Ok)
            {
                Debug.Log("Update activity failed: " + result);
            }
            Debug.Log("Update activity " + result);
        });
    }

    public void GetUserInfo()
    {
        userMgr.OnCurrentUserUpdate += () =>
        {
            Discord.User currentUser = userMgr.GetCurrentUser();
            username = currentUser.Username;
            discriminator = currentUser.Discriminator;
            id = currentUser.Id.ToString();
        };
    }

    public void SwitchPresence()
    {
        if (discordRP)
        {
            SetRichPresence("In Main Menu", "Just Vibing...");
        }
        else
        {
            activityMgr.ClearActivity((result) =>
            {
                if (result == Discord.Result.Ok)
                {
                    Debug.Log("Clear activity " + result);
                }
                else
                {
                    Debug.Log("Clear activity " + result);
                };
            });
        }
    }

    public void Update()
    {
        if (initialized)
        {
            initialized = PingDiscord();
        }
    }

    public void ReinitDiscord()
    {
        if (initialized)
        {

        }
        else
        {
            initialized = InitializeDiscord();
        }
    }

    public void OnApplicationQuit()
    {
        discord.Dispose();
    }
}
