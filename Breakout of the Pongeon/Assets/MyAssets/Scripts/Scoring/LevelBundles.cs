using UnityEngine;
using System.Collections.Generic;

public class LevelBundles : MonoBehaviour
{

    public LevelBundle[] bundles;

    public string[] AllActiveLevels (){
        List<string> returnList = new List<string>();
        LevelBundle previous = null;
        foreach(LevelBundle bundle in bundles) {
            if (previous == null|| previous.TotalScore() >= bundle.score ) {
                foreach (string level in bundle.levels) {
                    returnList.Add(level);
                }
                previous = bundle;
                continue;
            }
            break;
        }
        return returnList.ToArray();
    }
    
}
