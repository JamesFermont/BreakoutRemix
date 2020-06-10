using UnityEditor;
using UnityEngine;

public class EditorLevelInfo : EditorWindow {


    private Vector2 buttonDimension = new Vector2(100, 30);
    private Vector2 buttonOffset = new Vector2(0, 50);
    private bool isDisabled = false;
    private string currentLevelName = "name";

    [MenuItem("Window/BOP/LevelInfo")]
    public static void Init() {
        EditorLevelInfo window = GetWindow<EditorLevelInfo>(false, "Level", true);
        window.Show();
    }


    public void OnGUI() {
        if (EditorApplication.isPlaying) {
            GUILayout.Label("YOU ARE IN PLAY MODE");
            return;
        }


        if (LevelManager.currentLevel != null) {
            currentLevelName = EditorGUILayout.TextField("Name: ", LevelManager.currentLevel.name);
            GUILayout.Label("Object Count:\t" + GridEditor.getObjectCount());

            if (LevelManager.currentLevelGO != null)
                LevelManager.UpdateName(currentLevelName);
        } else
            currentLevelName = EditorGUILayout.TextField("Create or Load a Level: ", currentLevelName);
        if (GUI.Button(new Rect(getButtonPosition(1, 0), buttonDimension), "Load"))
            LevelManager.LoadLevel(currentLevelName);

        if (GUI.Button(new Rect(getButtonPosition(0, 1), buttonDimension), "Create New"))
            LevelManager.CreateNewLevel();

        if (LevelManager.currentLevel == null) {
            isDisabled = true;
            EditorGUI.BeginDisabledGroup(isDisabled);
        }

        if (GUI.Button(new Rect(getButtonPosition(0, 0), buttonDimension), "Save"))
            LevelManager.SaveCurrentLevel();

        if (GUI.Button(new Rect(getButtonPosition(1, 1), buttonDimension), "Clear"))
            LevelManager.ResetCurrentLevel();

        if (isDisabled)
            EditorGUI.EndDisabledGroup();
    }

    private Vector2 getButtonPosition(int x, int y) {
        return buttonOffset + new Vector2(buttonDimension.x * x, buttonDimension.y * y);
    }


}