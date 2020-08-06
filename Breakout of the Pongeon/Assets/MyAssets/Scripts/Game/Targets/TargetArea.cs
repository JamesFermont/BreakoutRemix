using UnityEngine;
using UnityEngine.SceneManagement;
public class TargetArea : MonoBehaviour {
	public TargetManager targetManager;
	public bool hasNoCollision = true;
	
	[Range(1, 10000)]
	public int scoreOnTag;

	private BallBehaviour ball;
	public Animator animator;
	private Coroutine drain;

	[HideInInspector]
	public bool isActivated;

	private AudioManager audioManager;
	private BackgroundManager bgManager;
	private static readonly int IsActive = Animator.StringToHash("isActive");

	private void Awake() {
		audioManager = FindObjectOfType<AudioManager>();
		bgManager = FindObjectOfType<BackgroundManager>();
	}
	
	private void OnEnable() {
		ball = GameObject.FindWithTag("Ball").GetComponent<BallBehaviour>();
		animator = GetComponent<Animator>();
		GetComponent<BoxCollider2D>().isTrigger = hasNoCollision;
		if (!targetManager) targetManager = GameObject.FindWithTag("TargetManager").GetComponent<TargetManager>();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Ball")) {
			if (!isActivated) {
				isActivated = true;
				LevelStatistics.instance.AddLevelScore(scoreOnTag);
				animator.SetBool(IsActive, true);
				targetManager.CheckCompleted();
				if (!targetManager.isCompleted) {
					audioManager.Play("target_hit");
					PlayVideo();
				} else {
					audioManager.Play("final_target_hit");
                    if(SceneManager.GetActiveScene().name != "EditorLevel")
                        PlayVideo2();
				}
			}
		}
	}

	private void PlayVideo() {
		int rand = Random.Range(1, 4);
		switch (rand)
		{
			case 1: 
				bgManager.Play("damnyou");
				break;
			case 2:
				bgManager.Play("futile");
				break;
			case 3:
				bgManager.Play("succeed");
				break;
		}
	}

	private void PlayVideo2() {
		bgManager.Play("theend");
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Ball")) {
			if (!isActivated) {
				isActivated = true;
				targetManager.CheckCompleted();
			}
		}
	}
}
