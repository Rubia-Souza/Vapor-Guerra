using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionMeter : MonoBehaviour {

    private const float DetectionDelay = 1f;

    private float ActualDetectionLevelPorcentage {

        get => (actualDetectionLevelValue / maxDetectionLevelValue);

    }

    private Coroutine increaseMeterCoroutine;
    private Coroutine decreaseMeterCoroutine;

    private float actualDetectionLevelValue;
    public float ActualDetectionLevelValue {

        get => actualDetectionLevelValue;
        private set {

            actualDetectionLevelValue = value;
            DetectionLevel = DetectionLevelUtils.GetDetectionLevel(ActualDetectionLevelPorcentage);

        }

    }

    [SerializeField] private DetectionLevelChangedUIEvent detectionLevelChangedEvent;

    [SerializeField] private float detectionSpeed;
    public float DetectionSpeed {

        get => detectionSpeed;
        set => detectionSpeed = value;

    }

    [SerializeField] private float maxDetectionLevelValue;
    public float MaxDetectionLevelValue {

        get => maxDetectionLevelValue;
        private set => maxDetectionLevelValue = value;

    }

    private DetectionLevels detectionLevel;
    public DetectionLevels DetectionLevel {

        get => detectionLevel;
        private set {

            if (detectionLevel == value) {

                return;

            }

            detectionLevel = value;
            detectionLevelChangedEvent.Raise(ActualDetectionLevelPorcentage);
            OnDetectionChange(DetectionLevel);

        }

    }

    public delegate void HandleDetectionChange(DetectionLevels newDetectionLevelValue);
    public event HandleDetectionChange OnDetectionChange = delegate { };

    public void StartIncreaseDetectionLevel() {

        if (actualDetectionLevelValue < 0) {

            actualDetectionLevelValue = 0;

        }

        StopDecreaseDetectionLevel();
        StopIncreaseDetectionLevel();
        increaseMeterCoroutine = StartCoroutine(IncreaseCoroutine());

    }

    private IEnumerator IncreaseCoroutine() {

        while (actualDetectionLevelValue < maxDetectionLevelValue) {

            ActualDetectionLevelValue += detectionSpeed;

            yield return new WaitForSeconds(DetectionDelay);

        }

    }

    public void StopIncreaseDetectionLevel() {

        if (increaseMeterCoroutine != null) {

            StopCoroutine(increaseMeterCoroutine);
            increaseMeterCoroutine = null;

        }

    }

    public void StartDecreaseDetectionLevel() {

        StopIncreaseDetectionLevel();
        StopDecreaseDetectionLevel();
        decreaseMeterCoroutine = StartCoroutine(DecreaseCoroutine());

    }

    private IEnumerator DecreaseCoroutine() {

        while (actualDetectionLevelValue > 0) {

            ActualDetectionLevelValue -= detectionSpeed;

            yield return new WaitForSeconds(DetectionDelay);

        }

    }

    public void StopDecreaseDetectionLevel() {

        if (decreaseMeterCoroutine != null) {

            StopCoroutine(decreaseMeterCoroutine);
            decreaseMeterCoroutine = null;

        }

    }

    public void SkipToLevel(DetectionLevels level) {

        float desiredLevePorcentage = DetectionLevelUtils.GetDetectionLevelPorcentage(level);

        ActualDetectionLevelValue = desiredLevePorcentage * maxDetectionLevelValue;

    }

}
