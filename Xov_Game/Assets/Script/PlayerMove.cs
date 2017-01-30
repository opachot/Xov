using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    #region DELCARATION
    // CONST
    private Vector2 NO_TILE_POS_FOUND = new Vector2(0, 0);
    private Vector2 NO_INDEX_FOUND = new Vector2(-666, -666);

    // PRIVATE
    private MapDataParser mapData;
    private ClickInformation clickInfo;
    private AStar_Pathfinding aStar_Pathfinding;

    private Vector2 startingIndex = new Vector2(1, 12);
    private Vector2 endingIndex;

    // PUBLIC

    #endregion

    #region UNITY METHODE
    void Awake()
    {
        mapData = Camera.main.GetComponent<MapDataParser>();
        clickInfo = Camera.main.GetComponent<ClickInformation>();
        aStar_Pathfinding = Camera.main.GetComponent<AStar_Pathfinding>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SetMovingLocation())
            {
                print("Index Destination: " + endingIndex);
                // WARNING: Also give number of Moving Point available?
                aStar_Pathfinding.FindPath(startingIndex, endingIndex);
            }
        }
    }
    #endregion

    private bool SetMovingLocation()
    {
        bool movingLocationSet = false;

        Vector2 destinationTilePos = clickInfo.Get_TilePositionClicked();
        bool tilePosFound = destinationTilePos != NO_TILE_POS_FOUND;

        if (tilePosFound)
        {
            Vector2 indexOfTile = mapData.Get_IndexFromTilePosition(destinationTilePos);
            bool tileIndexFound = indexOfTile != NO_INDEX_FOUND;

            if (tileIndexFound)
            {
                endingIndex = indexOfTile;
                movingLocationSet = true;
            }
        }

        return movingLocationSet;
    }

}