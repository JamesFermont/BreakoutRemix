using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridEditor {

    public static bool TryPlaceObjectInGrid(string objectID, Vector2Int position) {
        return TryPlaceObjectInGrid(objectID, position, false);
    }

    public static bool TryPlaceObjectInGrid(string objectID, Vector2Int position, bool overrideBlocks) {
        if (!BlockDictionary.hasBlock(objectID))
            return false;

        if (!Grid.isOnGrid(position) || position.y < Constants.PROTECTED_ROWS)
            return false;
        if (overrideBlocks) {
            if (!ObjectHasSpaceAt(position, objectID, LevelManager.currentLevel.grid.IDAtPosition(position)))
                return false;
            TryDeleteObjectAtPosition(position);
        } else if (!ObjectHasSpaceAt(position, objectID))
            return false;


        int newID = LevelManager.currentLevel.grid.levelObjects.Count + 1;
        LevelManager.currentLevel.grid.levelObjects.Add(newID, objectID);
        LevelGenerator.instance.AddSingleID(position, objectID);
        return PaintIDInMap(newID, position, BlockDictionary.instance.getBlock(objectID).GetComponent<BlockManager>().GetDimensions());
    }

    private static bool PaintIDInMap(int id, Vector2Int position, Vector2Int dimensions) {
        for (int x = 0; x < dimensions.x; x++) {
            for (int y = 0; y < dimensions.y; y++) {
                LevelManager.currentLevel.grid.levelMap[position.x + x].col[position.y + y] = id;
            }
        }
        return true;
    }


    public static bool TryDeleteObjectAtPosition(Vector2Int position) {
        if (!Grid.isOnGrid(position))
            return false;

        int key = LevelManager.currentLevel.grid.IDAtPosition(position);

        if (key == 0)
            return true;

        Debug.Log("Trying to delete " + LevelManager.currentLevel.grid.getLevelObject(key));
        return RemoveObjectAndUpdateGrid(key);
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
                if (!Grid.isOnGrid(currentPosition))
                    return false;
                if (LevelManager.currentLevel.grid.IDAtPosition(currentPosition) != 0)
                    if(LevelManager.currentLevel.grid.IDAtPosition(currentPosition) != key)
                        return false;
            }
        }
        return true;
    }
    private static bool RemoveObjectAndUpdateGrid(int objectID) {
        int[,] ObjectIDTranslation = new int[LevelManager.currentLevel.grid.levelObjects.Count, 2];
        int[,] translatedGridMap = new int[Constants.GRID_WIDTH, Constants.GRID_HEIGHT];
        GridList translatedDictionary = new GridList();
        //Remove objectID
        for (int i = 1; i <= LevelManager.currentLevel.grid.levelObjects.Count; i++) {
            if (i == objectID) {
                ObjectIDTranslation[i - 1, 0] = i;
                ObjectIDTranslation[i - 1, 1] = 0;
            } else if (i > objectID) {
                ObjectIDTranslation[i - 1, 0] = i;
                ObjectIDTranslation[i - 1, 1] = i - 1;
            } else {
                ObjectIDTranslation[i - 1, 0] = i;
                ObjectIDTranslation[i - 1, 1] = i;
            }
        }
        //Create a new Dictionary
        for (int i = 0; i < ObjectIDTranslation.GetLength(0); i++) {
            if (ObjectIDTranslation[i, 1] == 0)
                continue;
            translatedDictionary.Add(ObjectIDTranslation[i, 1], LevelManager.currentLevel.grid.getLevelObject(ObjectIDTranslation[i, 0]));
        }

        //Create a new Map
        for (int x = 0; x < Constants.GRID_WIDTH; x++) {
            for (int y = 0; y < Constants.GRID_HEIGHT; y++) {
                for (int i = 0; i < ObjectIDTranslation.GetLength(0); i++) {
                    if (ObjectIDTranslation[i, 0] == LevelManager.currentLevel.grid.IDAtPosition(x, y)) {
                        translatedGridMap[x, y] = ObjectIDTranslation[i, 1];
                    } else if (LevelManager.currentLevel.grid.IDAtPosition(x, y) == 0) {
                        translatedGridMap[x, y] = 0;
                    }
                }
            }
        }

        LevelManager.currentLevel.grid = new Grid(translatedGridMap, translatedDictionary);
        return true;

    }
}
