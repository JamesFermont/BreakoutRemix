using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {
    public GameObject SingleScreen;
    public GameObject HighScoreScreen;
    public Button SingleScreenButton;
    public Button HighScoreButton;
    public GameObject navigationButtons;
    public TMP_Text hiddenMenu;
    public float timeSpent;
    public int blocksDestroyed;

    private void Start() {
        float[] results = LevelStatistics.instance.CalculateScore();

        timeSpent = LevelStatistics.instance.time;
        //blocksDestroyed = LevelStatistics.instance.blocksDestroyed;

        SetupScreens(results);
        SetupButtons();
    }

    private void SetupScreens(float[] results) {
        SingleScreen.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = "+" + results[4] + " points";
        SingleScreen.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = "+" + results[2] + " points";
        SingleScreen.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = "+" + results[3] + " points";
        if ((int)results[5] == 0)
            SingleScreen.transform.GetChild(3).GetChild(2).GetComponent<TMP_Text>().text = "+" + GameObject.FindWithTag("LevelManager").GetComponent<ScoreModifiers>().scoreForPerfectGame + " points";
        else {
            SingleScreen.transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = "Balls lost: ";
            SingleScreen.transform.GetChild(3).GetChild(2).GetComponent<TMP_Text>().text =
                "-" + GameObject.FindWithTag("LevelManager").GetComponent<ScoreModifiers>().penaltyForDroppedBall *
                (int) results[5] + " points";
        }
        SingleScreen.transform.GetChild(4).GetChild(2).GetComponent<TMP_Text>().text =
            "Time: " + (int) LevelStatistics.instance.time + " seconds";
        SingleScreen.transform.GetChild(4).GetChild(3).GetComponent<TMP_Text>().text =
            "Target: " + LevelManager.targetTime() + " seconds";
        SingleScreen.transform.GetChild(5).GetChild(2).GetComponent<TMP_Text>().text = "x" + results[0];
        SingleScreen.transform.GetChild(6).GetChild(2).GetComponent<TMP_Text>().text = (int)results[1] + " points";

        if (Scores.isInHighScore(LevelManager.currentLevel.name, (int)results[1])) {
            SingleScreen.transform.GetChild(7).gameObject.SetActive(false);
            SingleScreen.transform.GetChild(8).gameObject.SetActive(true);
            navigationButtons.SetActive(false);

        } else {
            SingleScreen.transform.GetChild(7).gameObject.SetActive(true);
            SingleScreen.transform.GetChild(8).gameObject.SetActive(false);
            navigationButtons.SetActive(true);
            SetupHighScoreScreen();

        }
    }
    private void SetupButtons() {
        SingleScreenButton.onClick.AddListener(delegate { SwitchToSingleScreen(); });
        HighScoreButton.onClick.AddListener(delegate { SwitchToHighScore(); });
        SingleScreen.transform.GetComponentInChildren<Button>().onClick.AddListener(delegate { SubmitScore(); });
    }
    
    private void SubmitScore() {
        string playerName = SingleScreen.transform.GetChild(8).GetComponentInChildren<TMP_InputField>().text;
        if (!string.IsNullOrWhiteSpace(playerName)) {
            SingleScreen.transform.GetComponentInChildren<Button>().interactable = false;
            Score score = new Score(playerName, LevelManager.currentLevel.name, LevelStatistics.instance.score, (int)LevelStatistics.instance.time);
            hiddenMenu.text = LevelManager.currentLevel.name + ": " + playerName + " reached " + score.finalScore() + " (base: " + LevelStatistics.instance.score + " time: " + LevelStatistics.instance.time + ")";
            Scores.SubmitScore(score);
            
            navigationButtons.SetActive(true);
            SetupHighScoreScreen();
        } else {
            hiddenMenu.text = "Improper Submission";
        }
    }
    private void SetupHighScoreScreen() {
        List<Score> levelScore = Scores.GetSortedScoresFromLevel(LevelManager.currentLevel.name);
        Debug.Log(levelScore.Count);
        for (int i = levelScore.Count-1; i >= 0; i--) {
            HighScoreScreen.transform.GetChild(levelScore.Count - (i+1)).GetComponentInChildren<TMP_Text>().text = levelScore[i].player + string.Concat(System.Linq.Enumerable.Repeat(".", 24 - (levelScore[i].player.Length + ((int)levelScore[i].finalScore()).ToString().Length))) + (int)levelScore[i].finalScore();
        }
    }
    private void SwitchToHighScore() {
        HighScoreButton.interactable = false;
        SingleScreen.SetActive(false);
        HighScoreScreen.SetActive(true);
        SingleScreenButton.interactable = true;
    }
    private void SwitchToSingleScreen() {
        SingleScreenButton.interactable = false;
        HighScoreScreen.SetActive(false);
        SingleScreen.SetActive(true);
        HighScoreButton.interactable = true;
    }

    private void OnDestroy() {
        FindObjectOfType<AudioManager>()?.UpdatePitch(1f);
    }
}
