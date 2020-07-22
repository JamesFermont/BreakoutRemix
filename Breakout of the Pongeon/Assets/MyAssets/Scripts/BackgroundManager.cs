using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundManager : MonoBehaviour {
    public Background[] tracks;

    private AudioSource audioSource;
    private double duration;

    private void Awake() {
        StartCoroutine(InitLibrary());
    }

    private IEnumerator InitLibrary() {
        float timeElapsed = 0f;

        while (timeElapsed <= 0.5f) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        BuildLibrary();
    }

    private void BuildLibrary() {
        audioSource = FindObjectOfType<AudioManager>().FetchVideoSource();

        foreach (Background bg in tracks) {
            bg.source = gameObject.AddComponent<VideoPlayer>();
            bg.source.playOnAwake = false;
            bg.source.audioOutputMode = VideoAudioOutputMode.AudioSource;
            bg.source.SetTargetAudioSource(0, audioSource);
            bg.source.clip = bg.track;
            bg.source.renderMode = VideoRenderMode.CameraFarPlane;
            bg.source.targetCamera = Camera.main;
            bg.source.targetCameraAlpha = bg.alpha;
            bg.source.isLooping = bg.loop;
            bg.source.waitForFirstFrame = bg.waitForFirstFrame;
        }

        Play("idle");
    }

    public void Play(string bgName) {
        if (IsPlaying("idle")) return;
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return;
        }
        bgToPlay.source.Play();
        duration = bgToPlay.source.length;
        if (bgName != "idle") {
            StartCoroutine(ReturnToIdle());
        }
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

    private IEnumerator ReturnToIdle() {
        float timeElapsed = 0f;
        Debug.Log(duration);

        while (timeElapsed < duration) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        Debug.Log("returned to idle!");
        duration = 0f;
        Play("idle");
    }
}
