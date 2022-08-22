using System.Collections;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private Coroutine moveToCoroutine;

    private Vector2 destinyPosition = Vector2.zero;

    private Vector2 ActualPosition {

        get => transform.position;
        set => transform.position = value;

    }

    [SerializeField] private float stopingDistance = 1f;
    private float DistanceAproximation { get => stopingDistance; }

    [SerializeField] private float speed = 50f;
    public float Speed { get => speed; }

    private bool isFacingLeft = false;
    public bool IsFacingLeft {

        get => isFacingLeft;
        private set => isFacingLeft = value;

    }

    private void Awake() {

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    public void MoveTo(Vector2 position) {

        StopMovement();

        destinyPosition = position;
        moveToCoroutine = StartCoroutine(Move(destinyPosition));

    }

    private IEnumerator Move(Vector2 destinyPosition) {

        while (!HasReachDestination()) {

            ChangeFacingDirection(destinyPosition);

            if (animator != null) {

                animator.SetBool("isMoving", true);

            }

            float step = Speed * Time.fixedDeltaTime / 100;
            Vector2 nextPosition = Vector2.Lerp(ActualPosition, destinyPosition, step);

            rigidbody2d.MovePosition(nextPosition);
            yield return new WaitForFixedUpdate();

        }

        if (animator != null) {

            animator.SetBool("isMoving", false);

        }

    }

    private void ChangeFacingDirection(Vector2 targetFacingPosition) {

        if (!IsFacingLeft && ActualPosition.x > targetFacingPosition.x) {

            LookAtLeft();

        } 
        else if (IsFacingLeft && ActualPosition.x < targetFacingPosition.x) {

            LookAtRight();

        }

    }

    public void LookAtRight() {

        transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        FlipChildObjects();

        isFacingLeft = false;

    }

    public void LookAtLeft() {

        transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        FlipChildObjects();

        isFacingLeft = true;

    }

    private void FlipChildObjects() {

        foreach (Transform child in transform) {

            Vector3 childRotation = child.rotation.eulerAngles;

            float newYPosition = Mathf.CeilToInt(180 - childRotation.y);
            float roundedNewYPosition = (newYPosition + 4) / 5 * 5;
            Vector3 flipedRotation = new Vector3(childRotation.x, roundedNewYPosition, childRotation.z);

            child.eulerAngles = flipedRotation;

        }

    }

    public bool HasReachDestination() {

        return (Vector2.Distance(ActualPosition, destinyPosition) < DistanceAproximation);

    }

    public void StopMovement() {

        if (moveToCoroutine != null) {

            StopCoroutine(moveToCoroutine);

            destinyPosition = Vector2.zero;
            moveToCoroutine = null;

            if (animator != null) {

                animator.SetBool("isMoving", false);

            }

        }

    }

}
