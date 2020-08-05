using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelLoader : MonoBehaviour
{
    public static string currentLevel;

    // Start is called before the first frame update
    private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name != "GameLevel")
            return;
        StartCoroutine(ChangeToPlayMode());
    }
    public static void LoadLevel(string levelName) {
        currentLevel = levelName;
        if (SceneManager.GetSceneByName("GameLevel").isLoaded)
            SceneManager.UnloadSceneAsync("GameLevel");
        SceneManager.LoadSceneAsync("GameLevel", LoadSceneMode.Additive);
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private IEnumerator ChangeToPlayMode() {
        Time.timeScale = 1f;
        yield return new WaitForEndOfFrame();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameLevel"));
        LevelManager.LoadLevel(currentLevel);
        FindObjectOfType<TargetManager>().FindTargetAreas();
        if(SceneManager.GetSceneByName("LevelSelect").isLoaded)
        SceneManager.UnloadSceneAsync("LevelSelect");
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            SceneManager.UnloadSceneAsync("MainMenu");
        if (SceneManager.GetSceneByName("ResultScreen").isLoaded)
            SceneManager.UnloadSceneAsync("ResultScreen");
    }
}
