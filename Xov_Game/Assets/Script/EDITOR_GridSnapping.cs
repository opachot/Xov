using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class EDITOR_GridSnapping : MonoBehaviour {

    #region DECLARATION
    // CONST
    const float X_SNAP      = 0.5f;
    const float HALF_X_SNAP = X_SNAP / 2;

    const float Y_SNAP      = 0.25f;
    const float HALF_Y_SNAP = Y_SNAP / 2;

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
            if (Event.current.type == EventType.mouseUp)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    Snap(tiles[i]);
                    Set_SpriteLayer(tiles[i]);
                }  
            }
        }
    }
    #endif
    #endregion

    private void Snap(GameObject tile)
    {
        SnapX(tile);
        SnapY(tile);
    }

    private void SnapX(GameObject tile)
    {
        float rawPosX      = tile.transform.position.x;
        int   castedPosX   = (int)rawPosX;
        float overflowPosX = Mathf.Abs(rawPosX - castedPosX);

        if (overflowPosX < HALF_X_SNAP){
            tile.transform.position = new Vector3(castedPosX, tile.transform.position.y, tile.transform.position.z);
        }
        else if (overflowPosX > X_SNAP + HALF_X_SNAP){
            tile.transform.position = new Vector3(castedPosX + X_SNAP * 2, tile.transform.position.y, tile.transform.position.z);
        }
        else{
            tile.transform.position = new Vector3(castedPosX + X_SNAP, tile.transform.position.y, tile.transform.position.z);
        }
    }

    private void SnapY(GameObject tile)
    {
        float rawPosY      = tile.transform.position.y;
        int   castedPosY   = (int)rawPosY;
        float overflowPosY = Mathf.Abs(rawPosY - castedPosY);

        if (overflowPosY < HALF_Y_SNAP){
            tile.transform.position = new Vector3(tile.transform.position.x, castedPosY, tile.transform.position.z);
        }
        else if (overflowPosY >= HALF_Y_SNAP && overflowPosY < Y_SNAP + HALF_Y_SNAP){
            tile.transform.position = new Vector3(tile.transform.position.x, castedPosY + Y_SNAP, tile.transform.position.z);
        }
        else if (overflowPosY >= Y_SNAP + HALF_Y_SNAP && overflowPosY < Y_SNAP * 2 + HALF_Y_SNAP){
            tile.transform.position = new Vector3(tile.transform.position.x, castedPosY + Y_SNAP * 2, tile.transform.position.z);
        }
        else if (overflowPosY >= Y_SNAP * 2 + HALF_Y_SNAP && overflowPosY < Y_SNAP * 3 + HALF_Y_SNAP){
            tile.transform.position = new Vector3(tile.transform.position.x, castedPosY + Y_SNAP * 3, tile.transform.position.z);
        }
        else if (overflowPosY >= Y_SNAP * 3 + HALF_Y_SNAP){
            tile.transform.position = new Vector3(tile.transform.position.x, castedPosY + Y_SNAP * 4, tile.transform.position.z);
        } 
    }

    private void Set_SpriteLayer(GameObject tile)
    {
        const int JUMP_OF_TEN = -40;

        if (tile.GetComponent<SpriteRenderer>() != null)
        {
            tile.GetComponent<SpriteRenderer>().sortingOrder = (int)(tile.transform.position.y * JUMP_OF_TEN);
        }
    }

}