using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed = 5.0f;
    private Vector3 targetPosition;
    public float cellSize = 0.1f; // Taille de la cellule de ta grille isométrique

    public Tilemap tilemap;

    private void Start()
    {
        targetPosition = transform.position;
        Debug.Log($"Start position: {targetPosition}");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToGridPosition();
        }

        MoveCharacter(targetPosition);
    }

    void MoveToGridPosition()
    {
         Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPoint);
            Debug.Log($"{cellPosition}");
            Debug.Log($"{tilemap}");
            TileBase clickedTile = tilemap.GetTile(cellPosition);

            if (clickedTile != null)
            {
                Debug.Log($"Tu as cliqué sur la tile à la position {cellPosition} de type {clickedTile.name}");
                // Ici, tu peux ajouter plus de logique basée sur le type de tile ou sa position.
            }

         Vector2 rayPos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2 = Physics2D.Raycast(rayPos, Vector2.zero);

        if (hit2.collider != null)
        {
            Debug.Log($"Tu as cliqué sur : {hit2.collider.name}");
            // Tu peux ajouter plus de logique ici pour gérer le clic sur des éléments spécifiques de la Tilemap.
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // if (Physics.Raycast(ray, out hit))
        // {
            // Convertit la position du monde en position de grille
            // Vector2 gridPos = WorldToGridPosition(hit.point);
            // Convertit la position de grille en position du monde, ajustée au centre de la cellule de grille
            Vector2 cellPosition2D = new Vector2(cellPosition.x, cellPosition.y);
            targetPosition = GridToWorldPosition(cellPosition2D);
            Debug.Log($"Moving to grid position: {cellPosition} / Adjusted world position: {targetPosition}");
        // }
    }

//     Vector2 WorldToGridPosition(Vector3 worldPosition)
// {
//     // Ajuste ces calculs en fonction de l'orientation spécifique de ta grille isométrique
//     float x = Mathf.Round(worldPosition.x / cellSize-worldPosition.y)*cellSize /2;
//     float y = Mathf.Round(worldPosition.y / cellSize); // Utilise Y au lieu de Z
//     Debug.Log($"World position: {worldPosition} converted to grid position: {new Vector2(x, y)}");
//     return new Vector2(x, y);
// }

// Vector3 GridToWorldPosition(Vector2 gridPosition)
// {
//     // Convertit la position de grille en position du monde, centrée sur la cellule de la grille
//     float x = (gridPosition.x - gridPosition.y) * cellSize;
//     float y = (gridPosition.x + gridPosition.y) * cellSize / 2; // Utilise Y au lieu de Z
//     Debug.Log($"Grid position: {gridPosition} converted to world position: {new Vector3(x, y, transform.position.z)}"); // Utilise Z au lieu de Y
//     return new Vector3(x, y, transform.position.z); // Garde la hauteur originale du personnage
// }

 Vector2 WorldToGridPosition(Vector3 worldPosition)
    {
        float x = Mathf.Round((worldPosition.x / cellSize - worldPosition.y / (2 * cellSize)));
        float y = Mathf.Round((worldPosition.x / cellSize + worldPosition.y / (2 * cellSize)));
        // Debug.Log($"World position: {worldPosition} converted to grid position: {new Vector2(x, y)}");
        return new Vector2(x, y);
    }

    Vector3 GridToWorldPosition(Vector2 gridPosition)
    {
        float x = (gridPosition.x + gridPosition.y) * cellSize / 2;
        float y = (gridPosition.y - gridPosition.x) * cellSize / 2;
        // Debug.Log($"Grid position: {gridPosition} converted to world position: {new Vector3(x, y, transform.position.z)}");
        return new Vector3(x, y, transform.position.z);
    }
    void MoveCharacter(Vector3 targetPosition)
{
    if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
    {
        Vector3 intermediatePosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        if (Vector3.Distance(transform.position, intermediatePosition) > 0.1f)
        {
            Debug.Log($"Moving character from {transform.position} to {intermediatePosition}");
            transform.position = Vector3.MoveTowards(transform.position, intermediatePosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log($"Moving character from {transform.position} to {targetPosition}");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
}