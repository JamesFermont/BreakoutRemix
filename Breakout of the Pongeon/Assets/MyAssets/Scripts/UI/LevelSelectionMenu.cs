using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectionMenu : MonoBehaviour {
    public Transform levelList;
    public Object levelListPrefab;
    string[] levels;
    LevelBundles bundles;
    string currentLevel;

    private void Start() {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;
        AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!am.IsPlaying("bgm_menu")) {
            if (am.IsPlaying("bgm_game_01"))
                am.Stop("bgm_game_01");
            am.Play("bgm_menu");
        }
        bundles = FindObjectOfType<LevelBundles>();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        int numberOfLevels = levelList.childCount;

        if (levels == null) {
            levels = LevelIO.getLevelsInDirectory();
        }
       
        ((RectTransform)levelList).sizeDelta = new Vector2(0, levels.Length * 64);
            foreach (string level in levels) {
                if (levelList.Find(level))
                    continue;
            CreateLevelButton(level, numberOfLevels++);
                
            }

    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name != "GameLevel")
            return;
        StartCoroutine(ChangeToPlayMode());
    }

    private void LoadLevel(string levelName) {
        currentLevel = levelName;
        SceneManager.LoadSceneAsync("GameLevel", LoadSceneMode.Additive);
    }

    private IEnumerator ChangeToPlayMode() {
        yield return new WaitForEndOfFrame();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameLevel"));
        LevelManager.LoadLevel(currentLevel);
        FindObjectOfType<TargetManager>().FindTargetAreas();
        SceneManager.UnloadSceneAsync("LevelSelect");
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            SceneManager.UnloadSceneAsync("MainMenu");
    }

    private void CreateLevelButton (string name, int yPosition) {
        GameObject button = (GameObject)Instantiate(levelListPrefab, levelList);

        button.name = name;
        button.transform.localPosition = new Vector3(0f, -30f - 60f * yPosition, 0f);

        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = name;
        button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(name); });
        button.GetComponent<Button>().interactable = true;
    } 



}
