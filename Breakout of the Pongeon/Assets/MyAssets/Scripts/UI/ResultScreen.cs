using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour {
    public TMP_Text scoreCount;
    public TMP_Text timeMod;
    public TMP_Text finalScore;
    private void Start() {
        float[] results = LevelStatistics.instance.CalculateScore();

        scoreCount.text = "Score: " + LevelStatistics.instance.score;
        timeMod.text = "Time Mod: x" + results[0];
        finalScore.text = "Final Score: " + results[1];
    }
}
