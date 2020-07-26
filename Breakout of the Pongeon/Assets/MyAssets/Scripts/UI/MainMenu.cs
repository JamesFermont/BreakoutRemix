using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;
    private MainMenuImages backgrounds;
    private int activeMenu = 0;
    private void Start() {
        CheckPlayerPrefs();
        backgrounds = transform.GetComponentInChildren<MainMenuImages>();
        Debug.Log(FindObjectOfType<LevelBundles>().AllActiveLevels().Length);
        if (FindObjectOfType<LevelBundles>().AllActiveLevels().Length > 4) {
            transform.Find("StartMenu").Find("LevelEditor").GetComponent<Button>().interactable = true;
        }
            
    }

    public void SetMenu(int index) {
        backgrounds.SetMenu(index);
        if (activeMenu == 0)
            backgrounds.SetView(index);
    }
    public void SelectMenu(int index) {
        activeMenu = index;
        if (needsblankView(index))
            index = 0;
        backgrounds.SetViewAndSelection(index);
    }
    public void UnsetMenu() {
        backgrounds.SetMenu(activeMenu);
    }
    public void CallQuit() {
        Application.Quit();
    }

    public void FixedUpdate() {
        if (CanOpenTargetMenu()) {
            if (TargetMenuKeysPressed())
                OpenTimeTargetMenu();
        }

    }

    private void OpenTimeTargetMenu() {
        SceneManager.LoadSceneAsync("TimeTaken", LoadSceneMode.Additive);
    }

    public void OpenOptionsMenu() {
        if (!SceneManager.GetSceneByName("OptionsMenu").isLoaded)
            SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
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
            PixelPerfectCamera mainCamera = Camera.main.GetComponent<PixelPerfectCamera>();
            if (mainCamera != null) {
                mainCamera.refResolutionX = PlayerPrefs.GetInt("width");
                mainCamera.refResolutionY = PlayerPrefs.GetInt("height");
                mainCamera.assetsPPU = (int)(PlayerPrefs.GetInt("height") / 7.2f);
            }
        }

        if (!PlayerPrefs.HasKey("screenSizeId"))
            PlayerPrefs.SetInt("screenSizeId", 2);

    }
    private bool CanOpenTargetMenu() {
        return (SceneManager.GetActiveScene().name == "MainMenu" && !SceneManager.GetSceneByName("TimeTaken").IsValid());
    }
    private bool TargetMenuKeysPressed() {
        return ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKey(KeyCode.L));
    }
    private bool needsblankView(int index) {
        return (index == 1 || index == 3 || index == 4);
    }

}
