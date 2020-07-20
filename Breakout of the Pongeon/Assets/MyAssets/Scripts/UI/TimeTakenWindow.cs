using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTakenWindow : MonoBehaviour
{
    public Transform levelList;
    public Object levelTimePrefab;

    string[] levels;
    List<GameObject> entries;

    
    void OnEnable() {
        //
        GameObject currentLevelSign;
    }

    


    private void updateLists () {
        levels = LevelIO.getLevelsInDirectory();
        LevelTimeTargets.SaveLevels();
    }
}
