using UnityEngine;
using System.IO;
using System;

public static class LevelIO {
    public static string levelFilesPath = "/";
    public static string levelFilesEnding = ".bop";

    
    public static void printLevelPath() {
        Debug.Log(Application.dataPath + levelFilesPath);
    }

    public static  string[] getLevelsInDirectory () {
        return getLevelsInDirectory(false);
    }
    public static string[] getLevelsInDirectory (bool isUserPath) {
        try {
            string directory = Application.streamingAssetsPath;
            if (isUserPath)
                directory += "/User";
            directory += levelFilesPath;
            string[] allPaths = Directory.GetFiles(directory, "*"+levelFilesEnding);
            string[] returnString = new string[allPaths.Length];
            for (int i = 0; i < allPaths.Length; i++) {
                returnString[i] = allPaths[i].Substring(directory.Length, allPaths[i].Length - directory.Length - levelFilesEnding.Length);
            }
            return returnString;

        } catch (Exception ex) {
            return null;
        }
    } 

    public static bool SaveLevel(Level level) {
        return SaveLevel(level, false);
    }
    public static bool SaveLevel(Level level, bool isUserPath) {
        string directory = Application.streamingAssetsPath;
        if (isUserPath)
            directory += "/User";
        directory += levelFilesPath;
        LevelWriter writer = new LevelWriter(level, directory + level.name + levelFilesEnding);
        try {
            writer.WriteLevel();

        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return false;
        }
        return true;
    }
    public static Level LoadLevel(string name) {
        return LoadLevel(name, false);
    }
    public static Level LoadLevel(string name, bool isUserPath) {
        string directory = Application.streamingAssetsPath;
        if (isUserPath)
            directory += "/User";
        directory += levelFilesPath;

        LevelReader reader = new LevelReader(directory + name + levelFilesEnding);
        Level level;
        if (!File.Exists(directory + name + levelFilesEnding)) {
            return null;
        }
        try {
            level = reader.readLevel();
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return null;
        }
        return level;
    }

}