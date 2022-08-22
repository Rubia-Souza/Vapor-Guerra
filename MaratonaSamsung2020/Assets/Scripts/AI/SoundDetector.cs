using UnityEngine;

public class SoundDetector : MonoBehaviour {
    
    public delegate void OnSoundListen(Transform soundTransform);
    public event OnSoundListen AIHandleSoundDetection;

    private void OnTriggerEnter2D(Collider2D collider) {
        
        AIHandleSoundDetection(collider.transform);

    }

}
