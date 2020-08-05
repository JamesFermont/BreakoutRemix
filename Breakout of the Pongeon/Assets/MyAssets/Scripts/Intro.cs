using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.Video;

public class Intro : MonoBehaviour {
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private AudioMixer audioMixer;
    private AudioSource audioSource;
    
    private void Start() {
        CheckPlayerPrefs();
        vp.targetCamera = Camera.main;
        audioSource = FindObjectOfType<AudioManager>().FetchIntroSource();
        vp.SetTargetAudioSource(0, audioSource);
        vp.loopPointReached += LoadMainMenu;
        vp.Play();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void LoadMainMenu(VideoPlayer vp) {
        SceneManager.LoadScene("MainMenu");
    }
    
    private void CheckPlayerPrefs() {
        ReceiveVolumeFromPlayerPrefs();
        ReceiveViewFromPlayerPrefs();


        if (!PlayerPrefs.HasKey("playMode"))
            PlayerPrefs.SetInt("playMode", 0);
    }

    private void ReceiveVolumeFromPlayerPrefs() {
        if (PlayerPrefs.HasKey("masVolume")) {
            audioMixer.SetFloat("masVolume", PlayerPrefs.GetFloat("masVolume"));
        }

        if (PlayerPrefs.HasKey("bgmVolume")) {
            audioMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("bgmVolume"));
        }

        if (PlayerPrefs.HasKey("sfxVolume")) {
            audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
        }
    }
    
    private void ReceiveViewFromPlayerPrefs() {
        if (PlayerPrefs.HasKey("width") && PlayerPrefs.HasKey("height")) {
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen);
            if (Camera.main != null) {
                PixelPerfectCamera mainCamera = Camera.main.GetComponent<PixelPerfectCamera>();
                if (mainCamera != null) {
                    mainCamera.refResolutionX = PlayerPrefs.GetInt("width");
                    mainCamera.refResolutionY = PlayerPrefs.GetInt("height");
                    mainCamera.assetsPPU = (int)(PlayerPrefs.GetInt("height") / 7.2f);
                }
            }
        }

        if (!PlayerPrefs.HasKey("screenSizeId"))
            PlayerPrefs.SetInt("screenSizeId", 2);
    }
}
