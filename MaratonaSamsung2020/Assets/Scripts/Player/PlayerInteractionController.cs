using UnityEngine;

public class PlayerInteractionController : MonoBehaviour {

    private LayerMask interactableObjectsLayerMask;

    private void Awake() {
        
        interactableObjectsLayerMask = ProjectLayers.InteractableLayerMask;

    }

    private void Update() {

        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {

                Vector2 touchPosition = touch.position;
                Ray shootingRay = Camera.main.ScreenPointToRay(touchPosition);

                Vector2 shootingRayOrigin = shootingRay.origin;
                Vector2 shootingRayDirection = shootingRay.direction;

                RaycastHit2D raycastInfo = ShootRaycast(shootingRayOrigin, shootingRayDirection);

                if (raycastInfo.collider != null) {

                    Transform hittedObject = raycastInfo.collider.transform;
                    HandleObjectHit(hittedObject);

                }

            }

        }

    }

    private RaycastHit2D ShootRaycast(Vector2 origin, Vector2 shootingDirection) {

        RaycastHit2D raycastInfo = Physics2D.Raycast(origin, shootingDirection, int.MaxValue, interactableObjectsLayerMask);
        return raycastInfo;

    }

    private void HandleObjectHit(Transform hittedObject) {

        InteractableObject interactionAction = hittedObject.GetComponent<InteractableObject>();
        interactionAction.ExecuteAction();

    }

}
