using UnityEngine;
using UnityEditor;

public class EditorPalette : EditorWindow {
    [MenuItem("Window/BOP/Palette")]
    static void Init() {
        EditorPalette window = GetWindow<EditorPalette>(false, "Palette", true);
        window.Show();
    }

    Vector2 scrollPos;
    Vector2 otherScrollPos;
    public static string selectedBlock = "";
    private int columns = 2;

    void OnGUI() {

        if (EditorApplication.isPlaying) {
            GUILayout.Label("YOU ARE IN PLAY MODE");
            return;
        }



        string currentBlock = "";
        if (BlockDictionary.instance != null) {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false, GUILayout.Width(300));
            for (int i = 0; i < BlockDictionary.instance.Length; i++) {
                currentBlock = BlockDictionary.instance.dict[i].key;
                if (currentBlock == selectedBlock)
                    GUI.enabled = false;
                if (GUI.Button(nextPrefabPosition(i, Vector2.one, new Vector2(128, 72)), new GUIContent(BlockDictionary.instance.getBlock(currentBlock).GetComponent<SpriteRenderer>().sprite.texture)))
                    SelectBlock(currentBlock);
                if (!GUI.enabled)
                    GUI.enabled = true;
            }     
        EditorGUILayout.EndScrollView();
        }
        else 
            GUILayout.Label("Block Dictionary is empty!");
    }

    private Rect nextPrefabPosition (int index, Vector2 offset, Vector2 dimensions) {
        return new Rect(new Vector2(index % columns * dimensions.x,Mathf.FloorToInt(index/(float)columns)*dimensions.y) + offset, dimensions);
    }

    public void SelectBlock(string blockID) {
            selectedBlock = blockID;
            EditorMousePosition.instance.SelectNewBlock(blockID);
    }   

}
