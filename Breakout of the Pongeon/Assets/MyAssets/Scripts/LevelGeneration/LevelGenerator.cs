using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour {


    
    public static LevelGenerator instance;

    public void Awake() {
        if (instance == null)
            instance = this;
    }
#if UNITY_EDITOR
    public void LateUpdate() {
        if (instance == null)
            instance = this;
    }
#endif
    //Generate a Level
    public GameObject Generate(Level level) {
        GameObject levelObject = new GameObject(level.name);
        Vector2Int currentPointerPosition;
        string currentLevelObject;
        GameObject currentLevelGameObject;

        for (int x = 0; x < level.width; x++) {
            for (int y = 0; y < level.height; y++) {
                currentPointerPosition = new Vector2Int(x, y);

                if (level.grid.IDAtPosition(currentPointerPosition) > 0) {
                    currentLevelObject = level.grid.getLevelObject(level.grid.IDAtPosition(currentPointerPosition));
                    currentLevelGameObject = Instantiate(BlockDictionary.instance.getBlock(currentLevelObject), level.grid.toWorldPosition(currentPointerPosition), Quaternion.identity, levelObject.transform);
                    currentLevelGameObject.name = level.grid.IDAtPosition(currentPointerPosition) + "-" + currentLevelObject;
                    currentLevelGameObject.transform.SetSiblingIndex(level.grid.IDAtPosition(currentPointerPosition)-1);
                }
            }
        }
        return levelObject;
    }

    public void AddSingleID (Vector2Int position, string objectID) {
        Instantiate(BlockDictionary.instance.getBlock(objectID), LevelManager.instance.currentLevelGO.transform).transform.position = LevelManager.instance.currentLevel.grid.toWorldPosition(position);
    }

}
