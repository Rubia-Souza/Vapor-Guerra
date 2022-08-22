using System.Collections;
using UnityEngine;

public class TesteSoundPointReactionAI : EnemyAI {

    private Coroutine checkSoundHeardCoroutine;

    public float CheckSoundWaitTime = 1f;

    protected override void HandleSoundDetection(Transform soundTransform) {

        Vector2 targetPositionToCheck = soundTransform.localPosition;

        checkSoundHeardCoroutine = StartCoroutine(CheckSoundHeard(targetPositionToCheck));

    }

    private IEnumerator CheckSoundHeard(Vector2 soundPosition) {

        movement.MoveTo(soundPosition);

        while (!movement.HasReachDestination()) {

            yield return new WaitForFixedUpdate();

        }

        Debug.Log("What happend here?");
        yield return new WaitForSeconds(CheckSoundWaitTime);
        Debug.Log("It was nothing");

        checkSoundHeardCoroutine = null;

    }

    protected override void HandleEnterInView(Transform playerTransform) {
        throw new System.NotImplementedException();
    }

    protected override void HandleExitView(Transform playerTransform) {
        throw new System.NotImplementedException();
    }

    protected override void HandleStayInView(Transform playerTransform) {
        throw new System.NotImplementedException();
    }

}
