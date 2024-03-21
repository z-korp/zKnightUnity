using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowTile() {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void HideTile() {
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }
}