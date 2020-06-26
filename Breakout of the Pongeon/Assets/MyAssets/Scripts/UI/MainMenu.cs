using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public enum Menu { MAIN, LEVEL_SELECT, OPTIONS, CREDITS }
    Menu currentMenu = Menu.MAIN;
    private bool movingOut = false;
    private bool movingIn;

    public void ButtonClick(string buttonName) {
        Debug.Log(buttonName);
    }

    public void CallQuit() {
        Application.Quit();
    }

    public void ToLevelSelection() {
        Debug.Log(LevelIO.getLevelsInDirectory().Length);
        TransitionToMenu(Menu.LEVEL_SELECT);
    }

    public void TransitionToMenu(Menu menu) {
        StartCoroutine(moveInAndOut(menu));
    }
    private IEnumerator moveInAndOut(Menu newMenu) {
        UIFader currentFader = toTransform(currentMenu).GetComponent<UIFader>();
        currentFader.FadeOut(Direction.DOWN);
        yield return new WaitForSeconds(0.05f);
        while(currentFader.isMoving) {
            yield return new WaitForSeconds(0.05f);
        }
        currentMenu = newMenu;
        toTransform(currentMenu).GetComponent<UIFader>().FadeIn();
    }

    public void toMainMenu() {
        TransitionToMenu(Menu.MAIN);
    }

    public void OpenOptionsMenu() {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }

    private Transform toTransform(Menu menu) {
        switch (menu) {
            case Menu.MAIN:
                return transform.Find("StartMenu");
            case Menu.LEVEL_SELECT:
                return transform.Find("LevelMenu");
            case Menu.CREDITS:
                return transform.Find("CreditsMenu");
            case Menu.OPTIONS:
                return transform.Find("OptionsMenu");
            default:
                return null;
        }
    }
}
