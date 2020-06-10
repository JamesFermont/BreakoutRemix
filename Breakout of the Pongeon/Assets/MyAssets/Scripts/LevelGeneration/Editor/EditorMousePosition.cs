using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GameObject))]
public class EditorMousePosition : Editor {
    private bool isPainting = false;
    private GameObject HoverObject = null;
    public static EditorMousePosition instance = null;
    private Vector2Int mousePosition;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }

    }
    [InitializeOnLoadMethod]
    private void myLoad() {
        EditorApplication.playModeStateChanged += myfunc;
    }

    private void myfunc(PlayModeStateChange state) {
        Debug.Log(state + ": " + LevelManager.currentLevel);
    }

    public void OnSceneGUI() {
        if (EditorApplication.isPlaying)
            return;

        if (instance == null) {
            instance = this;
        }
        if (Event.current != null && LevelManager.currentLevel != null)
            mousePosition = LevelManager.currentLevel.grid.toGridPosition(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);



        if (HoverObject == null && UnityEngine.SceneManagement.SceneManager.GetActiveScene() != null) {
            GameObject[] sceneObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject go in sceneObjects) {
                if (go.CompareTag("EditorHover")) {
                    HoverObject = go;
                    break;
                }
            }
        }

        if (Event.current != null && LevelManager.currentLevel != null) {
            if (HoverObject != null) {
                HoverObject.transform.position = LevelManager.currentLevel.grid.toWorldPosition(mousePosition);
            }

            if (Event.current.type == EventType.MouseDown) {
                isPainting = true;
            }

            if (isPainting) {
                isPainting = false;
                switch (EditorPalette.mode) {
                    case EditorPalette.PaintMode.Paint:

                        GridEditor.TryPlaceObjectInGrid(EditorPalette.selectedBlock, mousePosition);

                        break;
                    case EditorPalette.PaintMode.Erase:
                        //Debug.Log("Trying to erase at " + LevelManager.instance.currentLevel.grid.toGridPosition(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin));
                        if (LevelManager.currentLevel.grid.IDAtPosition(mousePosition) == 0)
                            break;
                        string objectToDelete = LevelManager.currentLevel.grid.getLevelObject(LevelManager.currentLevel.grid.IDAtPosition(mousePosition));
                        if (GridEditor.TryDeleteObjectAtPosition(mousePosition)) {
                            Debug.Log("Hey!");
                            foreach (Transform child in LevelManager.currentLevelGO.transform) {
                                int currentID = LevelManager.currentLevel.grid.IDAtPosition(mousePosition);
                                //Debug.Log(currentID + "/" + LevelManager.currentLevel.grid.levelObjects.Count);
                                if (child.name == currentID + "-" + objectToDelete) {
                                    DestroyImmediate(child.gameObject);
                                    break;
                                }
                            }


                        }

                        break;
                    default:

                        break;

                }
            }
        }

    }

    public void SelectNewBlock(string block) {
        if (HoverObject != null && HoverObject.transform.childCount > 0) {
            DestroyImmediate(HoverObject.transform.GetChild(0).gameObject);
        }
        GameObject newBlock = BlockDictionary.instance.getBlock(block);
        if (newBlock.GetComponent<BlockManager>() != null)
            Instantiate(newBlock, HoverObject.transform).transform.localPosition = new Vector3((newBlock.GetComponent<BlockManager>().width - 1) * Constants.LEVEL_WIDTH / Constants.GRID_WIDTH, (newBlock.GetComponent<BlockManager>().height - 1) * Constants.LEVEL_HEIGHT / Constants.GRID_HEIGHT, 0f);
        else
            Instantiate(newBlock, HoverObject.transform);

    }
}
