using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridEditor {

    public static bool CreateEditorGrid() {
        return CreateEditorGrid(new Grid(Constants.GRID_WIDTH, Constants.GRID_HEIGHT, new int[Constants.GRID_WIDTH, Constants.GRID_HEIGHT], new GridList()));
    }
    public static bool CreateEditorGrid(Grid newGrid) {
        LevelManager.currentLevel.grid = newGrid;
        return true;
    }

    public static bool ClearGrid() {
        if (LevelManager.currentLevel.grid == null)
            return false;
        return CreateEditorGrid();
    }
    public static int getObjectCount() {
        if (LevelManager.currentLevel.grid.levelObjects != null)
            return LevelManager.currentLevel.grid.levelObjects.Count;
        return -1;
    }

    public static bool TryUpdateObjectPosition(int objectKey, Vector2Int newPosition) {
        if (!ObjectHasSpaceAt(newPosition, LevelManager.currentLevel.grid.getLevelObject(objectKey)))
            return false;

        for (int x = 0; x < LevelManager.currentLevel.grid.width; x++) {
            for (int y = 0; y < LevelManager.currentLevel.grid.height; y++) {
                if (LevelManager.currentLevel.grid.IDAtPosition(new Vector2Int(x, y)) == objectKey) {
                    LevelManager.currentLevel.grid.levelMap[x].col[y] = 0;
                }
            }
        }
        Vector2Int blockDimensions = BlockDictionary.instance.getBlock(LevelManager.currentLevel.grid.getLevelObject(objectKey)).GetComponent<BlockManager>().getDimensions();
        return PaintIDInMap(objectKey, newPosition, blockDimensions);
    }
    public static bool TryPlaceObjectInGrid(string objectID, Vector2Int position) {
        if (!ObjectHasSpaceAt(position, objectID))
            return false;
        int newID = LevelManager.currentLevel.grid.levelObjects.Count + 1;
        LevelManager.currentLevel.grid.levelObjects.Add( newID, objectID);
        LevelGenerator.instance.AddSingleID(position, objectID);
        PaintIDInMap(newID, position, BlockDictionary.instance.getBlock(objectID).GetComponent<BlockManager>().getDimensions());
        return true;
    }
    public static bool TryDeleteObjectAtPosition(Vector2Int position) {
        if (!LevelManager.currentLevel.grid.isOnGrid(position)) {
            return false;
        }
        Debug.Log("Object is on Grid!");
        return TryDeleteObject(LevelManager.currentLevel.grid.IDAtPosition(position));
    }


    private static bool TryDeleteObject(int key) {
        if (!LevelManager.currentLevel.grid.levelObjects.ContainsKey(key)) {
            return false;
        }
        Debug.Log("Object is in Dictionary!");
        return RemoveObjectAndUpdateGrid(key);
    }
    private static bool PaintIDInMap(int id, Vector2Int position, Vector2Int dimensions) {
        for (int x = 0; x < dimensions.x; x++) {
            for (int y = 0; y < dimensions.y; y++) {
                LevelManager.currentLevel.grid.levelMap[position.x + x].col[position.y + y] = id;
            }
        }
        return true;
    }
    private static bool ObjectHasSpaceAt(Vector2Int position, string objectID) {
        return ObjectHasSpaceAt(position, objectID, 0);
    }
    private static bool ObjectHasSpaceAt(Vector2Int position, string objectID, int key) {
        BlockManager obj = BlockDictionary.instance.getBlock(objectID).GetComponent<BlockManager>();
        Vector2Int currentPosition;
        if (obj == null) {
            Debug.LogError("Trying to add a Block without a Block Manager!");

        }
        for (int x = 0; x < obj.width; x++) {
            for (int y = 0; y < obj.height; y++) {
                currentPosition = position + new Vector2Int(x, y);
                if (!LevelManager.currentLevel.grid.isOnGrid(currentPosition) || (LevelManager.currentLevel.grid.IDAtPosition(currentPosition) != 0 ^ LevelManager.currentLevel.grid.IDAtPosition(currentPosition) != key)) {
                    return false;
                }
            }
        }
        return true;
    }
    private static bool RemoveObjectAndUpdateGrid(int objectID) {
        int[,] ObjectIDTranslation = new int[LevelManager.currentLevel.grid.levelObjects.Count, 2];
        int[,] translatedGridMap = new int[LevelManager.currentLevel.grid.width, LevelManager.currentLevel.grid.height];
        GridList translatedDictionary = new GridList();

        for (int i = 1; i <= LevelManager.currentLevel.grid.levelObjects.Count; i++) {
            if (i == objectID) {
                ObjectIDTranslation[i - 1, 0] = 0;
                ObjectIDTranslation[i - 1, 1] = 0;
            } else if (i > objectID) {
                ObjectIDTranslation[i - 1, 0] = i - 1;
                ObjectIDTranslation[i - 1, 1] = i - 1;
            } else {
                ObjectIDTranslation[i - 1, 0] = i;
                ObjectIDTranslation[i - 1, 1] = i;
            }
        }

        Debug.Log(objectID + "/" + LevelManager.currentLevel.grid.levelObjects.Count);

        for (int i = 0; i < LevelManager.currentLevel.grid.levelObjects.Count; i++) {
            for (int u = 0; u < ObjectIDTranslation.GetLength(0); u++) {

                if (ObjectIDTranslation[u, 0] == i && ObjectIDTranslation[u, 1] != 0) {
                    translatedDictionary.Add(ObjectIDTranslation[u, 1], LevelManager.currentLevel.grid.getLevelObject(i));
                }
            }
        }

        for (int x = 0; x < LevelManager.currentLevel.grid.width; x++) {
            for (int y = 0; y < LevelManager.currentLevel.grid.height; y++) {
                for (int i = 0; i < ObjectIDTranslation.GetLength(0); i++) {

                    if (ObjectIDTranslation[i, 0] == LevelManager.currentLevel.grid.IDAtPosition(x, y)) {
                        translatedGridMap[x, y] = ObjectIDTranslation[i, 1];
                    } else if (LevelManager.currentLevel.grid.IDAtPosition(x, y) == 0) {
                        translatedGridMap[x, y] = 0;
                    }
                }
            }
        }

        LevelManager.currentLevel.grid = new Grid(LevelManager.currentLevel.grid.width, LevelManager.currentLevel.grid.height, translatedGridMap, translatedDictionary);
        return true;

    }
}
