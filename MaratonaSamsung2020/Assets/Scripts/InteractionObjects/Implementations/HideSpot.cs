using UnityEngine;

public class HideSpot : InteractableObject {

    private const int PlayerLayerMaskIndex = ProjectLayers.PlayerLayerMaskIndex;
    private const int UndetectableLayerMaskIndex = ProjectLayers.UndetectableLayerMaskIndex;

    Transform playerTransform;

    protected new void OnTriggerEnter2D(Collider2D collider) {
        
        base.OnTriggerEnter2D(collider);

        if (collider.CompareTag(PlayerTag)) {

            playerTransform = collider.transform;

        }

    }

    protected new void OnTriggerExit2D(Collider2D collider) {

        base.OnTriggerExit2D(collider);

        if (playerTransform != null) {

            PlayerLeavesTheSpot();

        }

    }

    protected override void Action() {

       if (playerTransform != null) {

            PlayerHidesInSpot();

       }

    }

    private void PlayerHidesInSpot() {

        playerTransform.gameObject.layer = UndetectableLayerMaskIndex;

    }

    private void PlayerLeavesTheSpot() {

        playerTransform.gameObject.layer = PlayerLayerMaskIndex;
        playerTransform = null;

    }

}
