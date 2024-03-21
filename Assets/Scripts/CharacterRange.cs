using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterRange : MonoBehaviour
{
    public Tilemap tilemap;
    public Camera cam;
    public MapGenerator mapGenerator;

    private Vector3Int? lastHoveredCell = null;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        // Important: Set the z distance from the camera to the target plane.
        mousePos.z = cam.nearClipPlane - cam.transform.position.z;
        Vector3 worldPoint = cam.ScreenToWorldPoint(mousePos);
        Vector3Int mouseCellPosition = tilemap.WorldToCell(worldPoint);

        // Get the cell position of the player
        Vector3 playerWorldPoint = transform.position;
        Vector3Int playerCellPosition = tilemap.WorldToCell(playerWorldPoint);

        Debug.Log($"Mouse cell position: {mouseCellPosition}");
        Debug.Log($"Player cell position: {playerCellPosition}");

        // Check if mouse is over the player's cell
        if (mouseCellPosition == playerCellPosition)
        {
            if (!lastHoveredCell.HasValue || lastHoveredCell.Value != playerCellPosition)
            {
                // Mouse is newly hovered over the player
                List<Vector3Int> cellsToShowOverlay = GetNeighborCells(playerCellPosition);
                mapGenerator.DisplayOverlaysForCells(cellsToShowOverlay);
                lastHoveredCell = playerCellPosition; // Remember the last hovered cell
            }
        }
        else if (lastHoveredCell.HasValue)
        {
            // Mouse moved away from the player, clear overlays
            mapGenerator.HideAllOverlays(); // Assuming you have a method to hide all overlays
            lastHoveredCell = null; // Clear the last hovered cell
        }
    }

    public List<Vector3Int> GetNeighborCells(Vector3Int cellPosition)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        // Directly adjacent cells only (no diagonals, no current cell)
        int[] deltas = new int[] {-1, 1};

        // Horizontal neighbors
        foreach (int dx in deltas)
        {
            Vector3Int neighborPos = new Vector3Int(cellPosition.x + dx, cellPosition.y, cellPosition.z);
            neighbors.Add(neighborPos);
        }

        // Vertical neighbors
        foreach (int dy in deltas)
        {
            Vector3Int neighborPos = new Vector3Int(cellPosition.x, cellPosition.y + dy, cellPosition.z);
            neighbors.Add(neighborPos);
        }

        return neighbors;
    }
}

