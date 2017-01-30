using UnityEngine;
using System.Collections;

public class AStar_Pathfinding : MonoBehaviour {

    private Vector2 startingIndex = new Vector2(1, 12);
    private Vector2 endingIndex;

    //RaycastHit2D hit;
    RaycastHit2D[] hits;

    void Start ()
    {
	
	}

	void Update ()
    {

        //hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //if (hit.collider != null)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        endingIndex = hit.transform.position;
        //        
        //        // SEARCH PATHFINDING HERE?
        //        print(endingIndex);
        //
        //    }
        //}

        hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hits.Length != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Find closess layer collider.
                GameObject wantedTile = hits[0].collider.gameObject;
                int layer = hits[0].collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

                for (int i = 1; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder > layer)
                        wantedTile = hits[i].collider.gameObject;
                }

                endingIndex = wantedTile.transform.position;
                print(endingIndex);

                // SEARCH PATHFINDING HERE?

            }
        }



    }

}