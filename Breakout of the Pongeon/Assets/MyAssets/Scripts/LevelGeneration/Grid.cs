using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Grid {
    public static int width = Constants.GRID_WIDTH;
    public static int height = Constants.GRID_HEIGHT;
    static float cellWidth = Constants.LEVEL_WIDTH/ width;
    static float cellHeight = Constants.LEVEL_HEIGHT/ height;
    [SerializeField] public GridColumn[] levelMap;
    [SerializeField] public GridList levelObjects;
    
    private static Vector2 positionOffset;

    [Serializable]
    public class GridColumn {
        [SerializeField]public int[] col;

        public GridColumn (int[] c) {
            col = c;
        }
}


    public Grid(int[,] map, GridList objects) {
        positionOffset = new Vector2((width / 2 * -1f + 0.5f) * cellWidth, (height / 2 * -1f + 0.5f) * cellHeight);
        levelMap = new GridColumn[map.GetLength(0)];
        
        for(int x = 0; x < width; x++) {
            int[] returnY = new int[height];
            for(int y = 0; y < height; y++) {
                returnY[y] = map[x, y]; 
            }
            levelMap[x] = new GridColumn(returnY);
        }

        levelObjects = objects;
    }

    public Grid() : this(new int[width, height], new GridList()) { }

    public int IDAtPosition(Vector2Int position) {
        if (isOnGrid(position))
            return levelMap[position.x].col[position.y];
        else
            return 0;
    }
    public int IDAtPosition(int x, int y) {
        return IDAtPosition(new Vector2Int(x, y));
    }

    public static bool isOnGrid(Vector2Int position) {
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

    public static Vector2Int toGridPosition(Vector3 position) {
        return new Vector2Int(Mathf.FloorToInt((position.x - positionOffset.x + cellWidth / 2f) / cellWidth), Mathf.FloorToInt((position.y - positionOffset.y + cellHeight / 2f) / cellHeight));
    }

    //DEBUG Function
    public string PrintMap () {
        string returnString = "";
        for(int y = height - 1; y >= 0; y--) {
            for (int x = 0; x < width; x++) {
                returnString += levelMap[x].col[y];
            }
            returnString += "\n";
        }

        return returnString;
    }

}
