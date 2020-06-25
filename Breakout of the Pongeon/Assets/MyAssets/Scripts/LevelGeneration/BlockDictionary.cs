using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlockDictionary : MonoBehaviour {

    public static BlockDictionary instance;
    public List<dictionaryEntry> dict = new List<dictionaryEntry>();
    public int Length {
        get => dict.Count;
    }

    [System.Serializable]
    public class dictionaryEntry {
        public string key;
        public GameObject myGameObject;

        public dictionaryEntry(string key, GameObject go) {
            this.key = key;
            myGameObject = go;
        }

    }

    public void Awake() {
        if (instance == null) {
            instance = this;
        }

    }

    public static bool hasBlock(string blockName) {
        foreach (dictionaryEntry entry in instance.dict)
            if (entry.key == blockName)
                return true;
        return false;
    }


    public GameObject getBlock(string key) {
        foreach (dictionaryEntry entry in dict) {
            if (string.Compare(entry.key, key) == 0) {
                return entry.myGameObject;
            }
        }
        return null;
    }

}
