using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    // Audio mixer variable
    public AudioMixer audioMixer;

    // Volume is adjusted
	public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
