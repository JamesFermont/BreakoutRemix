using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public Button button;
    private LevelBundles bundles;

    private void Start() {
        bundles = FindObjectOfType<LevelBundles>();
        if (!FindObjectOfType<LevelBundles>().hasNext(LevelManager.currentLevel.name))
            button.gameObject.SetActive(false);
        button.onClick.AddListener(delegate { if (bundles.hasNext(LevelManager.currentLevel.name)) {GameLevelLoader.LoadLevel(bundles.nextLevel(LevelManager.currentLevel.name));button.interactable = false; } });
    }

    void LoadLevel() {

                SceneManager.LoadSceneAsync("GameLevel", LoadSceneMode.Single);
        
    }
}
