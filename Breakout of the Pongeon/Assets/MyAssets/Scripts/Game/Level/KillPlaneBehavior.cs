using UnityEngine;

public class KillPlaneBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponent<BallStart>().isBallStart = true;
        FindObjectOfType<AudioManager>().Play("death");
        LevelStatistics.instance.ballsDropped += 1;
    }
}
