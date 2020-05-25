using UnityEngine;

public class BlockManager : MonoBehaviour {
    public int health = 3;

    public delegate void OnDamaged();

    public event OnDamaged onDamaged;

    public delegate void OnDestroyed();

    public event OnDestroyed onDestroyed;
    
    private void OnCollisionEnter2D(Collision2D other) {
        health -= 1;

        if (health <= 0) {
            onDestroyed?.Invoke();
            Destroy(this.gameObject);
        } else {
            onDamaged?.Invoke();
        }
    }
}
