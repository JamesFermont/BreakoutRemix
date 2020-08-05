using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
        
    private GameObject gameMenu;
    private GameObject soundMenu;

    private void OnEnable() {
        if (PlayerPrefs.HasKey("masVolume")) {
            audioMixer.SetFloat("masVolume", PlayerPrefs.GetFloat("masVolume"));
            if (masterVolumeSlider) masterVolumeSlider.value = PlayerPrefs.GetFloat("masVolume");
        }

        if (PlayerPrefs.HasKey("bgmVolume")) {
            audioMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("bgmVolume"));
            if (bgmVolumeSlider) bgmVolumeSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }

        if (PlayerPrefs.HasKey("sfxVolume")) {
            audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
            if (sfxVolumeSlider) sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        
        gameMenu = GameObject.FindWithTag("GameMenu");
        soundMenu = GameObject.FindWithTag("VSMenu");
        soundMenu.SetActive(false);
    }

    public void UpdateMasterVolume(float volume) {
        audioMixer.SetFloat("masVolume", volume);
        PlayerPrefs.SetFloat("masVolume", volume);
    }
    
    public void UpdateBgmVolume(float volume) {
        audioMixer.SetFloat("bgmVolume", volume);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }
    
    public void UpdateSfxVolume(float volume) {
        audioMixer.SetFloat("sfxVolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void DisplayGame() {
        if (!gameMenu.activeInHierarchy) {
            soundMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
    }
	
    public void DisplayVideoSound() {
        if (!soundMenu.activeInHierarchy) {
            gameMenu.SetActive(false);
            soundMenu.SetActive(true);
        }
    }

    public void ReturnToMain() {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy() {
        if (!SceneManager.GetSceneByName("GameLevel").isLoaded) return;
        if (!SceneManager.GetSceneByName("ResultScreen").isLoaded) {
            if (GameObject.FindWithTag("Paddle").GetComponent<PlayerAbility>().btIsActive) {
                Time.timeScale = GameObject.FindWithTag("Paddle").GetComponent<PlayerAbility>().btTimeScale;
            } else {
                Time.timeScale = 1f;
            }
        }
    }
}
