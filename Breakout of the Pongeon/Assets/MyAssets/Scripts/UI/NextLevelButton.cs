using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public Button button;
    private LevelBundles bundles;

    private void Start() {
        bundles = FindObjectOfType<LevelBundles>();
        button.onClick.AddListener(delegate { if (bundles.hasNext(LevelManager.currentLevel.name)) {GameLevelLoader.LoadLevel(bundles.nextLevel(LevelManager.currentLevel.name));button.interactable = false; } });
    }

    void LoadLevel() {

                SceneManager.LoadSceneAsync("GameLevel", LoadSceneMode.Single);
        
    }
}
