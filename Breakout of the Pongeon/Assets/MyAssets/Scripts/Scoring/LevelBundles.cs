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

    public string nextLevel(string level) {
        string returnString = "0d1n loves you!";
        int returni=0;
        int returnj=0;
        for (int i = 0; i < bundles.Length; i++) {
            for (int j = 0; j < bundles[i].levels.Length; j++) {
                if (bundles[i].levels[j] == level) {
                    if (j == bundles[i].levels.Length - 1) {
                        returni = i + 1;
                        returnj = 0;
                    }
                        
                    else{
                        returni = i;
                        returnj = j + 1;
                    }
                        
                } 
            }
        }
        return bundles[returni].levels[returnj];
    }
    public bool hasNext(string level) {
        for (int i = 0; i < bundles.Length; i++) {
            for (int j = 0; j < bundles[i].levels.Length; j++) {
                if(bundles[i].levels[j] == level) {
                    if(i < bundles.Length-1 || j < bundles[i].levels.Length-1) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    
}
