using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private KeyCode pauseKey = KeyCode.Escape;
    private void Update() {
        if (!Input.GetKeyDown(pauseKey)) return;
        if (SceneManager.GetSceneByName("OptionsMenu").isLoaded) {
            SceneManager.UnloadSceneAsync("OptionsMenu");
        } else {
            Time.timeScale = 0f;
            SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
        }
    }
}
