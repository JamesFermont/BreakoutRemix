using System;
using UnityEngine;

public class MouseMovement : MonoBehaviour {
    //Consts
    public enum MovementMode { SMOOTH, DIRECT }

    //Variables
    public MovementMode currentMode;
    public float movementSpeed; //only needed in MovementMode.Smooth
    private Camera mainCamera;
    public float bounds = 6.4f;


    public bool isHidden;

    //Methods
    private void OnEnable() {
        if (!mainCamera) mainCamera = Camera.main;
    }

    private void Update() {
        if (!isHidden) {
            switch (currentMode) {
                case MovementMode.DIRECT:
                    MoveDirect();
                    break;
                case MovementMode.SMOOTH:
                    MoveSmooth();
                    break;
            }
        }
    }
    private void MoveDirect() {
        Vector3 newPosition = transform.position;
        Vector3 mousePositionInWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Time.timeScale >= Mathf.Epsilon)
            newPosition.x = Mathf.Clamp(mousePositionInWorld.x, bounds * -1f, bounds);
        transform.position = newPosition;
    }
    private void MoveSmooth() {
        Vector3 newPosition = transform.position;
        Vector3 mousePositionInWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        if (Mathf.Abs(mousePositionInWorld.x - transform.position.x) <= Time.deltaTime * movementSpeed) {
            newPosition.x = Mathf.Clamp(mousePositionInWorld.x, bounds * -1f, bounds);
        } else {
            newPosition.x +=
                (mousePositionInWorld.x == newPosition.x ? 0f : Mathf.Clamp(((mousePositionInWorld.x - newPosition.x) / Mathf.Abs(mousePositionInWorld.x - newPosition.x)) * Time.deltaTime * movementSpeed, bounds * -1f, bounds));
        }

        transform.position = newPosition;
    }
}
