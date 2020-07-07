using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider ballSpeedSlider;
    public TMP_Text valueText;
    public TMP_Dropdown resolutionDropdown;
    
    private void OnEnable() {
        if (PlayerPrefs.HasKey("masVolume")) {
            audioMixer.SetFloat("masVolume", PlayerPrefs.GetFloat("masVolume"));
            if (masterVolumeSlider) masterVolumeSlider.value = PlayerPrefs.GetFloat("masVolume");
        }

        if (PlayerPrefs.HasKey("bgmVolume")) {
            audioMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("bgmVolume"));
            if (bgmVolumeSlider) bgmVolumeSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }

        if (PlayerPrefs.HasKey("sfxVolume")) {
            audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
            if (sfxVolumeSlider) sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }

        if (PlayerPrefs.HasKey("ballSpeed")) {
            if (ballSpeedSlider) ballSpeedSlider.value = PlayerPrefs.GetFloat("ballSpeed");
            if (valueText) valueText.text = "x" + PlayerPrefs.GetFloat("ballSpeed");
        }
        
        if (PlayerPrefs.HasKey("width") && PlayerPrefs.HasKey("height")) 
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen);
        
        if (PlayerPrefs.HasKey("screenSizeId"))
            resolutionDropdown.value = PlayerPrefs.GetInt("screenSizeId");
    }

    public void UpdateBallSpeed(float mod) {
        PlayerPrefs.SetFloat("ballSpeed", (float) Math.Round(mod, 2));
        if (valueText) valueText.text = "x" + PlayerPrefs.GetFloat("ballSpeed");
    }

    public void UpdatePlayMode(bool isNormal) {
        PlayerPrefs.SetInt("playMode", isNormal ? 0 : 1);
    }

    public void UpdateMasterVolume(float volume) {
        audioMixer.SetFloat("masVolume", volume);
        PlayerPrefs.SetFloat("masVolume", volume);
    }
    
    public void UpdateBgmVolume(float volume) {
        audioMixer.SetFloat("bgmVolume", volume);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }
    
    public void UpdateSfxVolume(float volume) {
        audioMixer.SetFloat("sfxVolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullScreen", isFullscreen ? 1 : 0);
    }

    public void UpdateResolution(Int32 value) {
        switch (value) {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                PlayerPrefs.SetInt("width", 1920);
                PlayerPrefs.SetInt("height", 1080);
                PlayerPrefs.SetInt("screenSizeId", 0);
                break;
            case 1:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                PlayerPrefs.SetInt("width", 1600);
                PlayerPrefs.SetInt("height", 900);
                PlayerPrefs.SetInt("screenSizeId", 1);
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                PlayerPrefs.SetInt("width", 1280);
                PlayerPrefs.SetInt("height", 720);
                PlayerPrefs.SetInt("screenSizeId", 2);
                break;
        }
    }

    public void ReturnToTitle() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        Application.Quit();
    }

    private void OnApplicationQuit() {
        PlayerPrefs.Save();
    }
}
