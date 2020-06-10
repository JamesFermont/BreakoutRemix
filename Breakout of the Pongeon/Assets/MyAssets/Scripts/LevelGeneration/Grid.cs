using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    public int width;
    public int height;
    float cellWidth;
    float cellHeight;
    public Dictionary<int, string> levelObjects { get; private set; }
    public int[,] levelMap { get; private set; }
    private Vector2 positionOffset;


    public Grid(int width, int height, int[,] map, Dictionary<int, string> objects) {
        this.width = width;
        this.height = height;
        cellWidth = Constants.LEVEL_WIDTH / width;
        cellHeight = Constants.LEVEL_HEIGHT / height;
        positionOffset = new Vector2((width / 2 * -1f + 0.5f) * cellWidth, (height / 2 * -1f + 0.5f) * cellHeight);
        levelMap = map;

        levelObjects = objects;
    }

    public Grid(int width, int height) : this(width, height, new int[width, height], new Dictionary<int, string>()) { }


    public int IDAtPosition(Vector2Int position) {
        if (isOnGrid(position))
            return levelMap[position.x, position.y];
        else
            return 0;
    }
    public int IDAtPosition(int x, int y) {
        return IDAtPosition(new Vector2Int(x, y));
    }

    public bool isOnGrid(Vector2Int position) {
        return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
    }

    public string getLevelObject(int key) {
        if (!levelObjects.ContainsKey(key))
            Debug.Log("Uh-OH " + key);
        return levelObjects[key];
    }
    public bool hasLevelObject(int key) {
        return levelObjects.ContainsKey(key);
    }

    public Vector3 toWorldPosition(Vector2Int gridPosition) {
        return new Vector3(positionOffset.x + gridPosition.x * cellWidth, positionOffset.y + gridPosition.y * cellHeight, 0f);
    }

    public Vector2Int toGridPosition (Vector3 position) {
        return new Vector2Int(Mathf.FloorToInt((position.x - positionOffset.x + cellWidth/2f)/ cellWidth), Mathf.FloorToInt((position.y - positionOffset.y + cellHeight/2f) / cellHeight));
    }
}
