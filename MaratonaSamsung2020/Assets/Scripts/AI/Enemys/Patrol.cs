using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Patrol : MonoBehaviour {

    private Queue<Transform> patrolPointsOrder = new Queue<Transform>();

    [SerializeField] private bool doInversePatrol = false;
    [SerializeField] private Transform[] patrolPoints;

    public Transform LastPatrolledPosition { get; private set; }

    public int CountPatrollingPoints {

        get => patrolPointsOrder.Count;

    }

    private void Awake() {

        FillPatrolPointsQueue(patrolPoints);

    }

    private void FillPatrolPointsQueue(Transform[] patrolPoints) {

        patrolPointsOrder.Clear();
        foreach (Transform patrolPoint in patrolPoints) {

            if (patrolPoint != null) {

                patrolPointsOrder.Enqueue(patrolPoint);

            }

        }

    }

    public Transform GetNextPatrolPoint() {

        Transform patrolPoint = LastPatrolledPosition;

        if (patrolPointsOrder.Count <= 0 && doInversePatrol) {

            ReFillPatrolPointsQueue();

        }
        else if (patrolPointsOrder.Count > 0) {

            patrolPoint = patrolPointsOrder.Dequeue();
            LastPatrolledPosition = patrolPoint;

        }

        return patrolPoint;

    }

    private void ReFillPatrolPointsQueue() {

        if (LastPatrolledPosition == patrolPoints[0]) {

            FillPatrolPointsQueue(patrolPoints.Skip(1).ToArray());

        } 
        else {

            FillPatrolPointsQueue(patrolPoints.Reverse().Skip(1).ToArray());

        }

    }

}
