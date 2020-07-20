using System;
using UnityEngine;

public class Score {
    public string player;
    public string level;
    public int baseScore;
    public int timeTaken;

    public Score(string player, string level, int baseScore, int timeTaken) {
        this.player = player;
        this.level = level;
        this.baseScore = baseScore;
        this.timeTaken = timeTaken;
    }

    public bool isValid() {
        return (!string.IsNullOrWhiteSpace(player) && !string.IsNullOrWhiteSpace(level) && timeTaken >= 0);
    }

    public float finalScore() {
        return baseScore * timeMod();
    }

    public float timeMod() {
        // Time Mod = 1 + (+/- 0.1 for every secondsPerTimeModInterval seconds that the level time is below/above the target time, limited to between minTimeMod and maxTimeMod)
        return Mathf.Clamp(1 + (float)Math.Round((timeTaken - LevelTimeTargets.getTarget(level)) * (1f / Constants.SECONDS_PER_TIMEMOD_INTERVAL)) * 0.1f, Constants.MIN_TIMEMOD, Constants.MAX_TIMEMOD);
    }
}
