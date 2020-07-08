using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {
	private GameObject gameMenu;
	private GameObject vsMenu;
	public GameObject ingameMenu;
	
	private void OnEnable() {
		gameMenu = GameObject.FindWithTag("GameMenu");
		vsMenu = GameObject.FindWithTag("VSMenu");
		vsMenu.SetActive(false);
		ingameMenu.SetActive(SceneManager.GetSceneByName("GameLevel").isLoaded || SceneManager.GetSceneByName("EditorLevel").isLoaded);
	}

	public void DisplayGame() {
		if (!gameMenu.activeInHierarchy) {
			vsMenu.SetActive(false);
			gameMenu.SetActive(true);
        }
	}
	
	public void DisplayVideoSound() {
		if (!vsMenu.activeInHierarchy) {
			gameMenu.SetActive(false);
			vsMenu.SetActive(true);
        }
	}

	public void CloseMenu() {
		if (Time.timeScale <= Mathf.Epsilon) Time.timeScale = 1;
		SceneManager.UnloadSceneAsync("OptionsMenu");
	}
}
