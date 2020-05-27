using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    public static AudioManager instance;
    
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void Start() {
        Play("testbgm");
    }

    public void Play(string name) {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);
        if (soundToPlay == null) {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        soundToPlay.source.Play();
    }

    public void Stop(string name) {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);
        if (soundToPlay == null) {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        soundToPlay.source.Stop();
    }
}
