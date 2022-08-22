using UnityEngine;

public class Lever : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private bool isActive;
    public bool IsActive { get; }

    public Sprite ActiveSprite;
    public Sprite InactiveSprite;

    private void Awake() {

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void Activate() {

        spriteRenderer.sprite = ActiveSprite;
        isActive = true;

    }

    public void Deactivate() {

        spriteRenderer.sprite = InactiveSprite;
        isActive = false;

    }

}
