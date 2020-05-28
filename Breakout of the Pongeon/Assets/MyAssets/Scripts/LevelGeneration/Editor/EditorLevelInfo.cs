using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelOverviewEditor : EditorWindow {

    [MenuItem("Window/BOP/LevelInfo")]
    public static void Init() {
        LevelOverviewEditor window = GetWindow<LevelOverviewEditor>(false, "Level", true);
        window.Show();
    }

    private bool levelSelected = true;

    public void OnGUI() {
        if(LevelManager.instance.currentLevel != null) {
            LevelManager.instance.UpdateName(EditorGUILayout.TextField("Name: ", LevelManager.instance.currentLevel.name));
            GUILayout.Label("Object Count:\t" + GridEditor.instance.getObjectCount());
            if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(100, 0), new Vector2(100, 30)), "Load")) {
                LevelManager.instance.LoadLevel(LevelManager.instance.currentLevel.name);
            }
        } else {
            string loadLevelName = EditorGUILayout.TextField("Create or Load a Level: ", "name");
            if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(100, 0), new Vector2(100, 30)), "Load")) {
                LevelManager.instance.LoadLevel(loadLevelName);
            }
        }
        
        if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(0, 30), new Vector2(100, 30)), "Create New")) {
            LevelManager.instance.CreateNewLevel();
            Debug.Log("Loading Level:");
        }
        if (LevelManager.instance.currentLevel != null) {
            GUI.enabled = false;
        }
        if (GUI.Button(new Rect(new Vector2(0, 50), new Vector2(100, 30)), "Save")) {
            LevelManager.instance.SaveCurrentLevel();
            Debug.Log("Level Saved!");
        }
        if (GUI.Button(new Rect(new Vector2(0, 50) + new Vector2(100, 30), new Vector2(100, 30)), "Clear")) {
            LevelManager.instance.CreateNewLevel();
            Debug.Log("Loading Level:");
        }
        if (!GUI.enabled) {
            GUI.enabled = true;
        }
    }

}