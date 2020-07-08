using System.IO;
using UnityEngine;

public static class ScoreIO
{
    public static string scoreFilePath = "/";
    public static string scoreFileName = "notquitehighscores";
    public static string scoreFileEnding = ".nqi";
    
    private static BinaryReader reader;
    private static BinaryWriter writer;


    private static string scoreFile() {
        return Application.dataPath + scoreFilePath + scoreFileName + scoreFileEnding;
    }

    public static bool SaveScores(Level level) {
        return false;
        /*LevelWriter writer = new LevelWriter(level, Application.dataPath + levelFilesPath + level.name + levelFilesEnding);
        try {
            writer.WriteLevel();

        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return false;
        }
        return true;*/
    }

    public static Level LoadScores(string name) {
        if (reader == null)
            reader = new BinaryReader(File.Open(scoreFile(), FileMode.OpenOrCreate));
        return null;

        /*LevelReader reader = new LevelReader(Application.dataPath + levelFilesPath + name + levelFilesEnding);
        Level level;
        if (!File.Exists(Application.dataPath + levelFilesPath + name + levelFilesEnding)) {
            Debug.Log(Application.dataPath + levelFilesPath + name + levelFilesEnding);
            Debug.Log("A Level of such name, doesn't exist!");
            return null;
        }
        try {
            level = reader.readLevel();
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return null;
        }
        return level;*/
    }
}
