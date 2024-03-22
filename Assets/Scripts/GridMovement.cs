using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;
public class GridMovement : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed = 5.0f;
    private Vector3 targetPosition;
    public float cellSize = 0.1f; // Taille de la cellule de ta grille isométrique

    public Tilemap tilemap;

    public CharacterRange characterRange;

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
        Vector3 mousePos = Input.mousePosition;
        // Important: Set the z distance from the camera to the target plane.
        mousePos.z = cam.nearClipPlane - cam.transform.position.z;
         Vector3 worldPoint = cam.ScreenToWorldPoint(mousePos);
         
            Vector3Int cellPosition = tilemap.WorldToCell(worldPoint);

            Vector3Int playerCellPosition = tilemap.WorldToCell(transform.position);
            List<Vector3Int> neighborCells = characterRange.GetNeighborCells(playerCellPosition);
            Debug.Log(neighborCells.First());
            Debug.Log(playerCellPosition);
           if(!neighborCells.Contains(cellPosition))
            {
                return;
            }
            Debug.Log($"{cellPosition}");
            Debug.Log($"{tilemap}");
       
            targetPosition=tilemap.GetCellCenterWorld(cellPosition);
            Debug.Log($"Moving to grid position: {cellPosition} / Adjusted world position: {targetPosition}");
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