using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvCompleteTransition : MonoBehaviour {

	[SerializeField] private int frameCount = 1000;
	
	private Image image;
	private int framesPassed;
	
	private void OnEnable() {
		image = transform.GetChild(0).GetComponent<Image>();
	}

	private void Start() {
		StartCoroutine(ShowText());
	}
	

	private IEnumerator ShowText() {
		framesPassed = 0;

		while (framesPassed < frameCount) {
			if (frameCount - framesPassed >= frameCount - 300) {
				image.color = new Color(1, 1, 1, Mathf.Min(framesPassed / 300f, 1f));
			}

			if (frameCount-framesPassed <= 300) {
				image.color = new Color(1, 1, 1, Mathf.Max((frameCount-framesPassed) / 300f, 0f));
			}
			framesPassed++;
			yield return null;
		}

		SceneManager.UnloadSceneAsync("LvCompleteTransition");
		SceneManager.LoadSceneAsync("ResultScreen", LoadSceneMode.Additive);
	}
}
