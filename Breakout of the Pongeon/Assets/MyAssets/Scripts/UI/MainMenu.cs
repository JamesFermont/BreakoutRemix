using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private MainMenuImages backgrounds;
    private LevelBundles bundles;
    private int activeMenu = 0;
    private void Start() {
        bundles = FindObjectOfType<LevelBundles>();
        backgrounds = transform.GetComponentInChildren<MainMenuImages>();
        if (bundles.AllActiveLevels().Length > 4) {
            transform.Find("StartMenu").Find("Editor").GetComponent<Button>().interactable = true;
            transform.Find("StartMenu").Find("Play").GetComponent<UISceneLoader>().enabled = true;
        }
    }

    public void SetMenu(int index) {
        backgrounds.SetMenu(index);

    }
    public void SelectMenu(int index) {
        UnloadWrongScenes();
        activeMenu = index;
        if(index == 1 && bundles.AllActiveLevels().Length < 5) {
            GameLevelLoader.LoadLevel(bundles.bundles[0].levels[0]);
        }
    }
    public void UnsetMenu() {
        backgrounds.SetMenu(activeMenu);
    }
    public void CallQuit() {
        Application.Quit();
    }

    public void Update() {
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
        return (SceneManager.GetActiveScene().name == "MainMenu" && !SceneManager.GetSceneByName("TimeTaken").isLoaded);
    }
    private bool TargetMenuKeysPressed() {
        return ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.L));
    }
    private bool needsblankView(int index) {
        return (index == 1 || index == 3 || index == 4);
    }

    private void UnloadWrongScenes() {
        
        for(int i = 0; i < SceneManager.sceneCount; i++) {
            if (SceneManager.GetSceneAt(i).name != "MainMenu")
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
    }

}
