using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectionMenu : MonoBehaviour {
    public Transform levelList;
    public Object levelBundlePrefab;
    public Object lockedBundlePrefab;
    public Object userLevelPrefab;
    public Object userLevelButtonPrefab;
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
        int numberOfLevels = levelList.childCount;

        if (bundles == null) {
            bundles = FindObjectOfType<LevelBundles>();
        }
        ((RectTransform)levelList).sizeDelta = new Vector2(0, (int)(bundles.AllActiveLevels().Length/4f) * 300 +15+ 60 * (LevelIO.getLevelsInDirectory(true).Length+1));
        int playedLevels = 0;

        for (int i = 0; i < bundles.bundles.Length; i++) {
            foreach (string level in bundles.bundles[i].levels) {
                if (Scores.GetHighscore(level) > 0)
                    playedLevels++;
            }
            if (i > 0) {
                if(2 <= playedLevels)
                    CreateBundle(bundles.bundles[i], numberOfLevels++);
                playedLevels = 0;

            } else {
                CreateBundle(bundles.bundles[i], numberOfLevels++);
            }

        }
        Transform UserLevels = ((GameObject)Instantiate(userLevelPrefab, levelList)).transform;
        UserLevels.localPosition = new Vector3(UserLevels.localPosition.x, (int)(bundles.AllActiveLevels().Length / 4f) * -300 - 45, UserLevels.localPosition.z);
        UserLevels.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, 60 * (LevelIO.getLevelsInDirectory(true).Length + 1));
        int index = 1;
         foreach (string level in LevelIO.getLevelsInDirectory(true)) {
            GameObject UserLevelButton = (GameObject)Instantiate(userLevelButtonPrefab, UserLevels.transform);
            Button b = UserLevelButton.GetComponent<Button>();
            b.name = level;
            UserLevelButton.transform.localPosition = new Vector3(-10f, -30f - index * 60f + 45f, 0f);
            string levelName = level;
            b.onClick.AddListener(delegate { b.interactable = false; GameLevelLoader.LoadLevel(level, true); });
            b.transform.GetChild(0).GetComponent<Text>().text = "\t\t" + level;
            b.transform.GetChild(1).GetComponent<Text>().text = Scores.GetHighscore(level) + "\t\t";
            index++;
        }  
                
                

    }
    private void CreateBundle (LevelBundle bundle, int yPosition) {
        GameObject myBundle = (GameObject)Instantiate(levelBundlePrefab, levelList);

        myBundle.name = bundle.name;
        myBundle.transform.localPosition = new Vector3(0f, -150f - 300f * yPosition, 0f);
        myBundle.GetComponent<BundleObject>().Init(bundle);
    } 



}
