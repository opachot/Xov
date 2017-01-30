using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDataParser : MonoBehaviour {

    #region DECLARATION
    // CONST
    private const int MAP_SQR_SIZE = 25;

    private const int STARTING_POS = 12;
    private const float X_JUMP     = 0.5f;
    private const float Y_JUMP     = 0.25f;

    private const int WALKABLE = 0;
    private const int HOLE     = 1;
    private const int OBSTACLE = 2;

    // PRIVATE
    List<GameObject> allDataTile = new List<GameObject>();
    private int[,] MapData = new int[MAP_SQR_SIZE, MAP_SQR_SIZE];

    public GameObject object_MapData;

    // PUBLIC

    #endregion

    #region UNITY METHODE
    void Awake ()
    {
        PlaceMapDataVisualy();

        ListingAllTile();

        ForLoopingInMap();

        TurnTileInvisible();
    }
    #endregion

    private void PlaceMapDataVisualy()
    {
        object_MapData.SetActive(true);
    }

    private void ListingAllTile()
    {
        foreach (Transform t in object_MapData.transform)
            allDataTile.Add(t.gameObject);
    }

    private void TurnTileInvisible()
    {
        foreach (Transform t in object_MapData.transform)
        {
            SpriteRenderer tileRenderer = t.gameObject.GetComponent<SpriteRenderer>();
            Color tileColor = tileRenderer.color;
            tileRenderer.color = new Color(tileColor.r, tileColor.g, tileColor.b, 0);
        }
    }


    private void ForLoopingInMap()
    {
        for (int y = 0; y < MAP_SQR_SIZE; y++)
        {
            // Starting position of the x line.
            float searchingPosX = STARTING_POS - (X_JUMP * y);
            float searchingPosY = STARTING_POS - (Y_JUMP * y);

            for (int x = 0; x < MAP_SQR_SIZE; x++)
            {
                ForLoopingInTilesList(searchingPosX, searchingPosY, x, y);

                SwitchTileInX(ref searchingPosX, ref searchingPosY);
            }
        }
    }

    private void SwitchTileInX(ref float searchingPosX, ref float searchingPosY)
    {
        searchingPosX += X_JUMP;
        searchingPosY -= Y_JUMP;
    }

    private void ForLoopingInTilesList(float searchingPosX, float searchingPosY, int indexX, int indexY)
    {
        for (int i = 0; i < allDataTile.Count; i++)
        {
            if (IsTileMatchingPosition(allDataTile[i], searchingPosX, searchingPosY))
            {
                SavaMapDataInIndex(i, indexX, indexY);
                RemoveTileFromList(i);

                // Get out of the Tile Searching forloop.
                break;
            }
        }
    }

    private bool IsTileMatchingPosition(GameObject tile, float posX, float posY)
    {
        Vector3 tilePosition = tile.transform.position;
        Vector3 SearchedPosition = new Vector3(posX, posY, tilePosition.z);

        bool isTileMatchingPosition = (tilePosition == SearchedPosition);
        return isTileMatchingPosition;
    }

    private void SavaMapDataInIndex(int tileIndex, int dataIndexX, int dataIndexY)
    {
        if (allDataTile[tileIndex].CompareTag("WALKABLE"))
        {
            print("WALKABLE");
            MapData[dataIndexY, dataIndexX] = WALKABLE;
        }
        else if (allDataTile[tileIndex].CompareTag("HOLE"))
        {
            print("HOLE");
            MapData[dataIndexY, dataIndexX] = HOLE;
        }
        else if (allDataTile[tileIndex].CompareTag("OBSTACLE"))
        {
            print("OBSTACLE");
            MapData[dataIndexY, dataIndexX] = OBSTACLE;
        }
        else
        {
            // Place an obstacle if an error happen...
            Debug.Log("ERROR while parsing the map data in MapDataParser script: TAG NOT FOUND");
            MapData[dataIndexY, dataIndexX] = OBSTACLE;
        }
    }

    private void RemoveTileFromList(int index)
    {
        allDataTile.RemoveAt(index);
        allDataTile.TrimExcess();
    }

}