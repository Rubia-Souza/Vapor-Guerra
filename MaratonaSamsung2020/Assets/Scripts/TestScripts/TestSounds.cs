using UnityEngine;

public class TestSounds : MonoBehaviour {
    
    public SoundGeneratedEvent soundGeneratedEvent;
    public SoundData soundGeneratedData;

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Space)) {

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soundGeneratedData.Position = clickPosition;

            soundGeneratedEvent.Raise(soundGeneratedData);

        }

    }

}
