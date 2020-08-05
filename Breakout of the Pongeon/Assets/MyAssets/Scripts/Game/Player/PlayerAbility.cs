using System;
using System.Collections;
using UnityEngine;

public class PlayerAbility : MonoBehaviour {
    [SerializeField] private int btEnergyCost;
    [SerializeField] private int btDuration;

    [Range(0.1f, 1f)]
    public float btTimeScale;

    [HideInInspector]
    public int energy;
    [Range(100, 100000)]
    public int energyCap;

    public bool btIsActive;

    private AudioManager audioManager;
    private TargetManager targetManager;
    
    [SerializeField] private GameObject energyBar;
    [SerializeField] private SpriteRenderer energyDisplay;

    private void OnEnable() {
        StartCoroutine(GetRefs());
    }

    private IEnumerator GetRefs() {
        float timeElapsed = 0f;

        while (timeElapsed < 0.1f) {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        audioManager = FindObjectOfType<AudioManager>();
        targetManager = FindObjectOfType<TargetManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<DataPack>() == null) return;
        energy += other.GetComponent<DataPack>().energyGiven;
        LevelStatistics.instance.AddDataScore(other.GetComponent<DataPack>().pointsGiven);
        if (energy > energyCap) energy = energyCap;
        UpdateDisplay();
        Destroy(other.gameObject);
        
        if (!FindObjectOfType<TutorialManager>()) return;
        TutorialManager tutorialManager = FindObjectOfType<TutorialManager>().GetComponent<TutorialManager>();
        if (tutorialManager.stage != 2) {
            tutorialManager.gameObject.transform.position = this.gameObject.transform.position;
            tutorialManager.PlayTutorial(2);
        }
    }

    private void Update() {
        if (!Input.GetMouseButtonDown(1)) return;
        if (energy < btEnergyCost || btIsActive) return;
        energy -= btEnergyCost;
        UpdateDisplay();
        ActivateBulletTime();
    }

    private void FixedUpdate() {
        if (energy == 100) {
            DisplayFullCharge();
        }
    }

    private void ActivateBulletTime() {
        //audioManager.Play("bt_activate");
        StartCoroutine(BulletTime());
    }

    private void UpdateDisplay() {
        energyBar.transform.localScale = new Vector3(1-energy/100f, 1, 1);
    }

    private void DisplayFullCharge() {
        energyDisplay.color = new Color(1, 1, 1, (150f +Mathf.PingPong(Time.time*70, 105))/255f);
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
            if (targetManager.isCompleted) {
                break;
            }

            yield return null;
        }

        if (!targetManager.isCompleted)
            Time.timeScale = 1f;
        btIsActive = false;
        energyDisplay.color = new Color(1, 1, 1, 150f/255f);
        audioManager.UpdatePitch(1f);
        UpdateDisplay();
    }
}
