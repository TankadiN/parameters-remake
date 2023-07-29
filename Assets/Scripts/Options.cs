using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Options : MonoBehaviour
{
    [Header("Music")]
    public AudioMixer AudioMixer;
    public TMP_Text percText;
    public Slider AudioSlider;
    [Header("Screen")]
    public TMP_Dropdown ResolutionDD;
    public Toggle FullscreenToggle;

    private float audioMusic;
    private bool resBlock;

    Resolution[] resolutions;

    private void Start()
    {
        AudioMixer.GetFloat("MasterVolume", out audioMusic);
        AudioSlider.value = audioMusic;
        FullscreenToggle.isOn = Screen.fullScreen;

        resBlock = true;

        resolutions = Screen.resolutions;

        ResolutionDD.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @" + resolutions[i].refreshRateRatio;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDD.AddOptions(options);
        ResolutionDD.value = currentResolutionIndex;
        ResolutionDD.RefreshShownValue();

        resBlock = false;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (!resBlock)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height + " @" + res.refreshRateRatio;
    }

    public void SwitchDiscordPresence(bool toggle)
    {
        DiscordController.instance.discordRP = toggle;
        DiscordController.instance.SwitchPresence();
    }

    private void Update()
    {
        AudioMixer.GetFloat("MasterVolume", out audioMusic);

        float calcPercent = (AudioSlider.value / AudioSlider.minValue * -100) + 100;
        percText.text = calcPercent.ToString("0") + "%";
    }
}
