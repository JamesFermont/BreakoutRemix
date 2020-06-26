using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {
	private GameObject gameMenu;
	private GameObject vsMenu;
	
	private void OnEnable() {
		gameMenu = GameObject.FindWithTag("GameMenu");
		vsMenu = GameObject.FindWithTag("VSMenu");
		vsMenu.SetActive(false);
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
		SceneManager.UnloadSceneAsync("OptionsMenu");
	}
}
