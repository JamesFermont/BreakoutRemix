using UnityEditor;
using UnityEngine;

public class LevelOverviewEditor : EditorWindow {

    

    string levelName = "levelname";

    [MenuItem("Window/BOP/LevelInfo")]
    public static void Init( ) {
                    LevelOverviewEditor window = GetWindow<LevelOverviewEditor>(false, "Level", true);
            window.Show();
    }

    public void OnGUI () {
        if(LevelGenerator.instance.currentLevel != null) {
            levelName = EditorGUILayout.TextField("Name: ", LevelGenerator.instance.currentLevelGameObject.name);
            GUILayout.Label("WRONG Object Count:\t"+ LevelGenerator.instance.currentLevel.height);
        }
            


    }

}