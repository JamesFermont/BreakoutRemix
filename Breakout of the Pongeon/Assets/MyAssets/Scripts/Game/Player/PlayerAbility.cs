using System.Collections;
using UnityEngine;

public class PlayerAbility : MonoBehaviour {
	public int btEnergyCost;
	public int btDuration;

	[Range(0.1f, 1f)]
	public float btTimeScale;

	[HideInInspector] 
	public int energy;
	[Range (100, 100000)] 
	public int energyCap;
	
	public bool btIsActive;
	
	private AudioManager audioManager;

	private void OnEnable() {
		StartCoroutine(AudioManagerRef());
	}
	
	private IEnumerator AudioManagerRef() {
		float timeElapsed = 0f;

		while (timeElapsed < 0.1f) {
			timeElapsed += Time.deltaTime;

			yield return null;
		}
		
		audioManager = FindObjectOfType<AudioManager>();
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<DataPack>() == null) return;
		energy += other.GetComponent<DataPack>().energyGiven;
		LevelStatistics.instance.score += other.GetComponent<DataPack>().pointsGiven;
		if (energy > energyCap) energy = energyCap;
		Debug.Log(energy);
		Destroy(other.gameObject);
	}

	private void Update() {
		if (!Input.GetMouseButtonDown(1)) return;
		if (energy < btEnergyCost || btIsActive) return;
		energy -= btEnergyCost;
		ActivateBulletTime();
		Debug.Log("Bullet Time activate!!!!");
	}

	private void ActivateBulletTime() {
		audioManager.Play("bt_activate");
		StartCoroutine(BulletTime());
	}

	private IEnumerator BulletTime() {
		float timeElapsed = 0f;
		Time.timeScale = btTimeScale;
		btIsActive = true;

		while (timeElapsed < btDuration) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		Time.timeScale = 1f;
		btIsActive = false;
		Debug.Log("Bullet Time over!!!!");
	}
}
