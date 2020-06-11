﻿using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    //Consts
    public enum MovementMode {SMOOTH, DIRECT}

    //Variables
    public MovementMode currentMode;
    public float movementSpeed; //only needed in MovementMode.Smooth
    private Vector3 newPosition;
    private Vector3 mousePositionInWorld;
    private Camera mainCamera;
    
    //Methods
    private void OnEnable() {
        if (!mainCamera) mainCamera = Camera.main;
    }
    
    private void Update() {

        newPosition = transform.position;
        mousePositionInWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        switch (currentMode) {
            case MovementMode.DIRECT:
                newPosition.x = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                break;
            case MovementMode.SMOOTH:
                if(Mathf.Abs(mousePositionInWorld.x-transform.position.x) <= Time.deltaTime * movementSpeed) {
                    newPosition.x = mousePositionInWorld.x;
                } else {
                    Vector3 position = transform.position;
                    newPosition.x += 
                        (mousePositionInWorld.x == position.x? 0f : (mousePositionInWorld.x - position.x) / Mathf.Abs(mousePositionInWorld.x - position.x)) * Time.deltaTime * movementSpeed;
                }
                break;
        }

        transform.position = newPosition;
    }
}