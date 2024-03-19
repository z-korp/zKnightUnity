using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public struct TileHit
{
    public RaycastHit2D RaycastHit;
    public Vector3Int CellPosition;
    public Vector3 WorldPosition;
}
public class MouseController : MonoBehaviour
{
   public Tilemap tilemap1;
public Tilemap tilemap2;

void Start()
{
    Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();
    foreach (Tilemap tilemap in tilemaps)
    {
        if (tilemap.name == "Tilemap")
        {
            tilemap1 = tilemap;
        }
        else if (tilemap.name == "Tilemap level 1")
        {
            tilemap2 = tilemap;
        }
    }
}

    // Update is called once per frame
    void Update()
{
    Debug.Log("Update started");
    var focusedTileHit = GetFocusedOnTile();
    if(focusedTileHit.HasValue)
    {
        TileHit tileHit = focusedTileHit.Value;
        Vector3 worldPosition = tilemap1.GetCellCenterWorld(tileHit.CellPosition);
       
        transform.position = tileHit.WorldPosition; // Utilise la position correcte du centre de la cellule
       
        // TilemapRenderer tilemap =tileHit.RaycastHit.collider.GetComponent<TilemapRenderer>();
        // Debug.Log("Tilemap: " + tilemap);
        // Debug.Log("Tilemap: " + tilemap.name);



        gameObject.GetComponent<SpriteRenderer>().sortingOrder = tileHit.RaycastHit.collider.GetComponent<TilemapRenderer>().sortingOrder;
        // Debug.Log("Sorting order: " + gameObject.GetComponent<SpriteRenderer>().sortingOrder);
    }
    else
    {
        Debug.Log("No tile hit found");
    }
}

public TileHit? GetFocusedOnTile()
{
    Debug.Log("GetFocusedOnTile started");
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
    RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

    if (hits.Length > 0)
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                Tilemap tilemapHit = hit.collider.GetComponent<Tilemap>();
                if (tilemapHit != null)
                {
                    
                    // Obtient la position de la cellule à partir de la position du monde.
                    Vector3Int cellPosition = tilemapHit.WorldToCell(hit.point);
                    
                    // Obtient la position du monde du centre de la cellule frappée.
                    Vector3 cellCenterPos = tilemapHit.GetCellCenterWorld(cellPosition);
                    

                    TileHit tileHit = new TileHit
                    {
                        RaycastHit = hit,
                        CellPosition = cellPosition,
                        WorldPosition = cellCenterPos
                    };

                    Debug.Log("TileHit: " + tileHit.RaycastHit.collider.name + " " + tileHit.CellPosition + " " + tileHit.WorldPosition);
                    return tileHit;
                }
            }
        }
    }
    else
    {
        Debug.Log("No hits found");
    }

    return null;
}

}
