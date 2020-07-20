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
        Constants.PERFECT_GAME_BONUS =  Mathf.Clamp(scoreForPerfectGame, 10, 10000);
        Constants.DROPPED_BALL_PENATLY = Mathf.Clamp(penaltyForDroppedBall, -10000, -10);
        Constants.SECONDS_PER_TIMEMOD_INTERVAL = Mathf.Clamp(secondsPerTimeModInterval, 1, 10);
        Constants.MIN_TIMEMOD = Mathf.Clamp(minTimeMod, 0.1f, 1f);
        Constants.MAX_TIMEMOD = Mathf.Clamp(maxTimeMod, 1f, 3f);
    }

    private void FixedUpdate() {
        if(Constants.PERFECT_GAME_BONUS != scoreForPerfectGame)
            Constants.PERFECT_GAME_BONUS = Mathf.Clamp(scoreForPerfectGame, 10, 10000);
        if(Constants.DROPPED_BALL_PENATLY != penaltyForDroppedBall)
            Constants.DROPPED_BALL_PENATLY = Mathf.Clamp(penaltyForDroppedBall, -10000, -10);
        if(Constants.SECONDS_PER_TIMEMOD_INTERVAL != secondsPerTimeModInterval)
            Constants.SECONDS_PER_TIMEMOD_INTERVAL = Mathf.Clamp(secondsPerTimeModInterval, 1, 10);
        if (Constants.MIN_TIMEMOD != minTimeMod)
            Constants.MIN_TIMEMOD = Mathf.Clamp(minTimeMod, 0.1f, 1f);
        if (Constants.MAX_TIMEMOD != maxTimeMod)
            Constants.MAX_TIMEMOD = Mathf.Clamp(maxTimeMod, 1f, 3f);
    }

}
