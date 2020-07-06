using UnityEngine;

public class ScoreModifiers : MonoBehaviour {
    [Range(10, 10000)]
    public int scoreForPerfectGame;
    [Range(-10000, -10)]
    public int penaltyForDroppedBall;
    [Range(1,10)]
    public int secondsPerTimeModInterval;

    [Range(0.1f, 1f)]
    public float minTimeMod;
    [Range(1f, 3f)]
    public float maxTimeMod;

    private void Awake() {
        Mathf.Clamp(scoreForPerfectGame, 10, 10000);
        Mathf.Clamp(penaltyForDroppedBall, -10000, -10);
        Mathf.Clamp(secondsPerTimeModInterval, 1, 10);
        Mathf.Clamp(minTimeMod, 0.1f, 1f);
        Mathf.Clamp(maxTimeMod, 1f, 3f);
    }
}
