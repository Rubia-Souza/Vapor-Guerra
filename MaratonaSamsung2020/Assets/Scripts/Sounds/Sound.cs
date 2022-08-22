using UnityEngine;

public class Sound : MonoBehaviour {

    public SoundData SoundData;

    private SoundPropagation propagation;
    private AudioSource audioSource;

    private void Awake() {
        
        propagation = GetComponent<SoundPropagation>();
        audioSource = GetComponent<AudioSource>();

        propagation.HandleSoundEndPropagation += HandleSoundEndPropagation;

    }

    private void OnEnable() {
        
        if (SoundData != null) {

            SetupAudio();

            StartSoundBehaviour();

        }

    }

    private void OnDisable() {
        
        StopSoundBehaviour();

    }

    private void OnDestroy() {
        
        StopSoundBehaviour();
        propagation.HandleSoundEndPropagation -= HandleSoundEndPropagation;

    }

    public void StartSoundBehaviour() {
        
        propagation.StartSoundPropagation();
        audioSource.Play();

    }

    public void StopSoundBehaviour() {

        propagation.StopSoundPropagation();
        audioSource.Stop();

    }

    public void RestartSoundBehaviour() {

        propagation.RestartSoundPropagation();

        audioSource.Stop();
        audioSource.Play();
        
    }
    
    private void SetupAudio() {

        propagation.SoundVelocity = SoundData.Velocity;
        propagation.MaxRadius = SoundData.MaxRadius;
        propagation.PropagationTime = SoundData.PropagationTime;

        audioSource.clip = SoundData.SoundFile;
        
    }

    private void HandleSoundEndPropagation() {

        gameObject.SetActive(false);

    }
    
}
