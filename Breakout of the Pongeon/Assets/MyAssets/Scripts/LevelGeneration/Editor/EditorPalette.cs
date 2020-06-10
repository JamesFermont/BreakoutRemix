using UnityEngine;
using UnityEditor;

public class EditorPalette : EditorWindow {

    public enum PaintMode { Paint, Erase, None }
    public static string selectedBlock = "";


    private Vector2 scrollPos;
    private Vector2 otherScrollPos;
    private Vector2 selectSize = new Vector2(128, 72);
    private int columns = 2;
    public static PaintMode mode = PaintMode.None;


    [MenuItem("Window/BOP/Palette")]
    static void Init() {
        EditorPalette window = GetWindow<EditorPalette>(false, "Palette", true);
        window.Show();
    }

    void OnGUI() {

        if (EditorApplication.isPlaying) {
            GUILayout.Label("YOU ARE IN PLAY MODE");
            return;
        }
        PaintPaletteButtons();
    }



    private Rect nextPrefabPosition(int index) {
        return new Rect(new Vector2(index % columns * selectSize.x, Mathf.FloorToInt(index / (float)columns) * selectSize.y) + Vector2.one, selectSize);
    }

    public void SelectBlock(string blockID) {
        SelectBlock(PaintMode.Paint, blockID);
    }
    public void SelectBlock(PaintMode newMode) {
        SelectBlock(newMode, "");
    }
    public void SelectBlock(PaintMode newMode, string blockID) {
        mode = newMode;
        selectedBlock = blockID;

        if (newMode == PaintMode.Paint)
            EditorMousePosition.instance.SelectNewBlock(blockID);
    }


    public void PaintPaletteButtons() {
        string currentBlock = "";
        if (BlockDictionary.instance != null) {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false, GUILayout.Width(300));
            for (int i = -1; i <= BlockDictionary.instance.Length; i++) {
                if (i == -1) {
                    if (mode == PaintMode.Erase)
                        GUI.enabled = false;
                    if (GUI.Button(nextPrefabPosition(i + 1), new GUIContent("ERASE")))
                        SelectBlock(PaintMode.Erase);

                } else if (i == BlockDictionary.instance.Length) {
                    if (GUI.Button(nextPrefabPosition(i + 1), new GUIContent("+")))
                        SelectBlock(PaintMode.None);
                } else {
                    currentBlock = BlockDictionary.instance.dict[i].key;
                    if (currentBlock == selectedBlock)
                        GUI.enabled = false;
                    if (GUI.Button(nextPrefabPosition(i + 1), new GUIContent(BlockDictionary.instance.getBlock(currentBlock).GetComponent<SpriteRenderer>().sprite.texture)))
                        SelectBlock(currentBlock);
                }
                if (!GUI.enabled)
                    GUI.enabled = true;

            }
            EditorGUILayout.EndScrollView();
        } else
            GUILayout.Label("Block Dictionary is empty!");

    }
}
