using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundData))]
public class SoundDataEditorInfo : Editor {

    SoundData soundDataSO;

    public override void OnInspectorGUI() {

        soundDataSO = target as SoundData;

        CreateVelocityField();
        CreateSoundFileField();
        CreateMaxRadiusField();
        CreatePropagationTimeField();

        EditorUtility.SetDirty(target);

    }

    private void CreateMaxRadiusField() {

        soundDataSO.MaxRadius = EditorGUILayout.FloatField(nameof(soundDataSO.MaxRadius), soundDataSO.MaxRadius);

    }

    private void CreateVelocityField() {

        soundDataSO.Velocity = EditorGUILayout.FloatField(nameof(soundDataSO.Velocity), soundDataSO.Velocity);

    }

    private void CreateSoundFileField() {

        soundDataSO.SoundFile = (AudioClip) EditorGUILayout.ObjectField(nameof(soundDataSO.SoundFile), soundDataSO.SoundFile, typeof(AudioClip), false);

    }

    private void CreatePropagationTimeField() {

        soundDataSO.PropagateByFixedTime = GUILayout.Toggle(soundDataSO.PropagateByFixedTime, "PropagateByFixedTime");

        if (soundDataSO.PropagateByFixedTime) {

            soundDataSO.PropagationTime = EditorGUILayout.FloatField(nameof(soundDataSO.PropagationTime), soundDataSO.PropagationTime);

        }

    }

}
