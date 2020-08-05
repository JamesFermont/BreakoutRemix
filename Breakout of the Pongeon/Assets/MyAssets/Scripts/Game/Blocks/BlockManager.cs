using System.Collections;
using UnityEngine;

public class BlockManager : MonoBehaviour {
    public float maxHealth = 3;
    public bool isImmune = false;

    [Range(10, 10000)] 
    public int scoreOnDestroy;

    public int width;
    public int height;

    public GameObject dataPack;
    [Range(1,100)]
    public int dpBaseChance;
    public int dpBaseChanceIncrement;

    [HideInInspector]
    public float health;
    
    public delegate void OnDamaged();
    public event OnDamaged onDamaged;
    public delegate void OnDestroyed();
    public event OnDestroyed onDestroyed;

    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;

    private int dpDropChance;

    private void Awake() {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnEnable() {
        UpdateVisuals();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        PlaySounds();
        
        if (isImmune) return;
        health -= 1;

        if (health <= 0) {
            var dpChance = Random.Range(1, 100);

            if (gameObject.GetComponent<CircleExplosion>()) {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            } else {
                ToggleBlock(false);
            }
            onDestroyed?.Invoke(); // onDestroyed has to be invoked AFTER the collider is disabled to avoid a StackOverflowError
            LevelStatistics.instance.AddBlockScore(scoreOnDestroy);
            if (dpChance <= dpBaseChance + dpBaseChanceIncrement * LevelStatistics.instance.dpDropStep) {
                Instantiate(dataPack, this.transform);
                LevelStatistics.instance.dpDropStep = 0;
            } else {
                LevelStatistics.instance.dpDropStep += 1;
            }
        } else {
            onDamaged?.Invoke();
            UpdateVisuals();
        }
    }

    private void PlaySounds() {
        if (isImmune && !this.gameObject.GetComponent<TargetArea>()) {
            audioManager.Play("immune_hit");
            return;
        }

        if (this.gameObject.GetComponent<CircleExplosion>() != null) {
            audioManager.Play("bomb_hit");
            return;
        }
        
        if (this.gameObject.GetComponent<TargetArea>() == null && this.gameObject.GetComponent<ReviveBlock>() == null) {
            audioManager.Play("block_hit");
        }
    }

    public void ReceiveEffectDamage(int amount) {
        if (isImmune) return;
        health -= amount;
        if (health <= 0) {
            var dpChance = Random.Range(1, 100);

            if (gameObject.GetComponent<CircleExplosion>()) {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            } else {
                ToggleBlock(false);
            }
            onDestroyed?.Invoke(); // onDestroyed has to be invoked AFTER the collider is disabled to avoid a StackOverflowError
            LevelStatistics.instance.AddBlockScore(scoreOnDestroy);
            if (dpChance <= dpBaseChance + dpBaseChanceIncrement * LevelStatistics.instance.dpDropStep) {
                Instantiate(dataPack, this.transform);
                LevelStatistics.instance.dpDropStep = 0;
            } else {
                LevelStatistics.instance.dpDropStep += 1;
            }
        } else {
            onDamaged?.Invoke();
            UpdateVisuals();
        }
    }
    
    public Vector2Int GetDimensions() {
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

    public void ToggleBlock(bool state) {
        gameObject.GetComponent<SpriteRenderer>().enabled = state;
        gameObject.GetComponent<BoxCollider2D>().enabled = state;
    }
    
    public IEnumerator FadeOut() {
        for (float f = 1f; f >= 0; f -= 0.1f) {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = f;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        GetComponent<SpriteRenderer>().enabled = false;
    }
}
