using System.Collections;
using UnityEngine;

public class TargetArea : MonoBehaviour {
	public int drainCount = 30;
	public UnitType unitType;
	public TargetManager targetManager;
	public bool hasNoCollision = true;

	private BallBehaviour ball;
	private SpriteRenderer spriteRenderer;
	private Color baseColor;
	private Coroutine drain;

	[HideInInspector]
	public bool isActivated;

	private void OnEnable() {
		ball = GameObject.FindWithTag("Ball").GetComponent<BallBehaviour>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		baseColor = spriteRenderer.material.color;
		GetComponent<BoxCollider2D>().isTrigger = hasNoCollision;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Ball")) {
			if (!isActivated) {
				isActivated = true;
				spriteRenderer.material.color = Color.green;
				targetManager.CheckCompleted();
				if (!targetManager.isCompleted) {
					if (unitType == UnitType.UNIT_TIME) drain = StartCoroutine(DrainEnergyOverTime(drainCount));
					if (unitType == UnitType.UNIT_BOUNCE) drain = StartCoroutine(DrainEnergyOverBounces(drainCount));
				}
			}
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Ball")) {
			if (!isActivated) {
				isActivated = true;
				spriteRenderer.material.color = Color.green;
				targetManager.CheckCompleted();
				if (!targetManager.isCompleted) {
					if (unitType == UnitType.UNIT_TIME) drain = StartCoroutine(DrainEnergyOverTime(drainCount));
					if (unitType == UnitType.UNIT_BOUNCE) drain = StartCoroutine(DrainEnergyOverBounces(drainCount));
				}
			}
		}
	}

	private void Update() {
		if (targetManager.isCompleted) {
			if (drain != null) StopCoroutine(drain);
			spriteRenderer.material.color = Color.green;
		}
	}

	private IEnumerator DrainEnergyOverTime(int duration) {
		float timeElapsed = 0.0f;

		while (timeElapsed < duration) {
			timeElapsed += Time.deltaTime;

			spriteRenderer.material.color = Color.Lerp(Color.green, Color.red, timeElapsed / duration);
			
			yield return null;
		}

		isActivated = false;
		spriteRenderer.material.color = baseColor;
	}

	private IEnumerator DrainEnergyOverBounces(int bounces) {
		int bouncesCounted = 0;
		
		while (bouncesCounted<bounces) {
			if (ball.hasBouncedThisFrame) {
				bouncesCounted++;
				spriteRenderer.material.color = Color.Lerp(Color.green, Color.red, (float)bouncesCounted / bounces);
			}

			yield return null;
		}

		isActivated = false;
		spriteRenderer.material.color = baseColor;
	}
}
