using System;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundManager : MonoBehaviour {
    public Background[] tracks;

    private void Awake() {
        foreach (Background bg in tracks) {
            bg.source = gameObject.AddComponent<VideoPlayer>();
            bg.source.playOnAwake = false;
            bg.source.audioOutputMode = VideoAudioOutputMode.None;
            bg.source.SetDirectAudioVolume(0,0);
            bg.source.clip = bg.track;
            bg.source.renderMode = VideoRenderMode.CameraFarPlane;
            bg.source.targetCamera = Camera.main;
            bg.source.targetCameraAlpha = bg.alpha;
            bg.source.isLooping = bg.loop;
            bg.source.waitForFirstFrame = bg.waitForFirstFrame;
        }
    }

    private void Start() {
        Play("testbg");
    }

    public void Play(string bgName) {
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return;
        }
        bgToPlay.source.Play();
    }
    
    public void Stop(string bgName) {
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return;
        }
        bgToPlay.source.Stop();
    }

    public void Prepare(string bgName) {
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return;
        }
        bgToPlay.source.Prepare();
    }

    public bool IsPrepared(string bgName) {
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return false;
        }
        return bgToPlay.source.isPrepared;
    }

    public bool IsPlaying(string bgName) {
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return false;
        }
        return bgToPlay.source.isPlaying;
    }
}
