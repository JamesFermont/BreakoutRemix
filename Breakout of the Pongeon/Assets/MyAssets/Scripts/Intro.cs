using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour {
    [SerializeField] private VideoPlayer vp;
    
    private void Start() {
        vp.loopPointReached += LoadMainMenu;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void LoadMainMenu(VideoPlayer vp) {
        SceneManager.LoadScene("MainMenu");
    }
}
