using System;
using UnityEngine;

public class BlockManager : MonoBehaviour {
    public int maxHealth = 3;
    private int health;

    private Color[] blockColours = new Color[3];

    private SpriteRenderer spriteRenderer;
    
    public delegate void OnDamaged();
    public event OnDamaged onDamaged;
    public delegate void OnDestroyed();
    public event OnDestroyed onDestroyed;

    private void Awake() {
        blockColours[0] = Color.red;
        blockColours[1] = Color.yellow;
        blockColours[2] = Color.green;
        health = maxHealth;
    }

    private void OnEnable() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = blockColours[health-1];
    }

    private void OnCollisionEnter2D(Collision2D other) {
        health -= 1;
        if (health <= 0) {
            onDestroyed?.Invoke();
            Destroy(this.gameObject);
        } else {
            onDamaged?.Invoke();
            spriteRenderer.material.color = blockColours[health - 1];
        }
    }

    public void ReceiveEffectDamage() {
        health -= 1;
        if (health <= 0) {
            onDestroyed?.Invoke();
            Destroy(this.gameObject);
        } else {
            onDamaged?.Invoke();
            spriteRenderer.material.color = blockColours[health - 1];
        }
    }
}
