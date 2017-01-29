using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class EDITOR_MapDataModifier : MonoBehaviour {

    #region DECLARATION
    // CONST
    Color WALKABLE_COLOR = new Color(0, 1, 0, 0.3f);
    Color HOLE_COLOR     = new Color(0, 0, 1, 0.3f);
    Color OBSTACLE_COLOR = new Color(1, 0, 0, 0.3f);

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
        GameObject[] tiles = Selection.gameObjects;

        if (tiles != null)
        {
            if (Event.current.type == EventType.scrollWheel)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].CompareTag("UNKNOWN"))
                    {
                        tiles[i].tag = "WALKABLE";
                        tiles[i].GetComponent<SpriteRenderer>().color = WALKABLE_COLOR;
                    }
                    else if (tiles[i].CompareTag("WALKABLE"))
                    {
                        tiles[i].tag = "HOLE";
                        tiles[i].GetComponent<SpriteRenderer>().color = HOLE_COLOR;
                    }
                    else if (tiles[i].CompareTag("HOLE"))
                    {
                        tiles[i].tag = "OBSTACLE";
                        tiles[i].GetComponent<SpriteRenderer>().color = OBSTACLE_COLOR;
                    }
                    else if (tiles[i].CompareTag("OBSTACLE"))
                    {
                        tiles[i].tag = "WALKABLE";
                        tiles[i].GetComponent<SpriteRenderer>().color = WALKABLE_COLOR;
                    }
                }
            }
        }
    }
    #endif
    #endregion

}