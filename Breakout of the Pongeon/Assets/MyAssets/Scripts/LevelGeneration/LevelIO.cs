using UnityEngine;
using System.IO;
using System;

public class LevelIO {
    public const string levelFilesPath = "/";
    public const string levelFilesEnding = ".bop";

    private static LevelIO Instance = null;
    public static LevelIO instance {
        get {
            if (Instance == null)
                Instance = new LevelIO();
            return Instance;
        }
    }

    public void printLevelPath() {
        Debug.Log(Application.dataPath + levelFilesPath);
    }

    public bool SaveLevel(Level level) {
        LevelWriter writer = new LevelWriter(level, Application.dataPath + levelFilesPath + level.name + levelFilesEnding);
        try {
            writer.WriteLevel();

        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return false;
        }
        return true;
    }

    public Level LoadLevel(string name) {
        LevelReader reader = new LevelReader(Application.dataPath + levelFilesPath + name + levelFilesEnding);
        Level level;
        if(!File.Exists(Application.dataPath + levelFilesPath + name + levelFilesEnding)) {
            Debug.Log("A Level of such name, doesn't exist!");
            return null;
        }
        Debug.Log("Let's continue!");
        try {
            level = reader.readLevel();
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return null;
        }
        return level;
    }

}