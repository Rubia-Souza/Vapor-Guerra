using UnityEngine;

public class PlayerInUndetectableLayerVisualEffect : MonoBehaviour {

    private readonly int PlayerLayerMaskIndex = ProjectLayers.PlayerLayerMaskIndex;
    private readonly int UndetectableLayerMaskIndex = ProjectLayers.UndetectableLayerMaskIndex;

    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Color playerHiddenIndicationSpriteColor;

    private void Awake() {

        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;

    }

    private void Update(){

        int playerActualLayerMask = gameObject.layer;

        if (spriteRenderer.color == originalColor && playerActualLayerMask == UndetectableLayerMaskIndex) {

            ChangeSpriteColor(playerHiddenIndicationSpriteColor);

        }
        else if (spriteRenderer.color != originalColor && playerActualLayerMask == PlayerLayerMaskIndex) {

            ChangeSpriteColor(originalColor);

        }

    }

    private void ChangeSpriteColor(Color color) {

        spriteRenderer.color = color;

    }

}
