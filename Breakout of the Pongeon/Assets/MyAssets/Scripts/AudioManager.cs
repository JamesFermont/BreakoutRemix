﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    private void Awake() {
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.type;
        }
    }

    private void Start() {
        SceneManager.activeSceneChanged += PlayBgm;
    }

    private void PlayBgm(Scene current, Scene next) {
        if (next == SceneManager.GetSceneByName("MainMenu")) {
            Time.timeScale = 1f;
            UpdatePitch(1f);
            if (IsPlaying("bgm_game_01"))
                Stop("bgm_game_01");
            Play("bgm_menu");
        }
    }

    public void Play(string soundName) {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == soundName);
        if (soundToPlay == null) {
            Debug.LogWarning("Sound: " + soundName + " was not found!");
            return;
        }
        soundToPlay.source.Play();
    }

    public void Stop(string soundName) {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == soundName);
        if (soundToPlay == null) {
            Debug.LogWarning("Sound: " + soundName + " was not found!");
            return;
        }
        soundToPlay.source.Stop();
    }

    public bool IsPlaying(string soundName) {
        Sound soundToCheck = Array.Find(sounds, sound => sound.name == soundName);
        if (soundToCheck == null) {
            Debug.LogWarning("Sound: " + soundName + " was not found!");
            return false;
        }
        return soundToCheck.source.isPlaying;
    }

    public void UpdatePitch(float pitch) {
        foreach (Sound sound in sounds) {
            Mathf.Clamp(pitch, 0.1f, 3f);
            sound.source.pitch = pitch;
        }
    }

    public AudioSource FetchVideoSource() {
        Sound sourceToFetch = Array.Find(sounds, sound => sound.name == "video");
        return sourceToFetch.source;
    }
    
    public AudioSource FetchIntroSource() {
        Sound sourceToFetch = Array.Find(sounds, sound => sound.name == "intro");
        return sourceToFetch.source;
    }
}