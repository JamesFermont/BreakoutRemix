using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
	private GameObject gameMenu;
	private GameObject vsMenu;
	
	[SerializeField] private Button gameButton;
	[SerializeField] private Button avButton;

	[SerializeField] private TMP_Text gameText;
	[SerializeField] private TMP_Text avText;

	private Color green;
	private Color black;

	private void OnEnable() {
		gameMenu = GameObject.FindWithTag("GameMenu");
		vsMenu = GameObject.FindWithTag("VSMenu");
		green = gameButton.gameObject.GetComponent<Image>().color;
		black = avButton.gameObject.GetComponent<Image>().color;
		vsMenu.SetActive(false);
	}

	public void DisplayGame() {
		if (!gameMenu.activeInHierarchy) {
			vsMenu.SetActive(false);
			gameMenu.SetActive(true);
			
			gameButton.GetComponent<Image>().color = green;
			gameText.color = black;
			avButton.GetComponent<Image>().color = black;
			avText.color = green;
        }
	}
	
	public void DisplayVideoSound() {
		if (!vsMenu.activeInHierarchy) {
			gameMenu.SetActive(false);
			vsMenu.SetActive(true);

			gameButton.GetComponent<Image>().color = black;
			gameText.color = green;
			avButton.GetComponent<Image>().color = green;
			avText.color = black;
		}
	}

	public void OnDestroy() {
		SceneManager.UnloadSceneAsync("OptionsMenu");
	}
}
