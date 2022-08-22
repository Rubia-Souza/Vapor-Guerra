using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthIndicator : MonoBehaviour {
    public DetectionLevelChangedUIEvent detectionLevelChangedEvent;

    public Slider stealthDetectionMeasure;
    public Image eyeImage;

    void Start() {
        UpdateDetectionUI(0);
        detectionLevelChangedEvent.RegisterCallback(UpdateDetectionUI);
    }

    public void UpdateDetectionUI(float detectionPercentage) {
        stealthDetectionMeasure.value = detectionPercentage;
        eyeImage.color = new Color(255f, 255f, 255f, detectionPercentage);
    }
}
