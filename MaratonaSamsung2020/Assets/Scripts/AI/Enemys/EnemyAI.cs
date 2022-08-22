using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour {

    protected FieldOfView fieldOfView;
    protected SoundDetector soundDetector;
    protected NPCMovement movement;

    protected Patrol patrolPattern;

    protected Transform detectedTarget;

    protected Coroutine executingAction;

    public virtual float WaitTimeInPatrolPosition { get; }

    protected void Awake() {

        movement = GetComponent<NPCMovement>();
        patrolPattern = GetComponent<Patrol>();

        fieldOfView = GetComponentInChildren<FieldOfView>();
        soundDetector = GetComponentInChildren<SoundDetector>();

        RegisterEventsCallback();

    }

    private void RegisterEventsCallback() {

        fieldOfView.OnEnterInView += HandleEnterInView;
        fieldOfView.OnStayInView += HandleStayInView;
        fieldOfView.OnExitView += HandleExitView;

        soundDetector.AIHandleSoundDetection += HandleSoundDetection;

    }

    protected void OnDestroy() {

        UnregisterEventsCallback();

    }

    private void UnregisterEventsCallback() {

        fieldOfView.OnEnterInView -= HandleEnterInView;
        fieldOfView.OnStayInView -= HandleStayInView;
        fieldOfView.OnExitView -= HandleExitView;

        soundDetector.AIHandleSoundDetection -= HandleSoundDetection;

    }

    protected void ExecuteAction(IEnumerator action) {

        if (executingAction != null) {

            StopExecutingAction();

        }

        executingAction = StartCoroutine(action);

    }

    protected void StopExecutingAction() {

        if (executingAction != null) {

            StopCoroutine(executingAction);
            executingAction = null;

        }

    }

    protected virtual IEnumerator Patrolling() {

        while (detectedTarget == null) {

            Transform patrolPoint = patrolPattern.GetNextPatrolPoint();
            Vector3 horizontalPosition = new Vector3(patrolPoint.position.x, transform.position.y, patrolPoint.position.z);

            movement.MoveTo(horizontalPosition);

            yield return new WaitUntil(movement.HasReachDestination);

            yield return new WaitForSeconds(WaitTimeInPatrolPosition);

        }

        StopExecutingAction();

    }

    protected virtual void HandleEnterInView(Transform playerTransform) { }
    protected virtual void HandleStayInView(Transform playerTransform) { }
    protected virtual void HandleExitView(Transform playerTransform) { }

    protected virtual void HandleSoundDetection(Transform soundTransform) { }

}
