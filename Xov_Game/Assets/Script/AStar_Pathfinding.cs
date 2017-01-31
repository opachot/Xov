using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar_Pathfinding : MonoBehaviour {

    #region DELCARATION
    // CONST
    private const int MAP_SQR_SIZE = 25;
    private const int WALKABLE = 0;

    // PRIVATE
    private MapDataParser mapData;

    private int[,] mapStateData;
    private GameObject[,] mapTile;

    private PathfindingNode[,] tileNodes = new PathfindingNode[MAP_SQR_SIZE, MAP_SQR_SIZE];

    // PUBLIC

    #endregion

    #region UNITY METHODE
    void Awake ()
    {
        mapData = gameObject.GetComponent<MapDataParser>();
    }

    void Start()
    {
        mapStateData = mapData.Get_MapData();
        mapTile = mapData.Get_MapTile();

        for (int y = 0; y < MAP_SQR_SIZE; y++)
        {
            for (int x = 0; x < MAP_SQR_SIZE; x++)
            {
                tileNodes[y, x] = mapTile[y, x].GetComponent<PathfindingNode>();
            }
        }
    }
    #endregion

    public void FindPath(Vector2 startingIndex, Vector2 endingIndex)
    {
        if (!IsValidSituation(startingIndex, endingIndex))
        {
            print("Invalide destination");
            return;
        }

        SetAllNodeManhattanDistance(endingIndex);

        


        // Video algo start here.
        List<PathfindingNode> openList = new List<PathfindingNode>();
        List<PathfindingNode> closedList = new List<PathfindingNode>();

        openList.Add(tileNodes[(int)startingIndex.y, (int)startingIndex.x]);
        print(openList[0].manhattanDistance);


    }

    private bool IsValidSituation(Vector2 startingIndex, Vector2 endingIndex)
    {
        bool isValidSituation = true;

        // Do nothing if the same index.
        if (startingIndex == endingIndex)
            isValidSituation = false;

        // Do nothing if the destination is not walkable.
        else if (!IsWalkableNode(endingIndex))
            isValidSituation = false;

        return isValidSituation;
    }

    private void SetAllNodeManhattanDistance(Vector2 endingIndex)
    {
        for (int y = 0; y < MAP_SQR_SIZE; y++)
        {
            for (int x = 0; x < MAP_SQR_SIZE; x++)
            {
                Vector2 nodeIndex = new Vector2(x, y);

                SetNodeManhattanDistance(nodeIndex, endingIndex);
            }
        }
    }

    private void SetNodeManhattanDistance(Vector2 nodeIndex, Vector2 endingIndex)
    {
        if (IsWalkableNode(nodeIndex))
        {
            // Calculating Manhattan...
            int distanceX = (int)Mathf.Abs(nodeIndex.x - endingIndex.x);
            int distanceY = (int)Mathf.Abs(nodeIndex.y - endingIndex.y);
            int manhattanDistance = distanceX + distanceY;

            tileNodes[(int)nodeIndex.y, (int)nodeIndex.x].manhattanDistance = manhattanDistance;
        }
    }

    private bool IsWalkableNode(Vector2 nodeIndex)
    {
        bool isWalkableNode = mapStateData[(int)nodeIndex.y, (int)nodeIndex.x] == WALKABLE;
        return isWalkableNode;
    }

}