using UnityEngine;
using System.IO;
using System;

public class LevelIO : MonoBehaviour {
    public const string levelFilesPath = "/";
    public const string levelFilesEnding = ".bop";
    public static LevelIO instance;

    public void printLevelPath () {
        Debug.Log(Application.dataPath + levelFilesPath);
    }

    public void Awake() {
        if (instance == null)
            instance = this;
    }

    public bool SaveLevel(Level level) {
        LevelWriter writer = new LevelWriter(level, Application.dataPath+ levelFilesPath + level.name + levelFilesEnding);
        try  {
            writer.WriteLevel();

        } catch (Exception ex){
            Debug.LogError(ex.ToString());
            return false;
        }
            return true;
    }

    public Level LoadLevel(string name) {
        LevelReader reader = new LevelReader(Application.dataPath+ levelFilesPath + name + levelFilesEnding);
        Level level;
        try {
            level = reader.readLevel();
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return null;
        }
        return level;
    }

}