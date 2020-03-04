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
    public TMP_Text dbText;
    public GameObject audioWarning;
    public Slider AudioSlider;
    public float db;
    [Header("Screen")]
    public TMP_Dropdown ResolutionDD;
    public Toggle FullscreenToggle;

    Resolution[] resolutions;

    private void Start()
    {
        AudioMixer.GetFloat("MasterVolume", out db);
        AudioSlider.value = db;
        FullscreenToggle.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;

        ResolutionDD.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @" + resolutions[i].refreshRate;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDD.AddOptions(options);
        ResolutionDD.value = currentResolutionIndex;
        ResolutionDD.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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
        return res.width + " x " + res.height + " @" + res.refreshRate;
    }

    private void Update()
    {
        AudioMixer.GetFloat("MasterVolume", out db);
        dbText.text = db + " db";
        if(db >= 5)
        {
            audioWarning.SetActive(true);
        }
        else
        {
            audioWarning.SetActive(false);
        }
    }
}
