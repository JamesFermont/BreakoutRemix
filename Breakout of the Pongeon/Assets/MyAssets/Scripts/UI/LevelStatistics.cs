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
    private int score;

    public void StartTracker() {
        if (time < 1f) time = Time.fixedTime;
    }

    public void ResetTracker() {
        time = 0;
        blocksDestroyed = 0;
        score = 0;
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

    public int ReturnScore() {
        var ball = GameObject.FindWithTag("Ball").GetComponent<BallBehaviour>();
        float ballSpeedMod = ball.speedMod;
        float result = score * (1 / (blocksDestroyed + (time*ballSpeedMod))) * 100;
        return (int)result;
    }
}
