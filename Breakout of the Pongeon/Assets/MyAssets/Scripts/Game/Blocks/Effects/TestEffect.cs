using System;
using UnityEngine;
using UnityEngine.Rendering;

public class TestEffect : MonoBehaviour {
	public enum EffectType {ON_DAMAGED, ON_DESTROYED, ON_ALL}
	public EffectType effectType = EffectType.ON_DAMAGED;
	private void OnEnable() {
		if (effectType == EffectType.ON_DAMAGED) {
			//gameObject.GetComponent<BlockManager>().onDamaged -= PerformEffect;
			gameObject.GetComponent<BlockManager>().onDamaged += PerformEffect;
		} else if (effectType == EffectType.ON_DESTROYED) {
			//gameObject.GetComponent<BlockManager>().onDestroyed -= PerformEffect;
			gameObject.GetComponent<BlockManager>().onDestroyed += PerformEffect;
		} else {
			//gameObject.GetComponent<BlockManager>().onDamaged -= PerformEffect;
			gameObject.GetComponent<BlockManager>().onDamaged += PerformEffect;
			//gameObject.GetComponent<BlockManager>().onDestroyed -= PerformEffect;
			gameObject.GetComponent<BlockManager>().onDestroyed += PerformEffect;
		}
	}

	public void PerformEffect() {
		Debug.Log("Noooooooooooooo!");
	}
}
