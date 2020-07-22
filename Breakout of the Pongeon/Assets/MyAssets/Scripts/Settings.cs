﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider ballSpeedSlider;
    public TMP_Text valueText;
    public TMP_Dropdown resolutionDropdown;
    public Toggle normalMode;
    public Toggle hardMode;
    
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
        
        if (PlayerPrefs.HasKey("playMode"))
            switch (PlayerPrefs.GetInt("playMode")) {
                case 0:
                    normalMode.isOn = true;
                    hardMode.isOn = false;
                    break;
                case 1:
                    normalMode.isOn = false;
                    hardMode.isOn = true;
                    break;
            }
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
                if (Camera.main != null) {
                    Camera.main.GetComponent<PixelPerfectCamera>().refResolutionX = 1920;
                    Camera.main.GetComponent<PixelPerfectCamera>().refResolutionY = 1080;
                    Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 150;
                }
                break;
            case 1:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                PlayerPrefs.SetInt("width", 1600);
                PlayerPrefs.SetInt("height", 900);
                PlayerPrefs.SetInt("screenSizeId", 1);
                if (Camera.main != null) {
                    Camera.main.GetComponent<PixelPerfectCamera>().refResolutionX = 1600;
                    Camera.main.GetComponent<PixelPerfectCamera>().refResolutionY = 900;
                    Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 125;
                }
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                PlayerPrefs.SetInt("width", 1280);
                PlayerPrefs.SetInt("height", 720);
                PlayerPrefs.SetInt("screenSizeId", 2);
                if (Camera.main != null) {
                    Camera.main.GetComponent<PixelPerfectCamera>().refResolutionX = 1280;
                    Camera.main.GetComponent<PixelPerfectCamera>().refResolutionY = 720;
                    Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU = 100;
                }
                break;
        }
    }

    public void ReturnToTitle() {
        if (Time.timeScale <= Mathf.Epsilon) Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        if (Time.timeScale <= Mathf.Epsilon) Time.timeScale = 1;
        Application.Quit();
    }

    private void OnApplicationQuit() {
        PlayerPrefs.Save();
    }
}
