using UnityEngine;
using UnityEngine.UI;

public class SaveLevelDialog : MonoBehaviour
{
    public Button exitButton;
    public Button confirmButton;
    public TMPro.TMP_InputField input;


    private void OnEnable() {
        input.text = LevelManager.currentLevel.name;
    }

    private void Start() {
        exitButton.onClick.AddListener(delegate { gameObject.SetActive(false); });
        confirmButton.onClick.AddListener(delegate { if (LevelManager.currentLevel.name != input.text) LevelManager.currentLevel.name = input.text; LevelManager.SaveCurrentLevel();  gameObject.SetActive(false); });
    }
}
