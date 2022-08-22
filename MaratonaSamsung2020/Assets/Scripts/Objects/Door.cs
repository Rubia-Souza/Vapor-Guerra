using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour {

    private const float StopingDistance = 0.5f;

    private Coroutine openingCoroutine;
    private Coroutine closingCoroutine;

    [SerializeField] private float MovementSpeed;

    [SerializeField] private Vector2 OpenedPosition;
    [SerializeField] private Vector2 ClosedPosition;

    public bool IsOpen = false;

    public void Open() {

        StopOpening();
        StopClosing();
        openingCoroutine = StartCoroutine(MoveTo(OpenedPosition));
        IsOpen = true;

    }

    private void StopOpening() {

        if (openingCoroutine != null) {

            StopCoroutine(openingCoroutine);
            openingCoroutine = null;

        }

    }

    public void Close() {

        StopOpening();
        StopClosing();
        closingCoroutine = StartCoroutine(MoveTo(ClosedPosition));
        IsOpen = false;

    }

    private void StopClosing() {

        if (closingCoroutine != null) {

            StopCoroutine(closingCoroutine);
            closingCoroutine = null;

        }

    }

    private IEnumerator MoveTo(Vector2 position) {

        Vector2 actualPosition = transform.position;

        while (Vector2.Distance(actualPosition, position) > StopingDistance) {

            actualPosition = transform.position;
            Vector2 targetPosition = Vector2.Lerp(actualPosition, position, MovementSpeed * Time.fixedDeltaTime);

            transform.position = targetPosition;

            yield return new WaitForFixedUpdate();

        }

    }

}
