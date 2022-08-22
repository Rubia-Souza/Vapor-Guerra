using UnityEngine;

public class TestPatrol : EnemyAI {
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

    private void Start() {

        ExecuteAction(Patrolling());
        
    }

}
