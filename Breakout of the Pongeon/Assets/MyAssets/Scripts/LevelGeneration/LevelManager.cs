using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager {

    private static LevelManager Instance = null;
    public static LevelManager instance {
        get {
            if (Instance == null)
                Instance = new LevelManager();
            return Instance;
        }
    }


    public Level currentLevel;
    public GameObject currentLevelGO;

    public void CreateNewLevel() {
        CreateNewLevel("New Level");
    }
    public void CreateNewLevel(string name) {
        MakeLevel(new Level(name, new Grid(Constants.GRID_WIDTH, Constants.GRID_HEIGHT)));
    }


    public void ResetCurrentLevel() {
        Object.DestroyImmediate(currentLevelGO);
        MakeLevel(new Level(currentLevel.name, new Grid(Constants.GRID_WIDTH, Constants.GRID_HEIGHT)));
    }

    public void LoadLevel(string levelName) {
        if (currentLevelGO != null) {
            Object.DestroyImmediate(currentLevelGO);
        }

        MakeLevel(LevelIO.instance.LoadLevel(levelName));
        Debug.Log(currentLevelGO);
    }

    private void MakeLevel(Level level) {
        currentLevel = level;
        currentLevelGO = LevelGenerator.instance.Generate(currentLevel);
    }

    public void SaveCurrentLevel() {
        LevelIO.instance.SaveLevel(currentLevel);
    }

    public void UpdateGrid(Grid newGrid) {
        currentLevel.grid = newGrid;
        currentLevelGO = LevelGenerator.instance.Generate(currentLevel);
    }

    public void UpdateName(string newName) {
        currentLevel.name = newName;
        currentLevelGO.name = newName;
    }


}
