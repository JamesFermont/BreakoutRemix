using UnityEditor;
using UnityEngine;

public class LevelOverviewEditor : EditorWindow {


    private Vector2 buttonDimension = new Vector2(100, 30);
    private Vector2 buttonOffset = new Vector2(0, 50);
    private bool isDisabled = false;
    private string currentLevelName = "name";


    [MenuItem("Window/BOP/LevelInfo")]
    public static void Init() {
        LevelOverviewEditor window = GetWindow<LevelOverviewEditor>(false, "Level", true);
        window.Show();
    }


    LevelManager levelManager = LevelManager.instance;

    public void OnGUI() {
        if (EditorApplication.isPlaying) {
            GUILayout.Label("YOU ARE IN PLAY MODE");
            return;
        }


        if (levelManager.currentLevel != null) {
            currentLevelName = EditorGUILayout.TextField("Name: ", levelManager.currentLevel.name);
            GUILayout.Label("Object Count:\t" + GridEditor.instance.getObjectCount());

            if (levelManager.currentLevelGO != null)
                levelManager.UpdateName(currentLevelName);
        } else
            currentLevelName = EditorGUILayout.TextField("Create or Load a Level: ", currentLevelName);
        if (GUI.Button(new Rect(getButtonPosition(1, 0), buttonDimension), "Load"))
            levelManager.LoadLevel(currentLevelName);

        if (GUI.Button(new Rect(getButtonPosition(0, 1), buttonDimension), "Create New"))
            levelManager.CreateNewLevel();

        if (levelManager.currentLevel == null) {
            isDisabled = true;
            EditorGUI.BeginDisabledGroup(isDisabled);
        }

        if (GUI.Button(new Rect(getButtonPosition(0, 0), buttonDimension), "Save"))
            levelManager.SaveCurrentLevel();

        if (GUI.Button(new Rect(getButtonPosition(1, 1), buttonDimension), "Clear"))
            levelManager.ResetCurrentLevel();

        if (isDisabled)
            EditorGUI.EndDisabledGroup();
    }

    private Vector2 getButtonPosition(int x, int y) {
        return buttonOffset + new Vector2(buttonDimension.x * x, buttonDimension.y * y);
    }


}