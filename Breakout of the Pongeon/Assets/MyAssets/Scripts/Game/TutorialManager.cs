using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	public Sprite[] tutorials;

	public int stage;

	private bool isLongTut;
	
	private void OnEnable() {
		isLongTut = true;
		StartCoroutine(WaitLevelCheck());
	}

	private IEnumerator WaitLevelCheck() {
		float timeElapsed = 0f;

		while (timeElapsed < 0.1f) {
			timeElapsed += Time.deltaTime;

			yield return null;
		}
		
		CheckLevel();
	}

	private void CheckLevel() {
		if (LevelManager.currentLevelGO.name != "Level_1-1")
			Destroy(gameObject);
		else
			PlayTutorial(0);
	}

	public void PlayTutorial(int id) {
		GetComponent<SpriteRenderer>().sprite = tutorials[id];
		if (id > 0)
			isLongTut = false;
		StartCoroutine(PlayTutorial());
		stage = id;
	}

	private IEnumerator PlayTutorial() {
		Time.timeScale = 0f;
		StartCoroutine(FadeIn());
		int skipCount = 0;
		
		if (isLongTut) {
			for (int i = 0; i < 8; i++) {
				if (Input.GetMouseButton(1)) {
					skipCount++;
				}
				if (skipCount >= 2) {
					Time.timeScale = 1f;
					StartCoroutine(FadeOut());
					if (stage >= 2) {
						Destroy(gameObject);
					}
					break;
				}
				yield return new WaitForSecondsRealtime(1f);
			}
		} else {
			for (int i = 0; i < 5; i++) {
				if (Input.GetMouseButton(1)) {
					skipCount++;
				}
				if (skipCount >= 2) {
					Time.timeScale = 1f;
					StartCoroutine(FadeOut());
					if (stage >= 2) {
						Destroy(gameObject);
					}
					break;
				}
				yield return new WaitForSecondsRealtime(1f);
			}
		}
		
		Time.timeScale = 1f;
		StartCoroutine(FadeOut());
		if (stage >= 2) {
			Destroy(gameObject);
		}
	}

	private IEnumerator FadeIn() {
		GetComponent<SpriteRenderer>().enabled = true;
		
		for (float f = 0.05f; f <= 1; f += 0.1f) {
			Color c = GetComponent<SpriteRenderer>().color;
			c.a = f;
			GetComponent<SpriteRenderer>().color = c;
			yield return new WaitForSecondsRealtime(0.05f);
		}
	}
	
	private IEnumerator FadeOut() {
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = GetComponent<SpriteRenderer>().color;
			c.a = f;
			GetComponent<SpriteRenderer>().color = c;
			yield return new WaitForSecondsRealtime(0.05f);
		}

		GetComponent<SpriteRenderer>().enabled = false;
	}
}