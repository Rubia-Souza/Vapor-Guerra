using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {

    protected readonly string PlayerTag = ProjectTags.PlayerTag;
    protected bool isInRangeToInteract;

    protected void OnTriggerEnter2D(Collider2D collider) { 

        if (collider.CompareTag(PlayerTag)) {

            isInRangeToInteract = true;

        }

    }

    protected void OnTriggerExit2D(Collider2D collider) {

        if (collider.CompareTag(PlayerTag)) {

            isInRangeToInteract = false;

        }

    }

    public void ExecuteAction() {

        if (isInRangeToInteract) {

            Action();

        }

    }

    protected abstract void Action();

}
