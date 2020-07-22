using UnityEngine;
using System.IO;

public class LevelBundles : MonoBehaviour
{

    private static LevelBundles instance;

    private Bundles bundles;

    [System.Serializable]
    private class Bundles {
        public LevelBundle[] bundles;
    }
    private void Start() {
        
    }

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    private void LoadLevels() {
        bundles = (Bundles)JsonUtility.FromJson(File.ReadAllText(Application.streamingAssetsPath + "/"), typeof(Bundles)); 
    }

}
