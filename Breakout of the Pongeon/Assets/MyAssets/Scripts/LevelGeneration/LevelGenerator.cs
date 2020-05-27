using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator instance;
    public Level currentLevel;
    public GameObject currentLevelGameObject;

    //Generate a Level
    public void Generate(Level level) {
        GameObject levelObject = new GameObject(level.name);
        levelObject.transform.SetParent(transform);
        Vector2Int currentPointerPosition;
        string currentLevelObject;
        for (int x = 0; x < level.width; x++) {
            for (int y = 0; y < level.height; y++) {
                currentPointerPosition = new Vector2Int(x, y);
                
                if(level.grid.IDAtPosition(currentPointerPosition) > 0) {
                    currentLevelObject = level.grid.getLevelObject(level.grid.IDAtPosition(currentPointerPosition));
                    Instantiate( BlockDictionary.instance.getBlock(currentLevelObject), level.grid.toWorldPosition(currentPointerPosition), Quaternion.identity, levelObject.transform);
                }
            }
        }
        currentLevelGameObject = levelObject;
    }

    public void Awake() {
        if (instance == null)
            instance = this;
    }

    public void Start() {
        int[,] map = new int[20, 18];
        Dictionary<int, string> dict = new Dictionary<int, string>();
        int currentKey = 1;
        for (int x = 0; x < 20; x++) {
            for (int y = 0; y < 18; y++) {
                if (x == 0 || x == 19 || y == 0 || y == 17) {
                    dict.Add(currentKey, "DEBUG_BLOCK");
                    map[x, y] = currentKey;
                    currentKey++;
                }
                    
            }
        }

        Level debugLevel = new Level("Hello Ball", new Grid(20, 18, map, dict));
        //Generate(debugLevel);
        //currentLevel = debugLevel;
    }


    public void Update () {
        if(Input.GetKeyDown(KeyCode.J)) {
            bool saveSuccesful = false;
            if (currentLevel != null)
                saveSuccesful = LevelIO.instance.SaveLevel(currentLevel);

            Debug.Log(saveSuccesful);

        } else if(Input.GetKeyDown(KeyCode.L)) {
            currentLevel = LevelIO.instance.LoadLevel("Hello Ball");

            Generate(currentLevel);
        }
    }


}
