using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectionMenu : MonoBehaviour {
    public Transform levelList;
    public Object levelBundlePrefab;
    public Object lockedBundlePrefab;
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
        ((RectTransform)levelList).sizeDelta = new Vector2(0, bundles.bundles.Length * 300);
        int previousScore = 0;
        for (int i = 0; i < bundles.bundles.Length; i++) {
            if (i > 0) {
                if(bundles.bundles[i].score <= previousScore)
                    CreateBundle(bundles.bundles[i], numberOfLevels++); 

            } else {
                CreateBundle(bundles.bundles[i], numberOfLevels++);
            }

            previousScore = bundles.bundles[i].TotalScore();
        }
            
            
                
                

    }
    private void CreateBundle (LevelBundle bundle, int yPosition) {
        GameObject myBundle = (GameObject)Instantiate(levelBundlePrefab, levelList);

        myBundle.name = bundle.name;
        myBundle.transform.localPosition = new Vector3(0f, -150f - 300f * yPosition, 0f);
        myBundle.GetComponent<BundleObject>().Init(bundle);
    } 



}
