using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    private void Update() {
        scoreText.text = "" + LevelStatistics.instance.score;
    }
}
