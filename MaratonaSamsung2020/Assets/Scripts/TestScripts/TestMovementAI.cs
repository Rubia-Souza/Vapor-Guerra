using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementAI : EnemyAI {

    public Transform position;

    private void Start() {

        movement.MoveTo(position.position);

    }

    protected override void HandleEnterInView(Transform playerTransform) {
        throw new System.NotImplementedException();
    }

    protected override void HandleExitView(Transform playerTransform) {
        throw new System.NotImplementedException();
    }

    protected override void HandleSoundDetection(Transform soundTransform) {
        throw new System.NotImplementedException();
    }

    protected override void HandleStayInView(Transform playerTransform) {
        throw new System.NotImplementedException();
    }

}
