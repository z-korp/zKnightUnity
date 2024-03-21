using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles; 

    public GameObject overlayTilePrefab; 
    public GameObject overlayTileContainer;
    public GameObject textPrefab;

    private Dictionary<Vector3Int, GameObject> cellOverlayMap = new Dictionary<Vector3Int, GameObject>();

    private int[,] mapData = new int[8, 8]
    {
        { 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1 },
    };

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int waterIndex = 9;

        for (int x = -1; x <= mapData.GetLength(0); x++)
        {
            for (int y = -1; y <= mapData.GetLength(1); y++)
            {
                Tile tile = tiles[waterIndex]; // Default to water tile at the borders

                if (x >= 0 && x < mapData.GetLength(0) && y >= 0 && y < mapData.GetLength(1))
                {
                    int tileIndex = mapData[x, y];
                    if (tileIndex >= 0 && tileIndex < tiles.Length)
                    {
                        tile = tiles[tileIndex]; // Use the corresponding tile from the array
                    }
                }

                tilemap.SetTile(new Vector3Int(x, y, 0), tile);

                // Overlay instantiation
                if (overlayTilePrefab && overlayTileContainer)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0);
                    Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);
                    GameObject overlayTile = Instantiate(overlayTilePrefab, cellWorldPosition, Quaternion.identity, overlayTileContainer.transform);
                    overlayTile.name = $"OverlayTile_{cellPosition.x}_{cellPosition.y}"; // Custom name
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = 100; // Adjust sorting order as needed
                    cellOverlayMap[cellPosition] = overlayTile;

                    if(textPrefab) {
                        var textMeshObj = Instantiate(textPrefab, cellWorldPosition, Quaternion.identity, overlayTileContainer.transform);
                        textMeshObj.GetComponent<TextMeshPro>().text = $"({x}, {y})";
                        textMeshObj.transform.localScale = Vector3.one * 0.1f; // Adjust scale if needed
                    }
                }
            }
        }
    }

    // Add new methods to show/hide overlay tiles for a given cell
    public void DisplayOverlaysForCells(List<Vector3Int> cellPositions)
    {
        // First, hide all overlays
        HideAllOverlays();

        // Now, show overlays only for the specified cell positions
        foreach (var cellPosition in cellPositions)
        {
            if (cellOverlayMap.TryGetValue(cellPosition, out GameObject overlayTile))
            {
                overlayTile.GetComponent<OverlayTile>().ShowTile();
            }
        }
    }

    public void HideAllOverlays()
    {
        foreach (var overlayEntry in cellOverlayMap)
        {
            overlayEntry.Value.GetComponent<OverlayTile>().HideTile();
        }
    }
}
