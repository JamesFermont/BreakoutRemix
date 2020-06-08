using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelOverviewEditor : EditorWindow {

    [MenuItem("Window/BOP/LevelInfo")]
    public static void Init() {
        LevelOverviewEditor window = GetWindow<LevelOverviewEditor>(false, "Level", true);
        window.Show();
    }


    private bool isDisabled = false;
    private string currentLevelName = "name";
    public void OnGUI() {
        if (LevelManager.instance.currentLevel != null) {
                if(LevelManager.instance.currentLevel != null && LevelManager.instance.currentLevelGO != null) {
                    LevelManager.instance.UpdateName(EditorGUILayout.TextField("Name: ", LevelManager.instance.currentLevel.name));
            }
                    
                GUILayout.Label("Object Count:\t" + GridEditor.instance.getObjectCount());
                if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(100, 0), new Vector2(100, 30)), "Load")) {
                    LevelManager.instance.LoadLevel(LevelManager.instance.currentLevel.name);
                }
            } else {
                currentLevelName = EditorGUILayout.TextField("Create or Load a Level: ", currentLevelName);
                if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(100, 0), new Vector2(100, 30)), "Load")) {
                    LevelManager.instance.LoadLevel(currentLevelName);
                }
            }

            if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(0, 30), new Vector2(100, 30)), "Create New")) {
                LevelManager.instance.CreateNewLevel();
                Debug.Log("Loading Level:");
            }
            if (LevelManager.instance.currentLevel == null) {
                EditorGUI.BeginDisabledGroup(isDisabled);
                isDisabled = true;
            }
            if (GUI.Button(new Rect(new Vector2(0, 50), new Vector2(100, 30)), "Save")) {
                LevelManager.instance.SaveCurrentLevel();
                Debug.Log("Level Saved!");
            }
            if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(100, 30), new Vector2(100, 30)), "Clear")) {
                LevelManager.instance.ResetCurrentLevel();
            Debug.Log("Resetting Level!");
            }
            if (isDisabled) {
                EditorGUI.EndDisabledGroup();
            }
    }
        
}