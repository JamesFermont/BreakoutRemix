using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public int width;
    public int height;


    public Vector2Int getDimensions() {
        return new Vector2Int(width, height);
    }
}
