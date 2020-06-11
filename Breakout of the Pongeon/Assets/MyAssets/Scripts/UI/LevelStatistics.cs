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

    public void StartTracker() {
        time = Time.fixedTime;
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
}
