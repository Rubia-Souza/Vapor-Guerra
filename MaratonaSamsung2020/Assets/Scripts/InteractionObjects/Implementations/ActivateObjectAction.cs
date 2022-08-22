using UnityEngine.Events;

public class ActivateObjectAction : InteractableObject {

    public UnityEvent action;

    protected override void Action() {

        CallAction();

    }

    protected virtual void CallAction() {

        action.Invoke();

    }
}
