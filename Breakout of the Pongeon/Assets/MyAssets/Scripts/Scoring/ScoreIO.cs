using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ScoreIO {
    public static string scoreFilePath = "/StreamingAssets/";
    public static string scoreFileName = "notquitehighscores";
    public static string scoreFileEnding = ".nqi";

    private static BinaryReader reader;
    private static BinaryWriter writer;


    private static string scoreFile() {
        return Application.dataPath + scoreFilePath + scoreFileName + scoreFileEnding;
    }

    public static bool SaveScores(List<Score> scores) {
        writer = new BinaryWriter(File.Open(scoreFile(), FileMode.Create));
        try {
            foreach (Score score in scores) {
                writer.Write(score.player);
                writer.Write(score.level);
                writer.Write(score.baseScore);
                writer.Write(score.timeTaken);
            }


            writer.Close();
            return true;
        } catch (Exception ex) {
            Debug.Log(ex.ToString());
            writer.Close();
            return false;
        }
    }

    public static List<Score> LoadScores() {
        reader = new BinaryReader(File.Open(scoreFile(), FileMode.OpenOrCreate));
        List<Score> returnList = new List<Score>();
        try {
            while (reader.BaseStream.Position != reader.BaseStream.Length) {
                returnList.Add(new Score(reader.ReadString(), reader.ReadString(), reader.ReadInt32(), reader.ReadInt32()));
            }
            reader.Close();
            return returnList;
        } catch (Exception ex) {
            Debug.Log(ex.ToString());
            reader.Close();
            return null;
        }
    }
}
/* 
 Wir benötigen für einen Score folgendes:
    Name    (string)
    Score (int)
    Name_Level (string)
     */
