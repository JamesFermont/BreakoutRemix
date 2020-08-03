using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	public Sprite[] tutorials;

	public int stage;
	
	private int framesPassed;
	private bool isPlaying;
	
	private void Awake() {
		//if (PlayerPrefs.HasKey("TutorialSeen") && PlayerPrefs.GetInt("TutorialSeen") == 1)
		//	Destroy(gameObject);

		PlayTutorial(0);
	}

	public void PlayTutorial(int id) {
		GetComponent<SpriteRenderer>().sprite = tutorials[id];
		StartCoroutine(PlayTutorial());
		stage = id;
	}

	private void FixedUpdate() {
		if (isPlaying) {
			framesPassed++;
		}
	}

	private IEnumerator PlayTutorial() {
		isPlaying = true;
		framesPassed = 0;
		Time.timeScale = 0f;
		Debug.Log("Tut Start");

		GetComponent<SpriteRenderer>().enabled = true;
		
		while (framesPassed < 2500) {
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Min(framesPassed / 30f, 1f));
			framesPassed++;
			yield return null;
		}
		
		Debug.Log("Tut End");

		isPlaying = false;
		Time.timeScale = 1f;
		GetComponent<SpriteRenderer>().enabled = false;
		if (stage >= 2) {
			Destroy(gameObject);
		}
	}
}