using System;
using System.Collections;
using UnityEngine;

public class CircleExplosion : MonoBehaviour {
    public EffectType effectType = EffectType.ON_DAMAGED;
    public float explosionRadius = 2.0f;
    public int explosionDamage = 3;

    private AudioManager audioManager;
    private static readonly int ToExplode = Animator.StringToHash("toExplode");

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }

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

    private void PerformEffect() {
        audioManager.Play("bomb_detonate");
        this.gameObject.GetComponent<Animator>().SetBool(ToExplode, true);

        Collider2D[] hit = Physics2D.OverlapCircleAll(gameObject.transform.position, explosionRadius, 1<<8);
        foreach (Collider2D target in hit) {
            if (target.gameObject == this.gameObject) continue;
            target.GetComponent<BlockManager>()?.ReceiveEffectDamage(explosionDamage);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1,0,0,0.50f);
        Gizmos.DrawSphere(gameObject.transform.position, explosionRadius);
    }
}
