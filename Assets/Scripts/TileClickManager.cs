using UnityEngine;

public class TileClickManager : MonoBehaviour
{
    void Update()
    {
        // Check for a left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked!");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit.collider != null)
            {
                // Check if the clicked GameObject has an OverlayTile component
                OverlayTile overlayTile = hit.collider.gameObject.GetComponent<OverlayTile>();
                if (overlayTile != null)
                {
                    // Toggle tile visibility as an example
                    if (overlayTile.gameObject.GetComponent<SpriteRenderer>().color.a == 1)
                    {
                        overlayTile.HideTile();
                    }
                    else
                    {
                        overlayTile.ShowTile();
                    }
                }
            }
        }
    }
}
