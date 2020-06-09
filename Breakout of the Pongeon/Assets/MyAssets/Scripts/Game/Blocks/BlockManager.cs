using UnityEngine;
using UnityEngine.PlayerLoop;

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
        UpdateVisuals();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (isImmune) return;
        health -= 1;

        audioManager.Play("blockhit");
        audioManager.UpdatePitch("blockhit", Random.Range(0.3f, 1.5f));

        if (health <= 0) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            onDestroyed?.Invoke(); // onDestroyed has to be invoked AFTER the collider is disabled to avoid a StackOverflowError
        } else {
            onDamaged?.Invoke();
            UpdateVisuals();
        }
    }
    
    public Vector2Int getDimensions() {
        return new Vector2Int(width, height);
    }

    public void UpdateVisuals() {
        if (GetComponent<BlockColours>()) 
            spriteRenderer.material.color = 
                GetComponent<BlockColours>().ReturnBlockColour(health / maxHealth);
        if (GetComponent<BlockTextures>())
            spriteRenderer.sprite =
                GetComponent<BlockTextures>().ReturnBlockSprite(health / maxHealth);
    }

    public void ReceiveEffectDamage(int amount) {
        if (isImmune) return;
        health -= amount;
        if (health <= 0) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            onDestroyed?.Invoke();
        } else {
            onDamaged?.Invoke();
            UpdateVisuals();
        }
    }
}
