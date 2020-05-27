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
    string t = "This is a string and stuff!";

    void OnGUI() {
        GUILayout.Label("Hello World");
        string currentBlock = "";
        
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(100), GUILayout.Height(100));

        for (int i = 0; i < BlockDictionary.instance.Length; i++) {
            currentBlock = BlockDictionary.instance.dict[i].key;
            GUI.DrawTexture(new Rect(new Vector2(50, 90*i),new Vector2(128, 80) ), BlockDictionary.instance.getBlock(currentBlock).GetComponent<SpriteRenderer>().sprite.texture, ScaleMode.ScaleToFit);
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginHorizontal();
        otherScrollPos =
            EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.Label(t);
        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
            t += " \nAnd this is more text!";
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Clear"))
            t = "";
    }

}
