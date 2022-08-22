using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Animator animator;

    public CharacterController2D characterController;

    public float walkSpeed;

    public float runSpeedMultiplier;
    
    public bool isJumping {get; set;}
    
    public bool isCrouching {get; set;}

    public bool isRunning {get; set;}

    private float horizontalMoveDirection;

    private void Awake() {

        animator = GetComponent<Animator>();

    }

    public void Start() {
        horizontalMoveDirection = 0;
        isJumping = false;
        isRunning = false;
    }

    public void MoveCharacter(int movementDirection) {
        horizontalMoveDirection = movementDirection * walkSpeed;
    }

    void FixedUpdate() {
        horizontalMoveDirection *= isRunning ? runSpeedMultiplier : 1;

        isRunning = false;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMoveDirection));
        animator.SetBool("isFalling", !characterController.IsGrounded);

        characterController.Move(horizontalMoveDirection * Time.fixedDeltaTime, isCrouching, isJumping);
        
        isJumping = false;

    }
}