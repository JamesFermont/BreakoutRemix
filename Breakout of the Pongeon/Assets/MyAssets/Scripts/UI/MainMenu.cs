using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void CallQuit() {
        Application.Quit();
        AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if(!am.IsPlaying())
            am.Play("bgm_menu");
    }

    public void FixedUpdate() {
        if (SceneManager.GetActiveScene().name == "MainMenu" && !SceneManager.GetSceneByName("TimeTaken").IsValid()) {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKey(KeyCode.L))
                OpenTimeTargetMenu();
        }

    }

    private void OpenTimeTargetMenu() {
        SceneManager.LoadSceneAsync("TimeTaken", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("TimeTaken"));
    }

    public void OpenOptionsMenu() {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }
}
