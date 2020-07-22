using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTakenWindow : MonoBehaviour {
    public Transform levelList;
    public Object levelTimePrefab;
    public UnityEngine.UI.Button exitButton;

    string[] levels;
    List<GameObject> entries;


    void OnEnable() {
        if (levels == null)
            levels = LevelIO.getLevelsInDirectory();

        levelList.GetComponent<RectTransform>().sizeDelta = new Vector2(960, levels.Length * 72);
        GameObject currentLevelSign;

        for (int i = 0; i < levels.Length; i++) {
            currentLevelSign = (GameObject)Instantiate(levelTimePrefab, levelList);
            currentLevelSign.transform.localPosition = Vector3.down * (i + 0.5f) * 72;
            currentLevelSign.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = levels[i];
            currentLevelSign.transform.GetChild(1).GetComponent<TMPro.TMP_InputField>().text = LevelTimeTargets.getTarget(levels[i]).ToString();
            string name = levels[i];
            TMPro.TMP_InputField field = currentLevelSign.transform.GetChild(1).GetComponent<TMPro.TMP_InputField>();
            field.onSubmit.AddListener(delegate { SubmitTime(field, name); });
            field.onDeselect.AddListener(delegate { SubmitTime(field, name); });
        }
        exitButton.onClick.AddListener(delegate {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("TimeTaken"));
        });
    }
    private void SubmitTime(TMPro.TMP_InputField myField, string level) {
        int score = int.Parse(myField.text);
        LevelTimeTargets.setTarget(level, score);
        Debug.Log(level + ":" + myField.text);
    }

    private void updateLists() {
        levels = LevelIO.getLevelsInDirectory();
        LevelTimeTargets.SaveLevels();
    }


}
