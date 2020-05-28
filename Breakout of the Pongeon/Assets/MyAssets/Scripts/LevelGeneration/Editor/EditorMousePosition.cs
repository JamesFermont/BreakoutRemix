using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject))]
public class EditorMousePosition : Editor
{
    public void OnSceneGUI() {
        if (Event.current != null && LevelManager.instance.currentLevel != null)
            Debug.Log(LevelManager.instance.currentLevel.grid.toGridPosition(SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(Event.current.mousePosition)));
    }
}
