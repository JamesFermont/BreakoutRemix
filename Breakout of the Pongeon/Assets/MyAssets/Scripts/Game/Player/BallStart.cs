using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStart : MonoBehaviour {
    public Transform paddle;
    public Transform ball;
    public bool isBallStart;
    public bool isHidden;

    private AudioManager audioManager;

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnEnable() {
        if (!paddle) paddle = GameObject.FindWithTag("Paddle").transform;
        if (!ball) ball = GameObject.FindWithTag("Ball").transform;
        isBallStart = true;
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
    

    private void LateUpdate() {
        if (isBallStart) {
            ball.position = paddle.position + new Vector3(0f, 0.5f, 0f);
            if (!isHidden) {
                if (Input.GetMouseButtonDown(0)) {
                    audioManager.Stop("bgm_menu");
                    audioManager.Play("bgm_game_01");
                    ball.GetComponent<BallBehaviour>().Launch();
                    isBallStart = false;
                }
            }
        }
    }
}
