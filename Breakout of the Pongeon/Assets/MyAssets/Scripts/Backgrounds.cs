using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Background {
    public string name;
    public VideoClip track;

    public bool loop;
    public bool waitForFirstFrame;
    
    [Range(0.1f, 1f)]
    public float alpha;
    
    [HideInInspector]
    public VideoPlayer source;
}
