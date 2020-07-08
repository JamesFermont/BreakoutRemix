using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void CallQuit() {
        Application.Quit();
    }

    public void OpenOptionsMenu() {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }
}
