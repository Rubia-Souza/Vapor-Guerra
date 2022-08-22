using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTargetDetection : MonoBehaviour {

    private const string PlayerTag = ProjectTags.PlayerTag;

    private BoxCollider2D detectionArea;

    private Coroutine enableDetectionWithDelayCoroutine;
    private Coroutine desableDetectionWithDelayCoroutine;

    private bool hasCaptureTarget = false;
    public bool HasCaptureTarget { 

        get => hasCaptureTarget;
        private set => hasCaptureTarget = value; 

    }

    public PlayerScapeCaptureEvent playerScapedCaptureEvent;
    public PlayerCaptureEvent playerCaptureByAI;

    private void Awake() {

        detectionArea = GetComponent<BoxCollider2D>();
        
    }

    private void Start() {

        playerScapedCaptureEvent.RegisterCallback(ChangeCaptureTarget);

    }

    private void ChangeCaptureTarget() {

        if (HasCaptureTarget) {

            HasCaptureTarget = false;

            DesableDetection();
            EnableDetectionWithDelay(2f);

        }

    }

    private void OnTriggerStay2D(Collider2D collisionInfo) {

        Transform colidedObject = collisionInfo.transform;

        if (colidedObject.CompareTag(PlayerTag) && !HasCaptureTarget) {

            HasCaptureTarget = true;
            playerCaptureByAI.Raise();

        }

    }

    public void EnableDetection() {

        detectionArea.enabled = true;

    }

    public void EnableDetectionWithDelay(float delay) {

        enableDetectionWithDelayCoroutine = StartCoroutine(EnableDetectionDelayedBy(delay));

    }

    private IEnumerator EnableDetectionDelayedBy(float secondsToWait) {

        yield return new WaitForSeconds(secondsToWait);
        EnableDetection();

    }

    public void StopEnableDetectionWithDelay() {

        if (enableDetectionWithDelayCoroutine != null) {

            StopCoroutine(enableDetectionWithDelayCoroutine);
            enableDetectionWithDelayCoroutine = null;

        }

    }

    public void DesableDetection() {

        detectionArea.enabled = false;

    }

    public void DesableDetectionWithDelay(float delay) {

        desableDetectionWithDelayCoroutine = StartCoroutine(DesableDetectionDelayedBy(delay));

    }

    private IEnumerator DesableDetectionDelayedBy(float secondsToWait) {

        yield return new WaitForSeconds(secondsToWait);
        DesableDetection();

    }

    public void StopDesableDetectionWithDelay() {
        
        if (desableDetectionWithDelayCoroutine != null) {

            StopCoroutine(desableDetectionWithDelayCoroutine);
            desableDetectionWithDelayCoroutine = null;

        }

    }
    
}
