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
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start() {
        Play("testbgm");
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

    public void UpdatePitch(string soundName, float pitch) {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == soundName);
        if (soundToPlay == null) {
            Debug.LogWarning("Sound: " + soundName + " was not found!");
            return;
        }
        Mathf.Clamp(pitch, 0.1f, 3f);
        soundToPlay.source.pitch = pitch;
    }

    public void UpdateVolumeOfType(SoundType type, float volume) {
        Mathf.Clamp(volume, 0.1f, 1f);
        foreach (Sound sound in sounds) {
            if (sound.type == type) sound.volume = volume;
        }
    }
}
