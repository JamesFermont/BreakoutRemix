using TMPro;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    private void Start() {
        GetComponent<TMP_Text>().text = "Time spent: " + (int)LevelStatistics.instance.time + " seconds!";
    }
}
