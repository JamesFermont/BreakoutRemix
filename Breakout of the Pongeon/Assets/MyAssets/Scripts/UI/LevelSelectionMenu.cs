using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectionMenu : MonoBehaviour
{
    public Transform levelList;
    public Object levelListPrefab;


    int numberOfLevels = 0;
    private void OnEnable() {

        string[] levels = LevelIO.getLevelsInDirectory();

        GameObject currentLevelButton;
        ((RectTransform)levelList).sizeDelta = new Vector2(0, levels.Length * 64);
        foreach (string level in levels) {
            if (levelList.Find(level))
                continue;
            currentLevelButton = (GameObject)Instantiate(levelListPrefab, levelList);
            string s = level;
            currentLevelButton.name = level;
            currentLevelButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = level;
            currentLevelButton.transform.localPosition = new Vector3(0f, -30f - 60f * numberOfLevels++, 0f);
            currentLevelButton.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(s); });

        }

    }

    private void LoadLevel(string levelName) {
        StartCoroutine(LoadLevelInScene(levelName));
    }
    private IEnumerator LoadLevelInScene (string levelName) {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("GameLevel", LoadSceneMode.Additive);
        while (!loadingScene.isDone) {
            yield return null;
        }
        yield return new WaitForFixedUpdate();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameLevel"));
        LevelManager.LoadLevel(levelName);
        FindObjectOfType<TargetManager>().FindTargetAreas();
        SceneManager.UnloadSceneAsync("LevelSelect");
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            SceneManager.UnloadSceneAsync("MainMenu");

        
    }
}
