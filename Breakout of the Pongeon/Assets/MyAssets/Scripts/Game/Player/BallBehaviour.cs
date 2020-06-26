using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
	public float baseSpeed = 200.0f;
	public float paddleSpeedIncreaseIncrement = 1.0f;
	public float deflectionStrength = 1.0f;
	
	[HideInInspector]
	public float speedMod;
	
	[HideInInspector]
	public float speed;

	[HideInInspector]
	public bool hasBouncedThisFrame = false;

	private AudioManager audioManager;

	private void Awake() {
		audioManager = FindObjectOfType<AudioManager>();
	}

	private void LateUpdate() {
		hasBouncedThisFrame = false;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		hasBouncedThisFrame = true;
		
		if (other.gameObject.CompareTag("Paddle")) {
			if (PlayerPrefs.HasKey("playMode") && PlayerPrefs.GetInt("playMode") == 1)
				speed += paddleSpeedIncreaseIncrement;

			float newX = GetDeflectedX(this.transform.position, other.transform.position,
				other.collider.bounds.size.x);
			
			// ball will always move up, therefore y is always 1, normalized to keep the ball speed the same
			// deflectionStrength will influence how far X can vary from 1 to create steeper deflection angles after normalization
			Vector2 newDirection = new Vector2(newX * deflectionStrength, 1).normalized;
			GetComponent<Rigidbody2D>().velocity = newDirection * (speed * Time.fixedDeltaTime);
			
			audioManager.Play("paddle_bounce");
			GetComponent<ParticleSystem>().Play();
		} else {
			audioManager.UpdatePitch("bounce", 1f + 0.005f * (speed - baseSpeed));
			audioManager.Play("bounce");
			GetComponent<ParticleSystem>().Play();
		}
	}

	public void Launch() {
		LevelStatistics.instance.StartTracker();
		speedMod = PlayerPrefs.HasKey("ballSpeed") ? PlayerPrefs.GetFloat("ballSpeed") : 1;
		speed = baseSpeed * speedMod;
		GetComponent<Rigidbody2D>().velocity = Vector2.up * (speed * Time.fixedDeltaTime);
	}
	
	private float GetDeflectedX(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth) {
		// will return a float from -1 to +1 depending on where the ball hits the paddle (-1 for left edge, +1 for right edge)
		return (ballPosition.x - paddlePosition.x) / paddleWidth;
	}
}
