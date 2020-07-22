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

    public float timeSpent;
    public int blocksDestroyed;

    private void Start() {
        float[] results = LevelStatistics.instance.CalculateScore();

        timeSpent = LevelStatistics.instance.time;
        blocksDestroyed = LevelStatistics.instance.blocksDestroyed;

        SetupScreens(results);
        SetupButtons();
    }

    private void SetupScreens(float[] results) {
        SingleScreen.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = "Score: " + LevelStatistics.instance.score;
        SingleScreen.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = "Time Mod: x" + results[0] + "(Time: " + timeSpent + " Target:" + LevelManager.targetTime + ")";
        SingleScreen.transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = "Final Score: " + results[1];

        if (Scores.isInHighScore(LevelManager.currentLevel.name, (int)results[1])) {
            SingleScreen.transform.GetChild(3).gameObject.SetActive(false);
            SingleScreen.transform.GetChild(4).gameObject.SetActive(true);
            navigationButtons.SetActive(false);

        } else {
            SingleScreen.transform.GetChild(3).gameObject.SetActive(true);
            SingleScreen.transform.GetChild(4).gameObject.SetActive(false);
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
        string playerName = SingleScreen.transform.GetChild(4).GetComponentInChildren<TMP_InputField>().text;
        if (!string.IsNullOrWhiteSpace(playerName)) {
            SingleScreen.transform.GetComponentInChildren<Button>().interactable = false;
            Scores.SubmitScore(new Score(playerName, LevelManager.currentLevel.name, LevelStatistics.instance.score, (int)LevelStatistics.instance.time));
            navigationButtons.SetActive(true);
            SetupHighScoreScreen();
        }
    }

    private void SetupHighScoreScreen() {
        List<Score> levelScore = Scores.GetSortedScoresFromLevel(LevelManager.currentLevel.name);
        for (int i = 0; i < levelScore.Count; i++) {
            HighScoreScreen.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = levelScore[i].player + string.Concat(System.Linq.Enumerable.Repeat(".", 24 - (levelScore[i].player.Length + ((int)levelScore[i].finalScore()).ToString().Length))) + (int)levelScore[i].finalScore();
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
}
