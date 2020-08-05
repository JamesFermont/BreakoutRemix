using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private MainMenuImages backgrounds;
    private int activeMenu = 0;
    private void Start() {
        backgrounds = transform.GetComponentInChildren<MainMenuImages>();
        Debug.Log(FindObjectOfType<LevelBundles>().AllActiveLevels().Length);
        if (FindObjectOfType<LevelBundles>().AllActiveLevels().Length >= 4) {
            transform.Find("StartMenu").Find("Editor").GetComponent<Button>().interactable = true;
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
