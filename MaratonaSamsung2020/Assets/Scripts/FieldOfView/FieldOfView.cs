using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    private LayerMask playerMask;
    private LayerMask enviromentMask;
    
    private Transform detectedObject;
    private bool enterInView, stayInView, exitView;

    private Coroutine checkForTargetsCoroutine;

    [Range(0, 360)] public float Angle = 90f;
    public float Radius = 3f;

    public delegate void OnFieldOfViewDetection(Transform objectDetected);
    public event OnFieldOfViewDetection OnEnterInView = delegate {};
    public event OnFieldOfViewDetection OnStayInView = delegate {};
    public event OnFieldOfViewDetection OnExitView = delegate {};

    public void Start() {

        enterInView = stayInView = exitView = false;

        playerMask = ProjectLayers.PlayerLayerMask;
        enviromentMask = ProjectLayers.EnviromentLayerMask;

        StartCheckingForPlayerInView();

    }

    private IEnumerator CheckForPlayer() {

        while (true) {

            yield return new WaitForSeconds(0.5f);

            FindTargetInRadius();

            if (detectedObject != null) {

                if (enterInView) {

                    RaiseEnterInViewEvent(detectedObject);

                }

                if (stayInView) {

                    RaiseStayInViewEvent(detectedObject);

                }

                if (exitView) {

                    RaiseExitViewEvent(detectedObject);

                }

            }

        }

    }

    private void FindTargetInRadius() {
        
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, Radius, playerMask);

        if (playerCollider != null) {

            Transform playerTransform = playerCollider.transform;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            if (IsInViewAngle(directionToPlayer)) {

                float distanceToTarget = Vector2.Distance(transform.position, playerTransform.position);
                RaycastHit2D obstacules = Physics2D.Raycast(transform.position, directionToPlayer, distanceToTarget, enviromentMask);

                if (!obstacules && playerTransform.CompareTag("Player")) {

                    if (!enterInView && !stayInView) {

                        enterInView = true;

                    }
                    
                    stayInView = true;
                    detectedObject = playerTransform;

                    return;

                }

            }

        }

        if (stayInView) {

            exitView = true;
            stayInView = false;
            enterInView = false;

        }

    }

    public bool IsInViewAngle(Vector2 targetPosition) {

        return Vector2.Angle(transform.forward, targetPosition) < (Angle / 2); 

    }

    private void RaiseEnterInViewEvent(Transform detectedObject) {

        OnEnterInView(detectedObject);
        enterInView = false;

    }

    private void RaiseStayInViewEvent(Transform detectedObject) {

        OnStayInView(detectedObject);

    }

    private void RaiseExitViewEvent(Transform detectedObject) {

        OnExitView(detectedObject);
        exitView = false;
        detectedObject = null;

    }

    public void StopCheckingForPlayerInView() {

        if (checkForTargetsCoroutine != null) {

            StopCoroutine(checkForTargetsCoroutine);
            checkForTargetsCoroutine = null;

        }

    }

    public void StartCheckingForPlayerInView() {

        if (checkForTargetsCoroutine == null) {

            checkForTargetsCoroutine = StartCoroutine(CheckForPlayer());

        }

    }

}
