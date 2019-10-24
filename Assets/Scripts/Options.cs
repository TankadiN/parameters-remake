using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Options : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public TMP_Text dbText;
    public GameObject audioWarning;
    public Slider AudioSlider;
    public float db;

    private void Start()
    {
        AudioMixer.GetFloat("MasterVolume", out db);
        AudioSlider.value = db;
    }

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
