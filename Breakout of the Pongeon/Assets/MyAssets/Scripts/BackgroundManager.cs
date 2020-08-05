using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BackgroundManager : MonoBehaviour {
    public Background[] tracks;

    private AudioSource audioSource;
    private double duration;
    private string currentAnim;
    private bool isInitialized;

    private void Start() {
        SceneManager.activeSceneChanged += Initialize;
    }

    private void Initialize(Scene current, Scene next) {
        if (next == SceneManager.GetSceneByName("GameLevel") && !isInitialized) {
            isInitialized = true;
            StartCoroutine(InitLibrary());
        }
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
            bg.source.loopPointReached += PlayIdle;
        }

        currentAnim = "idle";
        Play("idle");
    }

    public void Play(string bgName) {
        Stop(currentAnim);
        
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return;
        }

        currentAnim = bgName;
        bgToPlay.source.Play();
    }

    private void PlayIdle(VideoPlayer vp) {
        vp.Stop();
        vp.Prepare();
        
        Background bgToPlay = Array.Find(tracks, bg => bg.name == "idle");
        if (bgToPlay == null) {
            Debug.LogWarning("Background: idle was not found!");
            return;
        }

        currentAnim = "idle";
        bgToPlay.source.Play();
    }
    
    public void Stop(string bgName) {
        Background bgToPlay = Array.Find(tracks, bg => bg.name == bgName);
        if (bgToPlay == null) {
            Debug.LogWarning("Background: " + bgName + " was not found!");
            return;
        }
        bgToPlay.source.Stop();
        bgToPlay.source.Prepare();
    }
}
