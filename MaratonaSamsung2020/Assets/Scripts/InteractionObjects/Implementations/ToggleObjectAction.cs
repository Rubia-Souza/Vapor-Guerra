using UnityEngine.Events;

public class ToggleObjectAction : ActivateObjectAction {

    private bool shouldMakeCounterAction = false;

    public UnityEvent counterAction;

    protected override void CallAction() {

        if (!shouldMakeCounterAction) {

            base.CallAction();

        }
        else {

            counterAction.Invoke();

        }
        
        shouldMakeCounterAction = !shouldMakeCounterAction;

    }

}
