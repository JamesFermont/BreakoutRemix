using TMPro;
using UnityEngine;

public class BlockCount : MonoBehaviour
{
    private void Start() {
        GetComponent<TMP_Text>().text = "Blocks Destroyed: " + LevelStatistics.instance.blocksDestroyed + "!";
    }
}
