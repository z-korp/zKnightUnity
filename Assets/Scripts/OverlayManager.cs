using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class OverlayManager : MonoBehaviour
{
    public Tilemap targetTilemap; // Assign in the inspector
    public GameObject overlayContainer; // Parent GameObject containing all overlays

    private Dictionary<Vector3Int, GameObject> cellOverlayMap;

    void Awake()
    {
        // Initialize the dictionary to keep track of overlays by cell position
        cellOverlayMap = new Dictionary<Vector3Int, GameObject>();
        InitializeOverlayMap();
    }

    // Associate each overlay with a Tilemap cell based on its position
    private void InitializeOverlayMap()
    {
        foreach (Transform overlayTransform in overlayContainer.transform)
        {
            GameObject overlay = overlayTransform.gameObject;
            Vector3Int cellPosition = targetTilemap.WorldToCell(overlay.transform.position);
            cellOverlayMap[cellPosition] = overlay;
            overlay.SetActive(false); // Start with all overlays hidden
        }
    }

    // Display an overlay at a specified cell
    public void DisplayOverlayAtCell(Vector3Int cellPosition)
    {
        if (cellOverlayMap.TryGetValue(cellPosition, out GameObject overlay))
        {
            overlay.SetActive(true);
            // Adjust transparency to 0% (fully visible)
            var color = overlay.GetComponent<SpriteRenderer>().color;
            color.a = 1f; // Fully opaque
            overlay.GetComponent<SpriteRenderer>().color = color;
        }
    }

    // Hide an overlay at a specified cell
    public void HideOverlayAtCell(Vector3Int cellPosition)
    {
        if (cellOverlayMap.TryGetValue(cellPosition, out GameObject overlay))
        {
            // Adjust transparency to 100% (fully transparent)
            var color = overlay.GetComponent<SpriteRenderer>().color;
            color.a = 0f; // Fully transparent
            overlay.GetComponent<SpriteRenderer>().color = color;
            overlay.SetActive(false);
        }
    }
}
