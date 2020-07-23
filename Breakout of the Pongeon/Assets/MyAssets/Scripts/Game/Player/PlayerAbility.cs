using System.Collections;
using UnityEngine;

public class PlayerAbility : MonoBehaviour {
    public int btEnergyCost;
    public int btDuration;

    [Range(0.1f, 1f)]
    public float btTimeScale;

    [HideInInspector]
    public int energy;
    [Range(100, 100000)]
    public int energyCap;

    public bool btIsActive;

    private AudioManager audioManager;

    [SerializeField] private Sprite[] charges;
    [SerializeField] private GameObject chargeSprite;
    [SerializeField] private GameObject energyBar;
    [SerializeField] private SpriteRenderer energyDisplay;

    private void OnEnable() {
        StartCoroutine(AudioManagerRef());
    }

    private IEnumerator AudioManagerRef() {
        float timeElapsed = 0f;

        while (timeElapsed < 0.1f) {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<DataPack>() == null) return;
        energy += other.GetComponent<DataPack>().energyGiven;
        LevelStatistics.instance.score += other.GetComponent<DataPack>().pointsGiven;
        if (energy > energyCap) energy = energyCap;
        UpdateDisplay();
        Destroy(other.gameObject);
    }

    private void Update() {
        if (!Input.GetMouseButtonDown(1)) return;
        if (energy < btEnergyCost || btIsActive) return;
        energy -= btEnergyCost;
        UpdateDisplay();
        ActivateBulletTime();
    }

    private void ActivateBulletTime() {
        audioManager.Play("bt_activate");
        StartCoroutine(BulletTime());
    }

    private void UpdateDisplay() {
        var chargedBts = (int)(energy / 100f);
        chargeSprite.GetComponent<SpriteRenderer>().sprite = charges[chargedBts];
        if (energy != 200)
            energyBar.transform.localScale = new Vector3(1-((energy - chargedBts * 100f) / 100), 1, 1);
        else
            energyBar.transform.localScale = new Vector3(0, 1, 1);;
    }

    private IEnumerator BulletTime() {
        float timeElapsed = 0f;
        Time.timeScale = btTimeScale;
        btIsActive = true;
        energyDisplay.color = new Color(0, 1, 0.1f, 1);
        audioManager.UpdatePitch(0.7f);

        while (timeElapsed < btDuration) {
            timeElapsed += Time.deltaTime;
            energyBar.transform.localScale = new Vector3(timeElapsed/btDuration, 1, 1);
            yield return null;
        }

        Time.timeScale = 1f;
        btIsActive = false;
        energyDisplay.color = Color.white;
        audioManager.UpdatePitch(1f);
    }
}
