using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
	public float baseSpeed = 200.0f;
	public float paddleSpeedIncreaseIncrement = 1.0f;
	public float deflectionStrength = 1.0f;
	private float speed;

	private void Awake() {
		speed = baseSpeed;
	}

	private void Start() {
		GetComponent<Rigidbody2D>().velocity = Vector2.up * speed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D other) { //To do: Absolute angle determination does not feel good.
		if (other.gameObject.CompareTag("Paddle")) {
			speed += paddleSpeedIncreaseIncrement;
			
			float newX = GetDeflectedX(this.transform.position, other.transform.position,
				other.collider.bounds.size.x);
			
			// ball will always move up, therefore y is always 1, normalized to keep the ball speed the same
			Vector2 newDirection = new Vector2(newX * deflectionStrength, 1).normalized;

			GetComponent<Rigidbody2D>().velocity = newDirection * speed * Time.deltaTime;
		}
	}

	private float GetDeflectedX(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth) {
		// will return a float from -1 to +1 depending on where the ball hits the paddle (-1 for left edge, +1 for right edge)
		return (ballPosition.x - paddlePosition.x) / paddleWidth;
	}
}
