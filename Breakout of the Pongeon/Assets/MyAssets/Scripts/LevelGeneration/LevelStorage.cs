using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class LevelStorage : MonoBehaviour {
    public Level level;
    public GameObject levelGO;

    public void Awake() {
        if (LevelManager.currentLevel == null && level != null)
            LevelManager.currentLevel = level;
        if (LevelManager.currentLevelGO == null && levelGO != null)
            LevelManager.currentLevelGO = levelGO;
        


    }

    public void Update() {
#if UNITY_EDITOR
        if(!EditorApplication.isPlaying) {
            if (LevelManager.currentLevel != null && Grid.toGridPosition(new Vector3(4f, 4f)) == Vector2Int.zero) {
                LevelManager.currentLevel = level;
                LevelManager.currentLevelGO = levelGO;
            }
        }
#endif
        if (((LevelManager.currentLevel == null || Grid.width == 0 )&& level != null) || (LevelManager.currentLevelGO == null && levelGO != null)) {
            LevelManager.currentLevel = level;
            LevelManager.currentLevelGO = levelGO;

        }



    }
}
