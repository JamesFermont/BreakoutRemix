using System.Collections;
using System.Collections.Generic;

public class LevelTimeTarget {
    public string level;
    public int target;

    public LevelTimeTarget(string levelName, int targetTime) {
        level = levelName;
        target = targetTime;
    }
}
