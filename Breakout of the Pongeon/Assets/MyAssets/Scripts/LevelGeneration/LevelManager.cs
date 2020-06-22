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
        MakeLevel(new Level(name, new Grid(Constants.GRID_WIDTH, Constants.GRID_HEIGHT)));
    }


    public static void ResetCurrentLevel() {
        Object.DestroyImmediate(currentLevelGO);
        MakeLevel(new Level(currentLevel.name, new Grid(Constants.GRID_WIDTH, Constants.GRID_HEIGHT)));
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
        SceneManager.LoadSceneAsync("UITest", LoadSceneMode.Additive);
        Time.timeScale = 0;
        GameObject.FindWithTag("Paddle").GetComponent<MouseMovement>().enabled = false;
    }
}
