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

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
       
            targetPosition=tilemap.GetCellCenterWorld(cellPosition);

            Vector3 moveVector = cellPosition - playerCellPosition;
            Debug.Log($"MoveVector: {moveVector}");
            animator.SetFloat("MoveX", moveVector.x);
            animator.SetFloat("MoveY", moveVector.y);
    }

    void MoveCharacter(Vector3 targetPosition)
{
    if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
    {
        Vector3 intermediatePosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        if (Vector3.Distance(transform.position, intermediatePosition) > 0.1f)
        {
            // Debug.Log($"Moving character from {transform.position} to {intermediatePosition}");
            transform.position = Vector3.MoveTowards(transform.position, intermediatePosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Debug.Log($"Moving character from {transform.position} to {targetPosition}");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
}