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

    public MapGenerator mapGenerator;

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
        Debug.Log("Tilemap1: " + tilemap1);
    }

    void Update()
    {
        var focusedTileHit = GetFocusedOnTile();
        if (focusedTileHit.HasValue)
        {
            TileHit tileHit = focusedTileHit.Value;

            // Check if the cell position is within the range [0, 8)
            if (tileHit.CellPosition.x >= 0 && tileHit.CellPosition.x < 8 && tileHit.CellPosition.y >= 0 && tileHit.CellPosition.y < 8)
            {
                // If in range, update the cursor position and make sure it's visible
                Vector3 worldPosition = tileHit.WorldPosition;
                transform.position = worldPosition;
                GetComponent<SpriteRenderer>().enabled = true; // Make cursor visible
            }
            else
            {
                // If not in range, ensure the cursor is hidden
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            // If no tile is focused, hide the cursor
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public TileHit? GetFocusedOnTile()
    {
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

                        if (cellPosition.x < 0 || cellPosition.x >= 8 || cellPosition.y < 0 || cellPosition.y >= 8)
                        {
                            continue; // Skip this hit and check the next one, if any
                        }
                        
                        // Obtient la position du monde du centre de la cellule frappée.
                        Vector3 cellCenterPos = tilemapHit.GetCellCenterWorld(cellPosition);
                        

                        TileHit tileHit = new TileHit
                        {
                            RaycastHit = hit,
                            CellPosition = cellPosition,
                            WorldPosition = cellCenterPos
                        };

                        //Debug.Log("TileHit: " + tileHit.RaycastHit.collider.name + " " + tileHit.CellPosition + " " + tileHit.WorldPosition);
                        return tileHit;
                    }
                }
            }
        }

        return null;
    }
}
