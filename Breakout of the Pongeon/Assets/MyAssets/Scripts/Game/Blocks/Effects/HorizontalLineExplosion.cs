﻿using System;
using UnityEngine;

public class HorizontalLineExplosion : MonoBehaviour {
    public EffectType effectType = EffectType.ON_DAMAGED;
    public int explosionDamage = 3;

    private AudioManager audioManager;

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
        audioManager.Play("hlinebomb");
        
        Collider2D[] hit = Physics2D.OverlapBoxAll(this.transform.position,new Vector2(25.6f, 0.1f), 0f, 1<<8);

        foreach (Collider2D target in hit) {
            if (target.gameObject == this.gameObject) continue;
            target.GetComponent<BlockManager>()?.ReceiveEffectDamage(explosionDamage);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1,0,0, 0.5f);
        var position = this.transform.position;
        Gizmos.DrawRay(new Vector3(-6.4f, position.y, 0f), Vector3.right * 12.8f);
    }
}
