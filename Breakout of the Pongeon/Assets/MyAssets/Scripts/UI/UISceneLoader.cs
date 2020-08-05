using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour {
    public enum LoadingType { LOAD, UNLOAD }

    public Button levelLoadButton;
    public string sceneName;
    public bool isAdditive;
    public LoadingType type;
    public bool forceReload = false;
    // Start is called before the first frame update
    void Start() {
        levelLoadButton.onClick.AddListener(delegate { LoadLevel(); });
    }

    // Update is called once per frame
    void LoadLevel() {

        if (type == LoadingType.LOAD) {
            if (SceneManager.GetSceneByName(sceneName).isLoaded && forceReload == false)
                return;
            if (isAdditive)
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            else
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        if (type == LoadingType.UNLOAD) {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                return;
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
