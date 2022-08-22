using UnityEngine;

public class InteractableObjectVisualResponse : MonoBehaviour {

    private const int InteractableObjectLayerIndex = ProjectLayers.InteractableLayerMaskIndex;

    private Animator feedbackAnimator;

    public Sprite interactionSymbol;
    public GameObject interactionFeedbackPoint;

    private void Awake() {

        feedbackAnimator = interactionFeedbackPoint.GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        GameObject collisionObject = collision.gameObject;

        if (collisionObject.layer == InteractableObjectLayerIndex) {

            ShowInteractionSymbol();

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision) {

        GameObject collisionObject = collision.gameObject;

        if (collisionObject.layer == InteractableObjectLayerIndex) {

            HideInteractionSymbol();

        }

    }

    private void ShowInteractionSymbol() {

        interactionFeedbackPoint.SetActive(true);
        feedbackAnimator.enabled = true;

        feedbackAnimator.Play("Questioning");

    }

    private void HideInteractionSymbol() {

        feedbackAnimator.enabled = false;
        interactionFeedbackPoint.SetActive(false);

    }

}
