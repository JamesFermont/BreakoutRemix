using UnityEngine;
using UnityEngine.UI;

public class SaveLevelDialog : MonoBehaviour
{
    public Button exitButton;
    public Button confirmButton;
    public TMPro.TMP_InputField input;
    public EditorNavigation editorNavigation;

    private void OnEnable() {
        input.text = LevelManager.currentLevel.name;
        editorNavigation.HidePreviewCursor(true);
    }

    private void Start() {
        exitButton.onClick.AddListener(delegate { Hide(); });
        confirmButton.onClick.AddListener(delegate { if (LevelManager.currentLevel.name != input.text) LevelManager.currentLevel.name = input.text; LevelManager.SaveCurrentLevel(true);  Hide(); });
    }

    private void Hide() {
        editorNavigation.HidePreviewCursor(false);
        gameObject.SetActive(false);
    }
}
