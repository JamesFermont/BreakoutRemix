using System;
using UnityEngine;

public class BallStart : MonoBehaviour {
    public Transform paddle;
    public Transform ball;
    public bool isBallStart;

    private void OnEnable() {
        if (!paddle) paddle = GameObject.FindWithTag("Paddle").transform;
        if (!ball) ball = GameObject.FindWithTag("Ball").transform;
        isBallStart = true;
    }

    private void LateUpdate() {
        if (isBallStart) {
            ball.position = paddle.position + new Vector3(0f, 0.5f, 0f);
            if (Input.GetMouseButtonDown(0)) {
                ball.GetComponent<BallBehaviour>().Launch();
                isBallStart = false;
            }
        }
    }
}
