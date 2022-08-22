using System.Collections;
using UnityEngine;

public class CaptureListener : MonoBehaviour {

    private PlayerMovement movementController;
    private CharacterController2D actionsController;
    private PlayerInteractionController interactionController;
    private IHealthController playerHealthControler;

    private Coroutine recieveDamageCoroutine;

    [SerializeField] private float scapeTime = 3f;

    public PlayerScapeCaptureEvent playerScapedAICaptureEvent;
    public PlayerCaptureEvent playerCaptureByAIEvent;

    private void Awake() {
        
        movementController = GetComponent<PlayerMovement>();
        actionsController = GetComponent<CharacterController2D>();
        playerHealthControler = GetComponent<IHealthController>();
        interactionController = GetComponent<PlayerInteractionController>();

    }

    private void Start() {
        
        playerCaptureByAIEvent.RegisterCallback(HandleCapture);
        playerScapedAICaptureEvent.RegisterCallback(HandleScape);

    }

    private void HandleCapture() {

        DesiblePlayerControl();

        StartCountDownToRecieveDamage();

    }

    private void DesiblePlayerControl() {

        movementController.enabled = false;
        actionsController.enabled = false;
        interactionController.enabled = false;

    }

    private void HandleScape() {

        StopCountDownToRecieveDamage();

        EnablePlayerControl();

    }

    private void EnablePlayerControl() {

        movementController.enabled = true;
        actionsController.enabled = true;
        interactionController.enabled = true;

    }

    private void StartCountDownToRecieveDamage() {

        StopCountDownToRecieveDamage();
        recieveDamageCoroutine = StartCoroutine(CountDownToRecieveDamage());

    }

    private IEnumerator CountDownToRecieveDamage() {

        yield return new WaitForSeconds(scapeTime);

        playerHealthControler.RecieveDamage(1f);

        playerScapedAICaptureEvent.Raise();

    }

    private void StopCountDownToRecieveDamage() {

        if (recieveDamageCoroutine != null) {

            StopCoroutine(recieveDamageCoroutine);
            recieveDamageCoroutine = null;

        }

    }
    
}
