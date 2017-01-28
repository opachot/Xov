using UnityEngine;
using UnityEditor;
using System.Collections;

public class EDITOR_ToolControlPanel : EditorWindow
{
    private EDITOR_ToolControl toolControl;
    private bool gridSnapping;
    private bool mapData;

    #region Static
    [MenuItem("Custom_Tool/Control_Panel")]
    public static void CreateWindow()
    {
        EditorWindow.GetWindow<EDITOR_ToolControlPanel>();
    }
    #endregion

    void OnEnable()
    {
        toolControl = GameObject.Find("TOOLS").GetComponent<EDITOR_ToolControl>();

        gridSnapping = toolControl.IsGridSnapperActive();
        mapData = toolControl.IsMapDataModifierActive();
    }

    void OnGUI()
    {
        bool old_GridSnapping = gridSnapping;
        bool old_MapData = mapData;


        EditorGUILayout.LabelField("Grid_Snapping");
        gridSnapping = GUI.Toggle(new Rect(10, 15, 100, 30), gridSnapping, gridSnapping.ToString());

        GUILayout.Space(20);
        EditorGUILayout.LabelField("Map_Data_Modifier");
        mapData = GUI.Toggle(new Rect(10, 55, 100, 30), mapData, mapData.ToString());


        if (old_GridSnapping != gridSnapping)
            toolControl.Toogle_GridSnapper();

        if (old_MapData != mapData)
            toolControl.Toogle_MapDataModifier();

    }

}
