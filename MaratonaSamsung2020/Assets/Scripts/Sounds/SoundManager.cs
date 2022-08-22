using UnityEngine;

public class SoundManager : MonoBehaviour {

    private Pool<GameObject> poolSounds;

    [SerializeField] private GameObject SoundPointPrefab;
    [SerializeField] private SoundGeneratedEvent soundGeneratedEvent;

    [SerializeField] private int maxInstancesOfSounds; 
    public int MaxInstancesOfSounds { 

        get => maxInstancesOfSounds; 
        private set => maxInstancesOfSounds = value;

    }

    private void Awake() {

        poolSounds = new SoundsPool(SoundPointPrefab, maxInstancesOfSounds);

    }

    private void Start() {
        
        soundGeneratedEvent.RegisterCallback(EnableAudio);

    }

    private void OnDisable() {
        
        soundGeneratedEvent.UnregisterCallback(EnableAudio);

    }

    private void OnDestroy() {
        
        soundGeneratedEvent.UnregisterCallback(EnableAudio);

    }

    private void EnableAudio(SoundData soundData) {

        GameObject soundPoint = poolSounds.GetItem();
        Sound sound = soundPoint.GetComponent<Sound>();

        soundPoint.SetActive(false);

        soundPoint.transform.position = soundData.Position;

        sound.SoundData = soundData;

        soundPoint.SetActive(true);

    }

}
