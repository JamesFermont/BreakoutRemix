using System.Collections;
using UnityEngine;

public enum Direction { DOWN, UP, LEFT, RIGHT }

public class UIFader : MonoBehaviour
{
    Direction currentDirection = Direction.DOWN;
    public float movementSpeed;
    public bool isMoving;

    public void FadeOut (Direction direction) {
        StartCoroutine(Move(ToVector(direction), Vector3.zero));
        currentDirection = direction;
    }

    public void FadeIn () {
        StartCoroutine(Move(ToVector(Opposite(currentDirection)), Vector3.zero));
    }


    private Vector3 ToVector(Direction direction) {
        switch (direction) {
            case Direction.DOWN:
                return Vector3.down;
            case Direction.UP:
                return Vector3.up;
            case Direction.LEFT:
                return Vector3.left;
            case Direction.RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
    private Vector3 TargetPosition(Direction direction) {
        switch (direction) {
            case Direction.DOWN:
                return new Vector3(0f, -720f, 0f);
            case Direction.UP:
                return new Vector3(0f, 720f, 0f);
            case Direction.LEFT:
                return new Vector3(-1280f, 0f, 0f);
            case Direction.RIGHT:
                return new Vector3(1280f, 0f, 0f);
            default:
                return Vector3.zero;
        }
    } 
    private Direction Opposite (Direction direction) {
        switch (direction) {
            case Direction.DOWN:
                return Direction.UP;
            case Direction.UP:
                return Direction.DOWN;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
            default:
                return direction;
        }
    }

    private IEnumerator Move (Vector3 moveDirection, Vector3 moveTarget) {
       while (!isMoving) {
                isMoving = true;
                while (transform.localPosition != moveTarget) {
                    transform.localPosition += moveDirection * movementSpeed;
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                isMoving = false;
            }        
    }


}
