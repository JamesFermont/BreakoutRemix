using System;
using UnityEngine;

public class LevelStatistics {
    private static LevelStatistics Instance = null;
    public static LevelStatistics instance {
        get {
            if (Instance == null)
                Instance = new LevelStatistics();
            return Instance;
        }
    }

    public float time;
    public int blocksDestroyed;
    public int ballsDropped;
    public int score;

    public int dpDropStep;

    public void StartTracker() {
        if (time < 1f) time = Time.fixedTime;
    }

    public void ResetTracker() {
        time = 0;
        blocksDestroyed = 0;
        score = 0;
        ballsDropped = 0;
        dpDropStep = 0;
    }

    public void EndTracker() {
        var oldTime = time;
        time = Time.fixedTime - oldTime;
        var deadBlocks = 0;
        foreach (BoxCollider2D col in LevelManager.currentLevelGO.GetComponentsInChildren<BoxCollider2D>()) {
            if (col.enabled == false) {
                deadBlocks += 1;
            }
        }
        blocksDestroyed = deadBlocks;
    }

    public void AddScore(int delta) {
        score += delta;
    }

    public float[] CalculateScore() {
        float[] result = new float[2];
        var ball = GameObject.FindWithTag("Ball").GetComponent<BallBehaviour>();
        var scoreMods = GameObject.FindWithTag("LevelManager").GetComponent<ScoreModifiers>();
        float ballSpeedMod = ball.speedMod;
        float levelTime = time * ballSpeedMod;
        float targetTime = LevelManager.targetTime() * ballSpeedMod;
        Debug.Log(LevelManager.targetTime());

        if (ballsDropped == 0) {
            score += scoreMods.scoreForPerfectGame;
        }

        score -= ballsDropped * scoreMods.penaltyForDroppedBall;

        // Time Mod = 1 + (+/- 0.1 for every secondsPerTimeModInterval seconds that the level time is below/above the target time, limited to between minTimeMod and maxTimeMod)
        float timeMod = Mathf.Clamp(1 + (float)Math.Round(((int)targetTime - (int)levelTime) * (1f / scoreMods.secondsPerTimeModInterval)) * 0.1f,
                        scoreMods.minTimeMod, scoreMods.maxTimeMod);
        float finalScore = score * timeMod;

        result[0] = timeMod;
        result[1] = finalScore;

        return result;
    }
}
