using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallStart : MonoBehaviour {
    public Transform paddle;
    public Transform ball;
    public bool isBallStart;
    public bool isHidden;

    [SerializeField] private AudioManager audioManager;
    private Animator animator;
    private static readonly int IsBallLaunched = Animator.StringToHash("isBallLaunched");

    private void OnEnable() {
        if (!paddle) paddle = GameObject.FindWithTag("Paddle").transform;
        if (!ball) ball = GameObject.FindWithTag("Ball").transform;
        isBallStart = true;
        if (!audioManager)
            StartCoroutine(GetRefs());
    }

    private IEnumerator GetRefs() {
        yield return new WaitForSecondsRealtime(0.1f);
		
        audioManager = FindObjectOfType<AudioManager>();
        animator = GameObject.FindWithTag("Background").GetComponent<Animator>();
    }    
    

    private void LateUpdate() {
        if (isBallStart) {
            ball.position = paddle.position + new Vector3(0f, 0.5f, 0f);
            if (!isHidden) {
                if (Input.GetMouseButtonDown(0)) {
                    if (!audioManager.IsPlaying("bgm_game_01")) {
                        audioManager.Stop("bgm_menu");
                        audioManager.Play("bgm_game_01");
                    }

                    ball.GetComponent<BallBehaviour>().Launch();
                    if(SceneManager.GetActiveScene().name == "GameLevel")
                        FindObjectOfType<Timer>().hasStarted = true;
                    isBallStart = false;
                    animator.SetBool(IsBallLaunched, true);
                }
            }
        }
    }
}
