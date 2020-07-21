using System;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour {
    public TMP_Text scoreCount;
    public TMP_Text timeMod;
    public TMP_Text finalScore;

    public float timeSpent;
    public int blocksDestroyed;
    
    private void Start() {
        float[] results = LevelStatistics.instance.CalculateScore();

        timeSpent = LevelStatistics.instance.time;
        blocksDestroyed = LevelStatistics.instance.blocksDestroyed;
        
        scoreCount.text = "Score: " + LevelStatistics.instance.score;
        timeMod.text = "Time Mod: x" + results[0] + "(Time: " + timeSpent + " Target:" + LevelManager.targetTime + ")";
        finalScore.text = "Final Score: " + results[1];
    }
}
