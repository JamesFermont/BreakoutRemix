using System;
using UnityEngine;

public class CircleExplosion : MonoBehaviour {
    public EffectType effectType = EffectType.ON_DAMAGED;
    public float explosionRadius = 2.0f;
    public int explosionDamage = 3;

    private AudioManager audioManager;
    private new ParticleSystem particleSystem;

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
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

        var particleSystemMain = particleSystem.main;
        particleSystemMain.startSpeed = explosionRadius * 10f;
    }

    public void PerformEffect() {
        audioManager.Play("cbomb");
        particleSystem.Play();
        
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
