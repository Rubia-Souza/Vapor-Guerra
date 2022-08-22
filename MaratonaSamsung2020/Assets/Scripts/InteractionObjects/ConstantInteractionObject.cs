using System.Collections;
using UnityEngine;

public abstract class ConstantInteractionObject : InteractableObject {

    private const float InteractionDelay = 1f;

    protected float actualInteractionProgress;
    protected float ActualInteractionProgress {

        get => actualInteractionProgress;
        set {

            actualInteractionProgress = value;
            interactionProgressChangedUIEvent.Raise(ActualInteractionProgressPorcentage);

        }

    }

    private Coroutine interactionCounterIncreaseCoroutine;

    public float ActualInteractionProgressPorcentage {

        get => (ActualInteractionProgress / maxInteractionValue);

    }

    [SerializeField] private float interactionSpeed;
    public float InteractionSpeed {

        get => interactionSpeed;
        protected set => interactionSpeed = value;

    }

    [SerializeField] private float maxInteractionValue;
    public float MaxInteractionValue {

        get => maxInteractionValue;
        set => maxInteractionValue = value;

    }

    public GameObject interactionFeedback;
    public InteractionProgressChangedUIEvent interactionProgressChangedUIEvent;

    protected new void OnTriggerExit2D(Collider2D collider) {

        base.OnTriggerExit2D(collider);

        StopAction();

    }

    public void StopAction() {

        StopInteractionCouting();

    }

    protected override void Action() {

        StartInteractionCouting();

    }

    private void StartInteractionCouting() {

        StopInteractionCouting();
        interactionCounterIncreaseCoroutine = StartCoroutine(InteractionCounterIncrease());

        EnableVisualFeedBack();

    }

    private IEnumerator InteractionCounterIncrease() {

        while (ActualInteractionProgress < maxInteractionValue) {

            ActualInteractionProgress += InteractionSpeed;
            interactionProgressChangedUIEvent.Raise(ActualInteractionProgressPorcentage);

            yield return new WaitForSeconds(InteractionDelay); 

        }

        OnInteractionCompleated();
        StopInteractionCouting();

    }

    private void StopInteractionCouting() {

        if (interactionCounterIncreaseCoroutine != null) {

            StopCoroutine(interactionCounterIncreaseCoroutine);
            DesableVisualFeedBack();

            ActualInteractionProgress = 0;

        }

    }

    protected virtual void EnableVisualFeedBack() {

        interactionFeedback.SetActive(true);

    }

    protected virtual void DesableVisualFeedBack() {

        interactionFeedback.SetActive(false);

    }

    protected abstract void OnInteractionCompleated();

}
