using UnityEngine;

public class KillPlaneBehavior : MonoBehaviour {
    private BackgroundManager bgManager;
    private static readonly int IsBallLaunched = Animator.StringToHash("isBallLaunched");

    private void Awake() {
        bgManager = FindObjectOfType<BackgroundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            other.gameObject.GetComponent<BallStart>().isBallStart = true;
            GameObject.FindWithTag("Background").GetComponent<Animator>().SetBool(IsBallLaunched, false);
            FindObjectOfType<AudioManager>().Play("death");
            LevelStatistics.instance.ballsDropped += 1;
            FindObjectOfType<TargetManager>().ApplyPenalty();
            other.GetComponent<BallBehaviour>().ResetTrail();
            PlayVideo();
        } else {
            Destroy(other.gameObject);
        }
    }
    
    private void PlayVideo() {
        int rand = Random.Range(1, 3);
        switch (rand)
        {
            case 1:
                bgManager.Play("laugh1");
                break;
            case 2:
                bgManager.Play("laugh2");
                break;
        }
    }
}
