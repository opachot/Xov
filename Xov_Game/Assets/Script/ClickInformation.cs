using UnityEngine;
using System.Collections;

public class ClickInformation : MonoBehaviour {

    #region DELCARATION
    // CONST

    // PRIVATE
    private RaycastHit2D[] hits;

    // PUBLIC

    #endregion

    #region UNITY METHODE
    void Update()
    {
        
    }
    #endregion

    public Vector2 Get_TilePositionClicked()
    {
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
                    {
                        wantedTile = hits[i].collider.gameObject;
                        layer = hits[i].collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                    } 
                }

                return wantedTile.transform.position;
            }
        }

        return new Vector2(0, 0);
    } 

}