using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEditor
{
    Grid editorGrid;


    public GridEditor instance {
        get {
            if (instance == null)
                instance = this;

            return instance;
        }
        private set { }
    }

    public bool CreateEditorGrid() {
        editorGrid = new Grid(20, 18, new int[20, 18], new Dictionary<int, string>());
        return true;
    }
    public bool ClearGrid () {
        if (editorGrid == null)
            return false;
        return CreateEditorGrid ();
    }


    public bool TryUpdateObjectPosition(int objectKey, Vector2Int newPosition) {
        if (!ObjectHasSpaceAt(newPosition, editorGrid.levelObjects[objectKey]))
            return false;

        for (int x = 0; x < editorGrid.width; x++) {
            for(int y = 0; y < editorGrid.height; y++) {
                if(editorGrid.IDAtPosition(new Vector2Int(x,y)) == objectKey) {
                    editorGrid.levelMap[x, y] = 0;
                }
            }
        }
        Vector2Int blockDimensions = BlockDictionary.instance.getBlock(editorGrid.levelObjects[objectKey]).GetComponent<LevelObject>().getDimensions();
        return PaintIDInMap(objectKey, newPosition, blockDimensions);
    }
    public bool TryPlaceObjectInGrid(string objectID, Vector2Int position) {
        if(!ObjectHasSpaceAt(position, objectID))
            return false;
        int newID = editorGrid.levelObjects.Count;
        editorGrid.levelObjects.Add(newID, objectID);
        PaintIDInMap(newID, position, BlockDictionary.instance.getBlock(objectID).GetComponent<LevelObject>().getDimensions());
        return true;
    }
    public bool TryDeleteObjectAtPosition(Vector2Int position) {
        if(!editorGrid.isOnGrid(position)) {
            return false;
        }
        return TryDeleteObject(editorGrid.IDAtPosition(position));
    }


    private bool TryDeleteObject (int key) {
        if (!editorGrid.levelObjects.ContainsKey(key)) {
            return false;
        }
        return RemoveObjectAndUpdateGrid(key);
    }
    private bool PaintIDInMap (int id, Vector2Int position, Vector2Int dimensions) {
        for (int x = 0; x < dimensions.x; x++) {
            for (int y = 0; y < dimensions.y; y++) {
                editorGrid.levelMap[position.x + x, position.y + y] = id;
            }
        }
        return true;
    }
    private bool ObjectHasSpaceAt (Vector2Int position, string objectID) {
        return ObjectHasSpaceAt(position, objectID, 0);
    }
    private bool ObjectHasSpaceAt(Vector2Int position, string objectID, int key) {
        LevelObject obj = BlockDictionary.instance.getBlock(objectID).GetComponent<LevelObject>();
        Vector2Int currentPosition;
        for (int x = 0; x < obj.width; x++) {
            for (int y = 0; y < obj.height; y++) {
                currentPosition = position + new Vector2Int(x, y);
                if (editorGrid.isOnGrid(currentPosition) || (editorGrid.IDAtPosition(currentPosition) != 0 ^ editorGrid.IDAtPosition(currentPosition) != key)) {
                    return false;
                }
            }
        }
        return true;
    }
    private bool RemoveObjectAndUpdateGrid(int objectID) {
        int[,] ObjectIDTranslation = new int[editorGrid.levelObjects.Count, 2];
        int[,] translatedGridMap = new int[editorGrid.width, editorGrid.height];
        Dictionary<int, string> translatedDictionary = new Dictionary<int, string>();

        for (int i = 0; i < editorGrid.levelObjects.Count; i++) {
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
        for (int i = 0; i < editorGrid.levelObjects.Keys.Count; i++) {
            for(int u = 0; u < ObjectIDTranslation.Length; u++) {
                if(ObjectIDTranslation[u,0]== i && ObjectIDTranslation[u,1]!= 0) {
                    translatedDictionary.Add(ObjectIDTranslation[u, 1], editorGrid.levelObjects[i]);
                }
            }
        }
        for (int x = 0; x <  editorGrid.width; x++) {
            for (int y = 0; x < editorGrid.height; y++) {
                for (int i = 0; i < ObjectIDTranslation.Length; i++) {
                    if (ObjectIDTranslation[i,0] == editorGrid.IDAtPosition(new Vector2Int (x, y))) {
                        translatedGridMap[x, y] = ObjectIDTranslation[i, 1];
                    } else if (editorGrid.IDAtPosition(new Vector2Int(x, y)) == 0) {
                        translatedGridMap[x, y] = 0;
                    }
                }
            }
        }

        editorGrid = new Grid(editorGrid.width, editorGrid.height, translatedGridMap, translatedDictionary);
        return true;

        } 
}
