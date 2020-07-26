using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class LevelTimeTargets {
    private static string path = "/";
    private static string fileName = "notquitetimelytargets.nqi";

    public static List<LevelTimeTarget> targets;

    public static int getTarget(string level) {
        if (targets == null)
            ReadLevels();
        foreach (LevelTimeTarget target in targets) {
            Debug.Log(target.level + ":" + target.target);
            if (target.level == level)
                return target.target;
        }
            
        return 0;
    }
    public static void setTarget(string level, int targetTime) {
        foreach (LevelTimeTarget target in targets) {
            if (target.level == level) {
                target.target = targetTime;
                return;
            }
        }
        targets.Add(new LevelTimeTarget(level, targetTime));
        SaveLevels();
    }

    public static bool SaveLevels() {
        BinaryWriter writer = new BinaryWriter(File.Open(Application.streamingAssetsPath + path + fileName, FileMode.Create));
        foreach (LevelTimeTarget target in targets) {
            writer.Write(target.level);
            writer.Write(target.target);
        }
        writer.Close();
        return false;
    }
    private static void ReadLevels() {
        targets = new List<LevelTimeTarget>();
        BinaryReader reader = new BinaryReader(File.Open(Application.streamingAssetsPath + path + fileName, FileMode.OpenOrCreate));
        while (reader.BaseStream.Position != reader.BaseStream.Length) {
            targets.Add(new LevelTimeTarget(reader.ReadString(), reader.ReadInt32()));
        }
        reader.Close();
    }

    public static List<LevelTimeTarget> allTargets() {
        if (targets == null)
            ReadLevels();
        return targets;
    }
}
