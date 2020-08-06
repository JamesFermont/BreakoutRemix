using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelDialog : MonoBehaviour
{
    public Button exitButton;
    public EditorNavigation editorNavigation;
    public Transform levelList;
    public Object levelListPrefab;

    int numberOfLevels = 0;
    private void OnEnable() {
        editorNavigation.HidePreviewCursor(true);

        string[] levels = LevelIO.getLevelsInDirectory(true);

        GameObject currentLevelButton;
        ((RectTransform)levelList).sizeDelta = new Vector2(0, levels.Length * 64);
        foreach (string level in levels) {
            if (levelList.Find(level))
                continue;
            currentLevelButton = (GameObject)Instantiate(levelListPrefab, levelList);
            string s = level;
            currentLevelButton.name = level;
            currentLevelButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = level;
            currentLevelButton.transform.localPosition = new Vector3(450f, -32f - 64f * numberOfLevels++, 0f);
            currentLevelButton.GetComponent<Button>().onClick.AddListener(delegate { LevelManager.LoadLevel(s); Hide(); });

        }

    }

    private void Start() {
        exitButton.onClick.AddListener(delegate { Hide(); });
    }

    private void Hide() {
        editorNavigation.HidePreviewCursor(false);
        gameObject.SetActive(false);
    }
}
