using UnityEngine;
using UnityEngine.UI;

public class CreateLevelDialog : MonoBehaviour {
    public Button exitButton;
    public Button confirmButton;
    public TMPro.TMP_InputField input;

    private void Start() {
        exitButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        confirmButton.onClick.AddListener(delegate {LevelManager.CreateNewLevel(input.text); gameObject.SetActive(false); });
    }
}
