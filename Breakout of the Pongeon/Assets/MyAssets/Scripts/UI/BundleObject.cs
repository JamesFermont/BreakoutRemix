using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BundleObject : MonoBehaviour
{
    public TMP_Text bundleName;
    public Button [] levelButton;
    public Color highlightTextColor = Color.black;
    public Color hightlightBackgroundColor = new Color (33, 231, 178);
    public ColorBlock highlightBackgroundColors;

    public void Init(LevelBundle bundle) {
        bundleName.text = bundle.name;
        for (int i = 0; i < bundle.levels.Length; i++) {
            if(LevelManager.currentLevel != null && LevelManager.currentLevel.name == bundle.levels[i]) {
                levelButton[i].GetComponentInChildren<Text>().color = highlightTextColor;
                levelButton[i].colors = highlightBackgroundColors;
            }
            levelButton[i].GetComponentInChildren<Text>().text = bundle.levels[i];
            string levelName = bundle.levels[i];
            levelButton[i].onClick.AddListener(delegate { FindObjectOfType<LevelSelectionMenu>().LoadLevel(levelName); });
        }

    }
}
