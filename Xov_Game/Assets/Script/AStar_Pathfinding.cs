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
        mapTile      = mapData.Get_MapTile();

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

        // (debugging) show manattan distance from the starting node.
        print(tileNodes[(int)startingIndex.y, (int)startingIndex.x].manhattanDistance);



        // Video algo start here.
        List<PathfindingNode> openList   = new List<PathfindingNode>();
        List<PathfindingNode> closedList = new List<PathfindingNode>();

        openList.Add(tileNodes[(int)startingIndex.y, (int)startingIndex.x]);

        //Should start a loop here the video said.
        PathfindingNode currentNode  = openList[0];
        int lowestCombinedValue      = openList[0].combinedValue;
        int currentNodeOpenListIndex = 0;

        for (int i = 0; i < openList.Count; i++)
        {
            if (lowestCombinedValue > openList[i].combinedValue)
            {
                lowestCombinedValue = openList[i].combinedValue;
                currentNode         = openList[i];
                currentNodeOpenListIndex = i;
            }
        }
        openList.RemoveAt(currentNodeOpenListIndex);
        openList.TrimExcess();

        closedList.Add(currentNode);

        TryRebuildPath(currentNode, endingIndex);

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

    private void SetAllNodeId()
    {
        int availableNodeId = 1;

        for (int y = 0; y < MAP_SQR_SIZE; y++)
        {
            for (int x = 0; x < MAP_SQR_SIZE; x++)
            {
                tileNodes[y, x].id = availableNodeId;
                availableNodeId++;
            }
        }
    }

    private void TryRebuildPath(PathfindingNode currentNode, Vector2 endingIndex)
    {
        Vector2 currentNodePosition = currentNode.gameObject.transform.position;
        Vector2 currentNodeIndex = mapData.Get_IndexFromTilePosition(currentNodePosition);

        if (currentNodeIndex == endingIndex)
        {
            // REBUILD PATH HERE
        }
    }

}