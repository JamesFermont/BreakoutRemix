using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GameObject))]
public class EditorMousePosition : Editor
{
    private bool isPainting = false;
    private GameObject HoverObject = null;
    public static EditorMousePosition instance = null;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void OnSceneGUI() {
        if (instance == null) {
            instance = this;
        }

        if(HoverObject == null && UnityEngine.SceneManagement.SceneManager.GetActiveScene() != null) {
            GameObject[] sceneObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach(GameObject go in sceneObjects) {
                if (go.CompareTag("EditorHover")) {
                    HoverObject = go;
                    break;
                }
            }
        }

        if (Event.current != null && LevelManager.instance.currentLevel != null) {
            if(HoverObject != null) {
                HoverObject.transform.position = LevelManager.instance.currentLevel.grid.toWorldPosition(LevelManager.instance.currentLevel.grid.toGridPosition(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin));
            }

            if(Event.current.type == EventType.MouseDown) {
                isPainting = true;
            } else if (Event.current.type == EventType.MouseUp) {
                isPainting = false;
            }

            if (isPainting) {
                Debug.Log("Trying to place " + EditorPalette.selectedBlock + " at " + LevelManager.instance.currentLevel.grid.toGridPosition(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin));
                Debug.Log(GridEditor.instance.TryPlaceObjectInGrid(EditorPalette.selectedBlock , LevelManager.instance.currentLevel.grid.toGridPosition(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin)));
            }
        }
            
    }

    public void SelectNewBlock(string block) {
        if (HoverObject != null && HoverObject.transform.childCount > 0) {
            DestroyImmediate(HoverObject.transform.GetChild(0));
        }
        GameObject newBlock = BlockDictionary.instance.getBlock(block);
        Instantiate(newBlock, new Vector3 ((newBlock.GetComponent<BlockManager>().width - 1) * Constants.LEVEL_WIDTH/Constants.GRID_WIDTH,(newBlock.GetComponent<BlockManager>().height -1)*Constants.LEVEL_HEIGHT / Constants.GRID_HEIGHT,0f), Quaternion.identity, HoverObject.transform);


        Debug.Log("Hey");
    }
}
