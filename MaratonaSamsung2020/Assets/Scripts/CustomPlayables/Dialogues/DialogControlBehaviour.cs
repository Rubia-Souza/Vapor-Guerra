using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DialogControlBehaviour : PlayableBehaviour {
  public Dialogue dialogue;
  public GameEvent dialogueEndedEvent;

  private bool dialogueStarted;
  private PlayableDirector timelineDirector;

  public override void OnPlayableCreate(Playable playable) {
    dialogueStarted = false;
		timelineDirector = (playable.GetGraph().GetResolver() as PlayableDirector);
	}

  public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
      DialogueController controller = playerData as DialogueController;

      if(!dialogueStarted) {
        controller.StartDialogue(dialogue);
        dialogueStarted = true;
        this.HandleDialogueStart();
      }
  }

  private void HandleDialogueStart() {
    TimelineUtil.PauseTimeline(timelineDirector);
    dialogueEndedEvent.RegisterCallback(HandleDialogueEnd);
  }

  private void HandleDialogueEnd() {
    TimelineUtil.ResumeTimeline(timelineDirector);
    dialogueEndedEvent.UnregisterCallback(HandleDialogueEnd);
  }
}
