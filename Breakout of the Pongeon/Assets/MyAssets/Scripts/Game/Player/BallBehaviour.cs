using System;
using System.Collections;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    public float baseSpeed = 200.0f;
    public float paddleSpeedIncreaseIncrement = 1.0f;
    public float deflectionStrength = 1.0f;

    public PlayerAbility bTime;
    public Rigidbody2D rb;

    public GameObject firstRay;
    public GameObject secondRay;

    [HideInInspector]
    public float speedMod;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public bool hasBouncedThisFrame = false;

    private AudioManager audioManager;

    private void OnEnable() {
        StartCoroutine(AudioManagerRef());
    }

    private void LateUpdate() {
        hasBouncedThisFrame = false;
        firstRay.GetComponent<SpriteRenderer>().enabled = false;
        secondRay.GetComponent<SpriteRenderer>().enabled = false;
        if (bTime.btIsActive) {
            Vector2 position = this.transform.position;
            Debug.DrawRay(position, rb.velocity.normalized * 3f, Color.green);
            RaycastHit2D dir = Physics2D.Raycast(position, rb.velocity, 3f, LayerMask.GetMask("Paddle"));
            setFirstRay(rb.velocity, dir);
            if (dir) {
                Vector2 newDir;
                newDir = new Vector2(GetDeflectedX(dir.point, dir.transform.position, dir.collider.bounds.size.x) * deflectionStrength, 1);
                newDir = newDir.normalized * (speed * Time.fixedDeltaTime);
                Debug.DrawRay(dir.point, newDir, Color.red);
                setSecondRay(dir.point, newDir);
            }
        }
    }

    private void setFirstRay(Vector2 velocity, RaycastHit2D hit) {
        if (hit && hit.point != (Vector2)transform.position) {
            Vector2 hitVector = hit.point - (Vector2)transform.position;
            firstRay.transform.localPosition = hitVector * 0.5f;
            if (hitVector.magnitude != 0 && hitVector.x != 0)
                firstRay.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Asin(hitVector.y / hitVector.magnitude)) * Mathf.Rad2Deg * (Mathf.Sqrt(Mathf.Pow(hitVector.x, 2)) / hitVector.x);
            else
                firstRay.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            firstRay.transform.localScale = new Vector3(Math.Min(3f, hitVector.magnitude), 0.12f, 1f);
        } else {
            firstRay.transform.localPosition = 1.5f * velocity.normalized;
            if (velocity.x != 0)
                firstRay.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Asin(velocity.normalized.y * 3f / 3f)) * Mathf.Rad2Deg * (Mathf.Sqrt(Mathf.Pow(velocity.x, 2)) / velocity.x);
            else
                firstRay.transform.eulerAngles = new Vector3(0f, 0f, 90f);

            firstRay.transform.localScale = new Vector3(3f, 0.09f, 1f);
        }
        firstRay.GetComponent<SpriteRenderer>().enabled = true;

    }
    private void setSecondRay(Vector2 origin, Vector2 dir) {
        secondRay.transform.localPosition = (origin - (Vector2)transform.position) + 0.5f * dir;
        if (dir.magnitude == 0 || dir.x == 0)
            return;
        secondRay.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Asin(dir.y / dir.magnitude)) * Mathf.Rad2Deg * (Mathf.Sqrt(Mathf.Pow(dir.x, 2)) / dir.x);
        secondRay.transform.localScale = new Vector3(dir.magnitude, 0.09f, 1f);
        secondRay.GetComponent<SpriteRenderer>().enabled = true;

    }

    /* 
     * Vector 1
     *  position : if(Distance > 3) => (ballPos + 1.5f * rb.velocity.normalized) else => (hitPos - ballPos)
     *  rotation : (0f, 0f, )
     *  scale : (Math.Min(3f, hitPoint), 0.1f, 1f)
     * Vector 2
     * position
     * rotation
     * scale
    */


    private IEnumerator AudioManagerRef() {
        float timeElapsed = 0f;

        while (timeElapsed < 0.1f) {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        hasBouncedThisFrame = true;

        if (other.gameObject.CompareTag("Paddle")) {
            GetComponentInChildren<TrailRenderer>().startColor = Color.white;
            GetComponentInChildren<TrailRenderer>().endColor = Color.white;
            
            if (PlayerPrefs.HasKey("playMode") && PlayerPrefs.GetInt("playMode") == 1)
                speed += paddleSpeedIncreaseIncrement;

            float newX = GetDeflectedX(this.transform.position, other.transform.position,
                other.collider.bounds.size.x);

            // ball will always move up, therefore y is always 1, normalized to keep the ball speed the same
            // deflectionStrength will influence how far X can vary from 1 to create steeper deflection angles after normalization
            Vector2 newDirection = new Vector2(newX * deflectionStrength, 1).normalized;
            GetComponent<Rigidbody2D>().velocity = newDirection * (speed * Time.fixedDeltaTime);

            audioManager.Play("paddle_bounce");
            GetComponent<ParticleSystem>().Play();
        } else if (other.gameObject.GetComponent<CircleExplosion>()) {
            GetComponent<Rigidbody2D>().velocity *= 1.5f;
            GetComponentInChildren<TrailRenderer>().startColor = Color.red;
            GetComponentInChildren<TrailRenderer>().endColor = Color.yellow;
        } else {
            audioManager.Play("bounce");
            GetComponent<ParticleSystem>().Play();
        }
    }

    public void Launch() {
        LevelStatistics.instance.StartTracker();
        speedMod = PlayerPrefs.HasKey("ballSpeed") ? PlayerPrefs.GetFloat("ballSpeed") : 1;
        speed = baseSpeed * speedMod;
        GetComponent<Rigidbody2D>().velocity = Vector2.up * (speed * Time.fixedDeltaTime);
    }

    private float GetDeflectedX(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth) {
        // will return a float from -1 to +1 depending on where the ball hits the paddle (-1 for left edge, +1 for right edge)
        return (ballPosition.x - paddlePosition.x) / paddleWidth;
    }
}
