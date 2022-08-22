using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Sound/SoundData")]
public class SoundData : ScriptableObject {

    private Vector3 position = new Vector3();
    public Vector3 Position {

        get => position;
        set => position = value;

    }

    public float Velocity;
    public float MaxRadius;
    public AudioClip SoundFile;

    public bool PropagateByFixedTime;
    public float PropagationTime;

    private void OnValidate() {

        if (!PropagateByFixedTime) {

            PropagationTime = SoundFile.length;

        }

    }

}
