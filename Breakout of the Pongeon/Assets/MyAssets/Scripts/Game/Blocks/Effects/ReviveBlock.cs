using System.Collections;
using UnityEngine;

public class ReviveBlock : MonoBehaviour {
	public int reviveCountMin = 30;
	public int reviveCountMax = 50;
	public int reviveHpAmount;
	public UnitType unitType;
	
	private BlockManager blockManager;
	private BallBehaviour ball;

	private void OnEnable() {
		blockManager = gameObject.GetComponent<BlockManager>();
		blockManager.onDestroyed += PerformEffect;
		ball = GameObject.FindWithTag("Ball").GetComponent<BallBehaviour>();
	}

	public void PerformEffect() {
		if (reviveCountMax < reviveCountMin) reviveCountMax = reviveCountMin;
		if (unitType == UnitType.UNIT_TIME) StartCoroutine(ReviveOverTime(Random.Range(reviveCountMin, reviveCountMax)));
		if (unitType == UnitType.UNIT_BOUNCE) StartCoroutine(ReviveOverBounces(Random.Range(reviveCountMin, reviveCountMax)));
	}

	private IEnumerator ReviveOverTime(int duration) {
		float timeElapsed = 0f;
		

		while (timeElapsed < duration) {
			timeElapsed += Time.deltaTime;

			yield return null;
		}
		
		blockManager.health = reviveHpAmount;
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}

	private IEnumerator ReviveOverBounces(int bounces) {
		int bouncesCounted = 0;

		while (bouncesCounted < bounces) {
			if (ball.hasBouncedThisFrame) {
				bouncesCounted++;
			}

			yield return null;
		}
		
		blockManager.health = reviveHpAmount;
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}
}
