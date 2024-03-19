using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            
            return _instance;
        }
    }

    public GameObject overlayTilePrefab;
    public GameObject overlayTileContainer;

    public Dictionary<Vector2Int,OverlayTile> map;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
            
        }
        else{
             _instance = this;
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        var tileMaps = gameObject.GetComponentsInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTile>();
        foreach (var tileMap in tileMaps)
        {
            BoundsInt bounds = tileMap.cellBounds;

            for(int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for(int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    for(int z = bounds.zMin; z < bounds.zMax; z++)
                    {
                        Vector3Int localPlace = (new Vector3Int(x, y,z));
                         var overlayTile= Instantiate(overlayTilePrefab, overlayTileContainer.transform);
                            var cellWorldPosition = tileMap.GetCellCenterWorld(localPlace);

                            Debug.Log("Cell world position: " + cellWorldPosition);

                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                            overlayTile.GetComponent<SpriteRenderer>().sortingOrder= tileMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                        
                        }
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
