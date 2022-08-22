using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFildOfViewAI : MonoBehaviour {

    public Transform playerDetected;
    public Transform playerUndetected;

    private FieldOfView fieldOfView;
    private NPCMovement movement;

    private const float WaitTime = 3f;

    private void Awake() {
        
        fieldOfView = GetComponentInChildren<FieldOfView>();
        movement = GetComponent<NPCMovement>();

        fieldOfView.OnEnterInView += HandleEnterInView;
        fieldOfView.OnStayInView += HandleStayInView;
        fieldOfView.OnExitView += HandleExitView;

    }

    private void HandleEnterInView(Transform playerTransform) {

        Debug.Log("Player is in the view");

    }

    private void HandleExitView(Transform playerTransform) {

        Debug.Log("Player exit the view");

    }

    private void HandleStayInView(Transform playerTransform) {

        Debug.Log("Seeing player");

    }

    private void Start() {

        StartCoroutine(TestCoroutine());

    }

    private IEnumerator TestCoroutine() {

        Vector2 position = new Vector2(playerDetected.position.x, transform.position.y);

        movement.MoveTo(position);

        yield return new WaitUntil(movement.HasReachDestination);

        yield return new WaitForSeconds(WaitTime);

        position = new Vector2(playerUndetected.position.x, transform.position.y);

        movement.MoveTo(position);

        yield return new WaitUntil(movement.HasReachDestination);

        yield return new WaitForSeconds(WaitTime);

    }

}
