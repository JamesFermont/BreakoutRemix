using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public int width { get; private set; }
    public int height { get; private set; }


    public Vector2Int getDimensions() {
        return new Vector2Int(width, height);
    }
}
