using UnityEngine;

public class TestEffect2 : MonoBehaviour {
	public EffectType effectType = EffectType.ON_DAMAGED;
	private void OnEnable() {
		if (effectType == EffectType.ON_DAMAGED) {
			gameObject.GetComponent<BlockManager>().onDamaged += PerformEffect;
		} else if (effectType == EffectType.ON_DESTROYED) {
			gameObject.GetComponent<BlockManager>().onDestroyed += PerformEffect;
		} else {
			gameObject.GetComponent<BlockManager>().onDamaged += PerformEffect;
			gameObject.GetComponent<BlockManager>().onDestroyed += PerformEffect;
		}
	}

	public void PerformEffect() {
		Debug.Log("Freeballin!");
	}
}