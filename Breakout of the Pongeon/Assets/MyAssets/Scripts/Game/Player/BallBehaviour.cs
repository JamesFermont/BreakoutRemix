using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
	public float baseSpeed = 200.0f;
	public float paddleSpeedIncreaseIncrement = 1.0f;
	public float deflectionStrength = 1.0f;
	private float speed;

	[HideInInspector]
	public bool hasBouncedThisFrame = false;

	private AudioManager audioManager;

	private void Awake() {
		audioManager = FindObjectOfType<AudioManager>();
	}

	private void OnEnable() {
		speed = baseSpeed;
	}

	private void LateUpdate() {
		hasBouncedThisFrame = false;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		audioManager.Play("bounce");

		GetComponent<ParticleSystem>().Play();

		hasBouncedThisFrame = true;
		
		if (other.gameObject.CompareTag("Paddle")) {
			speed += paddleSpeedIncreaseIncrement;
			
			float newX = GetDeflectedX(this.transform.position, other.transform.position,
				other.collider.bounds.size.x);
			
			// ball will always move up, therefore y is always 1, normalized to keep the ball speed the same
			// deflectionStrength will influence how far X can vary from 1 to create steeper deflection angles after normalization
			Vector2 newDirection = new Vector2(newX * deflectionStrength, 1).normalized;
			GetComponent<Rigidbody2D>().velocity = newDirection * (speed * Time.fixedDeltaTime);
		}
	}

	public void Launch() {
		GetComponent<Rigidbody2D>().velocity = Vector2.up * (speed * Time.fixedDeltaTime);
	}
	
	private float GetDeflectedX(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth) {
		// will return a float from -1 to +1 depending on where the ball hits the paddle (-1 for left edge, +1 for right edge)
		return (ballPosition.x - paddlePosition.x) / paddleWidth;
	}
}
