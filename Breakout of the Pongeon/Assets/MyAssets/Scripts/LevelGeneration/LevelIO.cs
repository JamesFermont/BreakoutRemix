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
        try {
            string[] allPaths = Directory.GetFiles(Application.streamingAssetsPath + levelFilesPath, "*.bop");
            string[] returnString = new string[allPaths.Length];
            for(int i = 0; i < allPaths.Length; i++) {
                returnString[i] = allPaths[i].Substring(Application.streamingAssetsPath.Length + levelFilesPath.Length, allPaths[i].Length - Application.streamingAssetsPath.Length - levelFilesPath.Length - ".bop".Length);
            }




            return returnString;

        } catch (Exception ex){
            return null;
        }
    }


    public static bool SaveLevel(Level level) {
        LevelWriter writer = new LevelWriter(level, Application.streamingAssetsPath + levelFilesPath + level.name + levelFilesEnding);
        try {
            writer.WriteLevel();

        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return false;
        }
        return true;
    }

    public static Level LoadLevel(string name) {
        LevelReader reader = new LevelReader(Application.streamingAssetsPath + levelFilesPath + name + levelFilesEnding);
        Level level;
        if(!File.Exists(Application.streamingAssetsPath + levelFilesPath + name + levelFilesEnding)) {
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