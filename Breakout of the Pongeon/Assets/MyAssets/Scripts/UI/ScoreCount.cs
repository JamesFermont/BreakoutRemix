using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    private void Start() {
        GetComponent<TMP_Text>().text = "Highscore: " + LevelStatistics.instance.ReturnScore();
    }
}
