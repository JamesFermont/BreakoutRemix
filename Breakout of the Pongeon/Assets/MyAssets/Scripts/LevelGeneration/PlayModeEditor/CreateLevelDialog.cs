using UnityEngine;
using UnityEngine.UI;

public class CreateLevelDialog : MonoBehaviour {
    public Button exitButton;
    public Button confirmButton;
    public TMPro.TMP_InputField input;
    public EditorNavigation editorNavigation;

    private void Start() {
        exitButton.onClick.AddListener(delegate { Hide(); });
        confirmButton.onClick.AddListener(delegate {LevelManager.CreateNewLevel(input.text); Hide(); });
    }

    private void OnEnable() {
        editorNavigation.HidePreviewCursor(true);
    }

    private void Hide() {
        editorNavigation.HidePreviewCursor(false);
        gameObject.SetActive(false);
    }
}
