using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEditor {

    private static GridEditor Instance;
    public static GridEditor instance {
        get {
            if (Instance == null)
                Instance = new GridEditor();

            return Instance;
        }
    }

    public bool CreateEditorGrid() {
        return CreateEditorGrid(new Grid(Constants.GRID_WIDTH, Constants.GRID_HEIGHT, new int[Constants.GRID_WIDTH, Constants.GRID_HEIGHT], new Dictionary<int, string>()));
    }
    public bool CreateEditorGrid(Grid newGrid) {
        LevelManager.instance.currentLevel.grid = newGrid;
        return true;
    }

    public bool ClearGrid() {
        if (LevelManager.instance.currentLevel.grid == null)
            return false;
        return CreateEditorGrid();
    }
    public int getObjectCount() {
        if (LevelManager.instance.currentLevel.grid != null)
            return LevelManager.instance.currentLevel.grid.levelObjects.Count;
        return -1;
    }

    public bool TryUpdateObjectPosition(int objectKey, Vector2Int newPosition) {
        if (!ObjectHasSpaceAt(newPosition, LevelManager.instance.currentLevel.grid.levelObjects[objectKey]))
            return false;

        for (int x = 0; x < LevelManager.instance.currentLevel.grid.width; x++) {
            for (int y = 0; y < LevelManager.instance.currentLevel.grid.height; y++) {
                if (LevelManager.instance.currentLevel.grid.IDAtPosition(new Vector2Int(x, y)) == objectKey) {
                    LevelManager.instance.currentLevel.grid.levelMap[x, y] = 0;
                }
            }
        }
        Vector2Int blockDimensions = BlockDictionary.instance.getBlock(LevelManager.instance.currentLevel.grid.levelObjects[objectKey]).GetComponent<BlockManager>().getDimensions();
        return PaintIDInMap(objectKey, newPosition, blockDimensions);
    }
    public bool TryPlaceObjectInGrid(string objectID, Vector2Int position) {
        if (!ObjectHasSpaceAt(position, objectID))
            return false;
        int newID = LevelManager.instance.currentLevel.grid.levelObjects.Count;
        LevelManager.instance.currentLevel.grid.levelObjects.Add(newID, objectID);
        PaintIDInMap(newID, position, BlockDictionary.instance.getBlock(objectID).GetComponent<BlockManager>().getDimensions());
        return true;
    }
    public bool TryDeleteObjectAtPosition(Vector2Int position) {
        if (!LevelManager.instance.currentLevel.grid.isOnGrid(position)) {
            return false;
        }
        return TryDeleteObject(LevelManager.instance.currentLevel.grid.IDAtPosition(position));
    }


    private bool TryDeleteObject(int key) {
        if (!LevelManager.instance.currentLevel.grid.levelObjects.ContainsKey(key)) {
            return false;
        }
        return RemoveObjectAndUpdateGrid(key);
    }
    private bool PaintIDInMap(int id, Vector2Int position, Vector2Int dimensions) {
        for (int x = 0; x < dimensions.x; x++) {
            for (int y = 0; y < dimensions.y; y++) {
                LevelManager.instance.currentLevel.grid.levelMap[position.x + x, position.y + y] = id;
            }
        }
        return true;
    }
    private bool ObjectHasSpaceAt(Vector2Int position, string objectID) {
        return ObjectHasSpaceAt(position, objectID, 0);
    }
    private bool ObjectHasSpaceAt(Vector2Int position, string objectID, int key) {
        BlockManager obj = BlockDictionary.instance.getBlock(objectID).GetComponent<BlockManager>();
        Vector2Int currentPosition;
        if(obj != null) {
            obj = BlockDictionary.instance.getBlock(objectID).AddComponent<BlockManager>();
            obj.width = 1;
            obj.height = 1;
        }
        for (int x = 0; x < obj.width; x++) {
            for (int y = 0; y < obj.height; y++) {
                currentPosition = position + new Vector2Int(x, y);
                if (LevelManager.instance.currentLevel.grid.isOnGrid(currentPosition) || (LevelManager.instance.currentLevel.grid.IDAtPosition(currentPosition) != 0 ^ LevelManager.instance.currentLevel.grid.IDAtPosition(currentPosition) != key)) {
                    return false;
                }
            }
        }
        return true;
    }
    private bool RemoveObjectAndUpdateGrid(int objectID) {
        int[,] ObjectIDTranslation = new int[LevelManager.instance.currentLevel.grid.levelObjects.Count, 2];
        int[,] translatedGridMap = new int[LevelManager.instance.currentLevel.grid.width, LevelManager.instance.currentLevel.grid.height];
        Dictionary<int, string> translatedDictionary = new Dictionary<int, string>();

        for (int i = 0; i < LevelManager.instance.currentLevel.grid.levelObjects.Count; i++) {
            if (i == objectID) {
                ObjectIDTranslation[i, 0] = 0;
                ObjectIDTranslation[i, 1] = 0;
            } else if (i > objectID) {
                ObjectIDTranslation[i, 0] = i - 1;
                ObjectIDTranslation[i, 1] = i - 1;
            } else {
                ObjectIDTranslation[i, 0] = i;
                ObjectIDTranslation[i, 1] = i;
            }
        }
        for (int i = 0; i < LevelManager.instance.currentLevel.grid.levelObjects.Keys.Count; i++) {
            for (int u = 0; u < ObjectIDTranslation.Length; u++) {
                if (ObjectIDTranslation[u, 0] == i && ObjectIDTranslation[u, 1] != 0) {
                    translatedDictionary.Add(ObjectIDTranslation[u, 1], LevelManager.instance.currentLevel.grid.levelObjects[i]);
                }
            }
        }
        for (int x = 0; x < LevelManager.instance.currentLevel.grid.width; x++) {
            for (int y = 0; x < LevelManager.instance.currentLevel.grid.height; y++) {
                for (int i = 0; i < ObjectIDTranslation.Length; i++) {
                    if (ObjectIDTranslation[i, 0] == LevelManager.instance.currentLevel.grid.IDAtPosition(new Vector2Int(x, y))) {
                        translatedGridMap[x, y] = ObjectIDTranslation[i, 1];
                    } else if (LevelManager.instance.currentLevel.grid.IDAtPosition(new Vector2Int(x, y)) == 0) {
                        translatedGridMap[x, y] = 0;
                    }
                }
            }
        }

        LevelManager.instance.currentLevel.grid = new Grid(LevelManager.instance.currentLevel.grid.width, LevelManager.instance.currentLevel.grid.height, translatedGridMap, translatedDictionary);
        return true;

    }
}
