using UnityEngine;
using System.Collections;

public class AStar_Pathfinding : MonoBehaviour {

    #region DELCARATION
    // CONST

    // PRIVATE
    private MapDataParser mapData;

    // PUBLIC

    #endregion

    #region UNITY METHODE
    void Awake ()
    {
        mapData = gameObject.GetComponent<MapDataParser>();
    }
    #endregion

    public void FindPath(Vector2 StartingIndex, Vector2 EndingIndex)
    {
        // SEARCH PATHFINDING HERE
        print("Code Pathfinding You Lazy Ass!");

        // Do nothing if the same index.
        if (StartingIndex == EndingIndex)
            return;








    }

}