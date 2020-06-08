using UnityEngine;

public class BlockManager : MonoBehaviour {
    public float maxHealth = 3;
    public bool isImmune = false;
    
    public int width;
    public int height;

    [HideInInspector]
    public float health;

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
        if (isImmune) return;
        health -= 1;

        audioManager.Play("blockhit");
        audioManager.UpdatePitch("blockhit", Random.Range(0.3f, 1.5f));

        if (health <= 0) {
            onDestroyed?.Invoke();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else {
            onDamaged?.Invoke();
            if (GetComponent<BlockColours>() != null)
                spriteRenderer.material.color = GetComponent<BlockColours>().ReturnBlockColour(health / maxHealth);
        }
    }
    
    public Vector2Int getDimensions() {
        return new Vector2Int(width, height);
    }

    public void ReceiveEffectDamage(int amount) {
        if (isImmune) return;
        health -= amount;
        if (health <= 0) {
            onDestroyed?.Invoke();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else {
            onDamaged?.Invoke();
            if (GetComponent<BlockColours>() != null) spriteRenderer.material.color = GetComponent<BlockColours>().ReturnBlockColour(health/maxHealth);
        }
    }
}
