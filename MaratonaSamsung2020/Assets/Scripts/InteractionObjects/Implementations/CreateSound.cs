using UnityEngine;

public class CreateSound : InteractableObject {

    public SoundData sound;
    public SoundGeneratedEvent soundGeneratedEvent;
    public GameObject soundPosition;

    protected override void Action() {

        sound.Position = soundPosition.transform.position;

        soundGeneratedEvent.Raise(sound);

    }
    
}
