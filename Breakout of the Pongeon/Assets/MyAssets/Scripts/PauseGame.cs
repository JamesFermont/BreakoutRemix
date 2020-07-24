using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
    private KeyCode pauseKey = KeyCode.Escape;
    private void Update() {
        if (!Input.GetKeyDown(pauseKey)) return;
        if (SceneManager.GetSceneByName("PauseMenu").isLoaded) {
            SceneManager.UnloadSceneAsync("PauseMenu");
        } else {
            Time.timeScale = 0f;
            SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        }
    }
}
