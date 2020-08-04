using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	public Sprite[] tutorials;

	public int stage;
	
	private int framesPassed;
	private int frameCount = 2500;
	
	private void OnEnable() {
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
		Debug.Log("Playing Tutorial with ID " + id);
		GetComponent<SpriteRenderer>().sprite = tutorials[id];
		if (id > 0) frameCount = 1500;
		StartCoroutine(PlayTutorial());
		stage = id;
	}

	private IEnumerator PlayTutorial() {
		framesPassed = 0;
		Time.timeScale = 0f;
		GetComponent<SpriteRenderer>().enabled = true;
		
		while (framesPassed < frameCount) {
			if (frameCount-framesPassed >= frameCount-50)
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Min(framesPassed / 40f, 1f));
			if (frameCount-framesPassed <= 50)
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Max((frameCount-framesPassed) / 40f, 0f));
			framesPassed++;
			yield return null;
		}
		
		Time.timeScale = 1f;
		GetComponent<SpriteRenderer>().enabled = false;
		if (stage >= 2) {
			Destroy(gameObject);
		}
	}
}