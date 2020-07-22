﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;
    
    private void Start() {
        if (PlayerPrefs.HasKey("masVolume")) {
            audioMixer.SetFloat("masVolume", PlayerPrefs.GetFloat("masVolume"));
        }

        if (PlayerPrefs.HasKey("bgmVolume")) {
            audioMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("bgmVolume"));
        }

        if (PlayerPrefs.HasKey("sfxVolume")) {
            audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
        }
        if (PlayerPrefs.HasKey("width") && PlayerPrefs.HasKey("height")) 
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen);
        
        if (!PlayerPrefs.HasKey("screenSizeId"))
            PlayerPrefs.SetInt("screenSizeId", 2);
        
        if (!PlayerPrefs.HasKey("playMode"))
            PlayerPrefs.SetInt("playMode", 0);
    }

    public void CallQuit() {
        Application.Quit();
    }

    public void FixedUpdate() {
        if (SceneManager.GetActiveScene().name == "MainMenu" && !SceneManager.GetSceneByName("TimeTaken").IsValid()) {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKey(KeyCode.L))
                OpenTimeTargetMenu();
        }

    }

    private void OpenTimeTargetMenu() {
        SceneManager.LoadSceneAsync("TimeTaken", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("TimeTaken"));
    }

    public void OpenOptionsMenu() {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }
}
