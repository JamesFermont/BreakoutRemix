using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvCompleteTransition : MonoBehaviour {
	private Image image;

	private void OnEnable() {
		image = transform.GetChild(0).GetComponent<Image>();
	}

	private void Start() {
		StartCoroutine(ShowText());
	}

	private IEnumerator ShowText() {
		FindObjectOfType<AudioManager>().UpdatePitch(1f);
		StartCoroutine(FadeIn());
		
		yield return new WaitForSecondsRealtime(5f);

		StartCoroutine(FadeOut());
	}
	
	private IEnumerator FadeIn() {
		for (float f = 0.05f; f <= 1; f += 0.1f) {
			Color c = image.color;
			c.a = f;
			image.color = c;
			yield return new WaitForSecondsRealtime(0.05f);
		}
	}
	
	private IEnumerator FadeOut() {
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = image.color;
			c.a = f;
			image.color = c;
			yield return new WaitForSecondsRealtime(0.05f);
		}
		
		SceneManager.UnloadSceneAsync("LvCompleteTransition");
		SceneManager.LoadSceneAsync("ResultScreen", LoadSceneMode.Additive);
	}
}
