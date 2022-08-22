using System.Collections;
using UnityEngine;

public class GuardAI : EnemyAI {

    private const string PlayerTag = ProjectTags.PlayerTag;

    private DetectionMeter detection;
    private CaptureTargetDetection captureAreaDetection;

    private Vector2 lastSeenPlayerPosition = Vector2.zero;

    [SerializeField] private float maxDistanceToCheckTargetLastSeenPosition;
    [SerializeField] private float checkSoundWaitTime;
    [SerializeField] private float delayToReEnableTargetCapture;
    [SerializeField] private float waitTimeInPatrolPosition;

    public float MaxDistanceToCheckTargetLastSeenPosition { get => maxDistanceToCheckTargetLastSeenPosition; }

    public float CheckSoundWaitTime { get => checkSoundWaitTime; }
    public float DelayToReEnableTargetCapture { get => delayToReEnableTargetCapture; }

    public override float WaitTimeInPatrolPosition { get => waitTimeInPatrolPosition; }

    private GuardAIStates actualState = GuardAIStates.DEFAULT;
    public GuardAIStates ActualState {

        get => actualState;
        private set { 

            actualState = value;

            if (actualState == GuardAIStates.CHASING) {

                captureAreaDetection.EnableDetectionWithDelay(DelayToReEnableTargetCapture);

            }
            else {

                captureAreaDetection.DesableDetection();

            }

        }

    }

    protected new void Awake() {

        base.Awake();

        detection = GetComponent<DetectionMeter>();
        detection.OnDetectionChange += HandleDetectionLevelChanged;

        captureAreaDetection = GetComponentInChildren<CaptureTargetDetection>();

    }

    private void Start() {

        captureAreaDetection.DesableDetection();

    }

    private void Update() {

        if (actualState == GuardAIStates.DEFAULT) {

            ExecuteAction(Patrolling());
            ActualState = GuardAIStates.PATROLLING;

        }

    }

    private void OnDisable() {

        StopExecutingAction();

        actualState = GuardAIStates.DEFAULT;
        lastSeenPlayerPosition = Vector2.zero;
        detectedTarget = null;

    }

    private void HandleDetectionLevelChanged(DetectionLevels actualDetectionLevel) {

        if (detectedTarget != null && detectedTarget.CompareTag(PlayerTag)) {

            if (actualDetectionLevel == DetectionLevels.DETECTED) {

                ExecuteAction(ChaseDetectedTarget());
                ActualState = GuardAIStates.CHASING;

            }
            else if (actualDetectionLevel <= DetectionLevels.LOW && actualState == GuardAIStates.CHASING) {

                StopExecutingAction();

                lastSeenPlayerPosition = detectedTarget.position;
                detectedTarget = null;

                ActualState = GuardAIStates.SUSPECTING;

                ExecuteAction(CheckPlayerLastSeenPosition());

            }
            else if (actualDetectionLevel == DetectionLevels.UNSEEN && actualState == GuardAIStates.SUSPECTING) {

                detectedTarget = null;
                ActualState = GuardAIStates.DEFAULT;

            }

        }

    }

    protected override void HandleEnterInView(Transform playerTransform) {

        if (actualState != GuardAIStates.CHASING) {

            StopExecutingAction();

            ActualState = GuardAIStates.SUSPECTING;
            detectedTarget = playerTransform;

            Vector2 positionCloseToTarget = new Vector2(detectedTarget.position.x, transform.position.y);

            movement.MoveTo(positionCloseToTarget);

        }

        detection.StartIncreaseDetectionLevel();

    }

    protected override void HandleStayInView(Transform playerTransform) {

        lastSeenPlayerPosition = playerTransform.position;

    }

    protected override void HandleExitView(Transform playerTransform) {

        detection.StartDecreaseDetectionLevel();

    }

    protected override void HandleSoundDetection(Transform soundTransform) {

        if (detectedTarget == null || !detectedTarget.CompareTag(PlayerTag)) {

            StopExecutingAction();

            detectedTarget = soundTransform;
            ExecuteAction(CheckSoundPosition(detectedTarget.position));
            ActualState = GuardAIStates.SUSPECTING;

        }

    }

    private void OnCollisionEnter2D(Collision2D collisionInfo) {

        Transform collidedObject = collisionInfo.transform;

        if (collidedObject.CompareTag(PlayerTag)) {

            detectedTarget = collidedObject;
            detection.SkipToLevel(DetectionLevels.DETECTED);

        }

    }

    private IEnumerator CheckPlayerLastSeenPosition() {
        
        if (Vector2.Distance(lastSeenPlayerPosition, transform.position) < MaxDistanceToCheckTargetLastSeenPosition) {

            Vector2 lastSeenPlayerHorizontalPosition = new Vector2(lastSeenPlayerPosition.x, transform.position.y);

            movement.MoveTo(lastSeenPlayerHorizontalPosition);

            yield return new WaitUntil(movement.HasReachDestination);

            yield return new WaitForSeconds(WaitTimeInPatrolPosition);

        }

        lastSeenPlayerPosition = Vector2.zero;
        ActualState = GuardAIStates.DEFAULT;

        StopExecutingAction();

    }

    private IEnumerator ChaseDetectedTarget() {

        while (detectedTarget != null && detectedTarget.CompareTag(PlayerTag)) {

            Vector2 targetHorizontalPosition = new Vector2(detectedTarget.position.x, transform.position.y);

            movement.MoveTo(targetHorizontalPosition);

            yield return new WaitForFixedUpdate();

        }

        StopExecutingAction();

    }

    private IEnumerator CheckSoundPosition(Vector2 soundPosition) {

        Vector2 soundHorizontalPosition = new Vector2(soundPosition.x, transform.position.y);

        movement.MoveTo(soundHorizontalPosition);

        yield return new WaitUntil(movement.HasReachDestination);

        yield return new WaitForSeconds(CheckSoundWaitTime);

        detectedTarget = null;
        ActualState = GuardAIStates.DEFAULT;

        StopExecutingAction();

    }

}
