using System;
using UnityEngine;

public class BlockManager : MonoBehaviour {
    public float maxHealth = 3;
    private float health;

    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;
    
    public delegate void OnDamaged();
    public event OnDamaged onDamaged;
    public delegate void OnDestroyed();
    public event OnDestroyed onDestroyed;

    private void Awake() {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnEnable() {
        if (GetComponent<BlockColours>() != null) spriteRenderer.material.color = GetComponent<BlockColours>().ReturnBlockColour( health/maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        health -= 1;

        audioManager.Play("blockhit");
        
        if (health <= 0) {
            onDestroyed?.Invoke();
            gameObject.SetActive(false);
        } else {
            onDamaged?.Invoke();
            if (GetComponent<BlockColours>() != null) spriteRenderer.material.color = GetComponent<BlockColours>().ReturnBlockColour(health/maxHealth);
        }
    }

    public void ReceiveEffectDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            onDestroyed?.Invoke();
            Destroy(this.gameObject);
        } else {
            onDamaged?.Invoke();
            if (GetComponent<BlockColours>() != null) spriteRenderer.material.color = GetComponent<BlockColours>().ReturnBlockColour(health/maxHealth);
        }
    }
}
