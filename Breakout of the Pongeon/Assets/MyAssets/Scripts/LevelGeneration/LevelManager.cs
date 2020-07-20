using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class LevelManager {

    public static Level currentLevel;
    public static GameObject currentLevelGO;

    public static void CreateNewLevel() {
        CreateNewLevel("New Level");
    }
    public static void CreateNewLevel(string name) {
        if (currentLevelGO != null)
            Object.Destroy(currentLevelGO);
        MakeLevel(new Level(name, new Grid()));

    }

    public static void ResetCurrentLevel() {
        Object.Destroy(currentLevelGO);
        currentLevelGO = LevelGenerator.instance.Generate(currentLevel);
    }

    public static void LoadLevel(string levelName) {
        if (currentLevelGO != null) {
            Object.DestroyImmediate(currentLevelGO);
        }
        MakeLevel(LevelIO.LoadLevel(levelName));
    }

    private static void MakeLevel(Level level) {
        currentLevel = level;
        currentLevelGO = LevelGenerator.instance.Generate(currentLevel);
    }

    public static void SaveCurrentLevel() {
        if (currentLevel.name.Contains(" "))
            currentLevel.name = currentLevel.name.Replace(' ', '_');
        LevelIO.SaveLevel(currentLevel);
    }

    public static void UpdateGrid(Grid newGrid) {
        currentLevel.grid = newGrid;
        currentLevelGO = LevelGenerator.instance.Generate(currentLevel);
    }

    public static void UpdateName(string newName) {
        currentLevel.name = newName;
        currentLevelGO.name = newName;
    }

    public static void EndLevel() {
        LevelStatistics.instance.EndTracker();
        SceneManager.LoadSceneAsync("ResultScreen", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
}
