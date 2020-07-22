using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    private void Awake()
    {
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
        Play("bgm_menu");
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

    public void UpdatePitch(string soundName, float pitch) {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == soundName);
        if (soundToPlay == null) {
            Debug.LogWarning("Sound: " + soundName + " was not found!");
            return;
        }
        Mathf.Clamp(pitch, 0.1f, 3f);
        soundToPlay.source.pitch = pitch;
    }

    public AudioSource FetchVideoSource() {
        Sound sourceToFetch = Array.Find(sounds, sound => sound.name == "video");
        return sourceToFetch.source;
    }
}