using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    public static Console CMD;

    public TMP_InputField CMD_INPUT;
    public GameObject ConsolePanel;
    public KeyCode HoldKey;
    public KeyCode PushKey;
    public bool Unlocked;
    public bool EnabledCheats;
    public string Password;
    private Player PLAYER;
    private OutputLog OL;

    void Awake()
    {
        DDOL();
        OL = GetComponent<OutputLog>();
    }

    private void Start()
    {
        PrintFirst();
    }

    void DDOL()
    {
        if (CMD == null)
        {
            DontDestroyOnLoad(gameObject);
            CMD = this;
        }
        else
        {
            if (CMD != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKey(HoldKey) && Input.GetKeyDown(PushKey))
        {
            if(ConsolePanel.activeInHierarchy == true)
            {
                ConsolePanel.SetActive(false);
            }
            else
            {
                ConsolePanel.SetActive(true);
            }
        }

        if (PLAYER == null)
        {
            if (GameObject.Find("GameManager"))
            {
                PLAYER = GameObject.Find("GameManager").GetComponent<Player>();
            }
            else
            {
            }
        }
    }

    public void PrintFirst()
    {
        OL.AddCommandLog("Parameters Remake Console © Wolfteam Studios 2019\n" +
            "\n" +
            "Type 'help' for a list of commands.\n");
    }

    public void ExecuteCommand()
    {
        string[] args = CMD_INPUT.text.Split(char.Parse(" "));
        if (Unlocked)
        {
            switch (args[0])
            {
                #region Help
                case "help":
                    OL.AddCommandLog("List of available commands (type with lowercase): \n" +
                        "<color=#FF8000>Works only in Main Menu</color>, <color=#FF00FF>Works only in a Level</color>.\n" +
                        "\n" +
                        "ping - A command to check if console works.\n" +
                        "\n" +
                        "cheats - A command that enables cheat commands.\n" +
                        "\n" +
                        "<color=#FF8000>enabledebugpanel - Enable debug tools.</color>\n" +
                        "\n" +
                        "<color=#FF00FF>add [ITEM] (VALUE) - Add specified value to parameter.</color>\n" +
                        "[money, skeys, gkeys, upgradepoints]\n" +
                        "\n" +
                        "<color=#FF00FF>set [ITEM] (VALUE) - Set specified value to parameter.</color>\n" +
                        "[life, energy, recovery, attack, defense]\n" +
                        "\n" +
                        "<color=#FF00FF>replenish - Replenish all parameters.</color>\n");
                    break;
                #endregion Help

                #region Cheats
                case "cheats":
                    if(EnabledCheats)
                    {
                        EnabledCheats = false;
                        OL.AddCommandLog("Cheats disabled.");
                    }
                    else
                    {
                        EnabledCheats = true;
                        OL.AddCommandLog("Cheats enabled.");
                    }
                    break;
                #endregion Cheats

                #region Enabledebugpanel
                case "enabledebugpanel":
                    if (SceneManager.GetActiveScene().name == "MainMenu")
                    {
                        MainMenu.MM.EnableDebug();
                        OL.AddCommandLog("Debug panel enabled.");
                    }
                    else
                    {
                        OL.AddCommandLog("<color=#FF0000>ERROR: Invalid Scene.</color>");
                    }
                    break;
                #endregion Enabledebugpanel

                #region Add
                case "add":
                    if (EnabledCheats)
                    {
                        float value;
                        bool isNum = float.TryParse(args[2], out value);
                        if (args[1] == "money")
                        {
                            if (isNum)
                            {
                                PLAYER.Money += value;
                                OL.AddCommandLog("Added <color=#FFFF00>" + value + "$</color>.");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "skeys")
                        {
                            if (isNum)
                            {
                                PLAYER.SilverKeys += value;
                                OL.AddCommandLog("Added <color=#BEBEBE>" + value + " Silver Keys</color>.");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "gkeys")
                        {
                            if (isNum)
                            {
                                PLAYER.GoldenKeys += value;
                                OL.AddCommandLog("Added <color=#FFC300>" + value + " Gold Keys</color>.");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "upgradepoints")
                        {
                            if (isNum)
                            {
                                PLAYER.UpgradePoints += value;
                                OL.AddCommandLog("Added <color=#FF0000>" + value + " Gold Keys</color>.");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        else
                        {
                            OL.AddCommandLog("<color=#FF0000>ERROR: Invalid item type/not specified.</color>");
                        }
                    }
                    else
                    {
                        OL.AddCommandLog("<color=#FF0000>ERROR: Cheats not enabled, enable them with command 'cheats'.\n" +
                            "WARNING: Enabling them will disable ability to send score to leaderboards, until you\n" +
                            "disable them and reenter level again.</color>");
                    }
                    break;
                #endregion Add

                #region Set
                case "set":
                    if (EnabledCheats)
                    {
                        float value2;
                        bool isNum2 = float.TryParse(args[2], out value2);
                        if (args[1] == "life")
                        {
                            if (isNum2)
                            {
                                PLAYER.MaxHealth = value2;
                                OL.AddCommandLog("<color=#FFB900>Life</color> set to " + args[2] + ".");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "energy")
                        {
                            if (isNum2)
                            {
                                PLAYER.MaxEnergy = value2;
                                OL.AddCommandLog("<color=#0080FF>Energy</color> set to " + args[2] + ".");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "recovery")
                        {
                            if (isNum2)
                            {
                                PLAYER.CurrentRecovery = value2;
                                OL.AddCommandLog("<color=#00FFFF>Recovery</color> set to " + args[2] + ".");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "attack")
                        {
                            if (isNum2)
                            {
                                PLAYER.MaxAttack = value2;
                                OL.AddCommandLog("<color=#FF0000>Attack</color> set to " + args[2] + ".");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        if (args[1] == "defense")
                        {
                            if (isNum2)
                            {
                                PLAYER.MaxDefense = value2;
                                OL.AddCommandLog("<color=#8000FF>Defense</color> set to " + args[2] + ".");
                            }
                            else
                            {
                                OL.AddCommandLog("<color=#FF0000>ERROR: Invalid number specified.</color>");
                            }
                            break;
                        }
                        else
                        {
                            OL.AddCommandLog("<color=#FF0000>ERROR: Invalid item type/not specified.</color>");
                        }
                    }
                    else
                    {
                        OL.AddCommandLog("<color=#FF0000>ERROR: Cheats not enabled, enable them with command 'cheats'.\n" +
                            "WARNING: Enabling them will disable ability to send score to leaderboards, until you\n" +
                            "disable them and reenter level again.</color>");
                    }
                    break;
                #endregion Set

                #region Replenish
                case "replenish":
                    if (EnabledCheats)
                    {
                        if (PLAYER != null)
                        {
                            PLAYER.CurrentHealth = PLAYER.MaxHealth;
                            PLAYER.CurrentEnergy = PLAYER.MaxEnergy;
                            PLAYER.CurrentAttack = PLAYER.MaxAttack;
                            PLAYER.CurrentDefense = PLAYER.MaxDefense;
                            OL.AddCommandLog("All stats replenished.");
                        }
                        else
                        {
                            OL.AddCommandLog("<color=#FF0000>ERROR: Invalid Scene.</color>");
                        }
                    }
                    else
                    {
                        OL.AddCommandLog("<color=#FF0000>ERROR: Cheats not enabled, enable them with command 'cheats'.\n" +
                            "WARNING: Enabling them will disable ability to send score to leaderboards, until you\n" +
                            "disable them and reenter level again.</color>");
                    }
                    break;
                #endregion Replenish

                default:
                    OL.AddCommandLog("<color=#FF0000>Unknown Command, type 'help' for a list of command</color>");
                    break;
            }
        }
        else
        {
            switch (args[0])
            {
                case "unlock":
                    if(args[1] == Password)
                    {
                        Unlocked = true;
                        OL.AddCommandLog("Console unlocked.");
                    }
                    else
                    {
                        OL.AddCommandLog("<color=#FF0000>ERROR: Wrong password.</color>");
                    }
                    break;

                default:
                    OL.AddCommandLog("<color=#FF0000>ERROR: Console blocked, use 'unlock' + password to unlock it.</color>");
                    break;
            }
        }
        CMD_INPUT.text = "";
    }
}
