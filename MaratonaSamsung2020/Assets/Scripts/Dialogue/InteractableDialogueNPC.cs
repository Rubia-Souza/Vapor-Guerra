using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableDialogueNPC : InteractableObject {
    public Dialogue dialogue;
    public DialogueController dialogueController;

    public GameEvent DialogueEndedEvent;

    public UnityEvent onDialogEnd;

    protected override void Action() {
        dialogueController.StartDialogue(dialogue);
        DialogueEndedEvent.RegisterCallback(ExecuteDialogEndCallbacks);
    }

    private void ExecuteDialogEndCallbacks() {
        onDialogEnd.Invoke();
        DialogueEndedEvent.UnregisterCallback(ExecuteDialogEndCallbacks);
    }
}
