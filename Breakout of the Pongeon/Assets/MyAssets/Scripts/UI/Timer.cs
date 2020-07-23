using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
	[SerializeField] private TMP_Text timer;
	private float time;

	public bool hasStarted;
	
	private void Update() {
		if (!hasStarted) return;
		time += Time.deltaTime;
		timer.text = "" + (int) time;
	}
}
