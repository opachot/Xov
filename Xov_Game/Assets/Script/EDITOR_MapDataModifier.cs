using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class EDITOR_MapDataModifier : MonoBehaviour {

    #region DECLARATION
    // CONST
    Color WALKABLE_COLOR = new Color(0, 255, 0, 100);
    Color HOLE_COLOR     = new Color(0, 0, 255, 100);
    Color OBSTACLE_COLOR = new Color(255, 0, 0, 100);

    // PRIVATE

    // PUBLIC

    #endregion

    #region UNITY METHODE
    #if UNITY_EDITOR
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += OnSceneGui;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGui;
    }

    void OnSceneGui(SceneView view)
    {
        GameObject tile = Selection.activeGameObject;

        if (tile != null)
        {
            if (Event.current.type == EventType.keyDown)
            {
                if (tile.CompareTag("UNKNOWN"))
                {
                    tile.tag = "WALKABLE";
                    tile.GetComponent<SpriteRenderer>().color = WALKABLE_COLOR;
                }
                else if (tile.CompareTag("WALKABLE"))
                {
                    tile.tag = "HOLE";
                    tile.GetComponent<SpriteRenderer>().color = HOLE_COLOR;
                }
                else if (tile.CompareTag("HOLE"))
                {
                    tile.tag = "OBSTACLE";
                    tile.GetComponent<SpriteRenderer>().color = OBSTACLE_COLOR;
                }
                else if (tile.CompareTag("OBSTACLE"))
                {
                    tile.tag = "WALKABLE";
                    tile.GetComponent<SpriteRenderer>().color = WALKABLE_COLOR;
                }
            }
        }
    }
    #endif
    #endregion

}