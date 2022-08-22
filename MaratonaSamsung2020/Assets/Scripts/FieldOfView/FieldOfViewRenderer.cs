using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewRenderer : MonoBehaviour {

    public float meshResolution = 1f;
    public int edgeVerificationMaxCheck = 4;
    public float edgeDistanceDeadZone = 1f;

    private FieldOfView fieldOfView;
    private MeshFilter meshFilter;
    private Mesh viewMesh;

    private LayerMask enviromentMask;

    private struct RaycastHitInfo {

        public bool hasHitted;
        public Vector3 hitPoint;
        public float distance;
        public float shootingAngle;

    }

    private struct EdgeHitInfo {

        public Vector3 insidePoint;
        public Vector3 outsidePoint;

    }

    private void Start() {

        enviromentMask = ProjectLayers.EnviromentLayerMask;

        fieldOfView = GetComponent<FieldOfView>();
        meshFilter = GetComponent<MeshFilter>();

        viewMesh = new Mesh();
        viewMesh.name = "Field Of View Mesh";

        meshFilter.mesh = viewMesh;

    }

    private void LateUpdate() {

        DrawFieldOfView();

    }

    private void DrawFieldOfView() {

        int stepCount = Mathf.RoundToInt(fieldOfView.Angle * meshResolution);
        float stepAngle = fieldOfView.Angle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        RaycastHitInfo lastRaycastHit = new RaycastHitInfo();

        for (int i = 0; i <= stepCount; i++) {

            float currentAngle = transform.eulerAngles.y - fieldOfView.Angle / 2 + stepAngle * i;
            RaycastHitInfo raycastHit = ShootRaycast(currentAngle);

            if (i > 0) {

                if (lastRaycastHit.hasHitted != raycastHit.hasHitted || HasHittedTwoAbstacules(lastRaycastHit, raycastHit)) {

                    EdgeHitInfo edgeHitInfo = FindObstaculeEdgeBinarySearch(lastRaycastHit, raycastHit);

                    if (edgeHitInfo.insidePoint != Vector3.zero) {

                        viewPoints.Add(edgeHitInfo.insidePoint);

                    }

                    if (edgeHitInfo.outsidePoint != Vector3.zero) {

                        viewPoints.Add(edgeHitInfo.outsidePoint);

                    }

                }

            }

            viewPoints.Add(raycastHit.hitPoint);
            lastRaycastHit = raycastHit;

        }

        SetupViewMesh(viewPoints);

    }

    private RaycastHitInfo ShootRaycast(float shootingAngle) {

        Vector3 shootingDirection = MathExtension.GetDirectionFromGlobalAngle(shootingAngle);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootingDirection, fieldOfView.Radius, enviromentMask);
        
        if (hit) {

            return new RaycastHitInfo {
                hasHitted = true,
                hitPoint = hit.point,
                distance = hit.distance,
                shootingAngle = shootingAngle
            };

        } 
        else {

            return new RaycastHitInfo {
                hasHitted = false,
                hitPoint = transform.position + shootingDirection * fieldOfView.Radius,
                distance = fieldOfView.Radius,
                shootingAngle = shootingAngle
            };

        }

    }

    private bool HasHittedTwoAbstacules(RaycastHitInfo firstRaycast, RaycastHitInfo secondRaycast) {

        return (firstRaycast.hasHitted && secondRaycast.hasHitted && HasPassedTheEdgeDistanceDeadZone(firstRaycast, secondRaycast));

    }

    private bool HasPassedTheEdgeDistanceDeadZone(RaycastHitInfo firstRaycast, RaycastHitInfo secondRaycast) {

        return Mathf.Abs(firstRaycast.distance - secondRaycast.distance) > edgeDistanceDeadZone;

    }

    private EdgeHitInfo FindObstaculeEdgeBinarySearch(RaycastHitInfo insideRaycastHit, RaycastHitInfo outsideRaycastHit) {

        float insideAngle = insideRaycastHit.shootingAngle;
        float outsideAngle = outsideRaycastHit.shootingAngle;

        Vector3 insideHitPoint = Vector3.zero;
        Vector3 outsideHitPoint = Vector3.zero;

        for (int i = 0; i < edgeVerificationMaxCheck; i++) {

            float shootingAngle = (insideAngle + outsideAngle) / 2;
            RaycastHitInfo findEdgeRaycast = ShootRaycast(shootingAngle);

            if (findEdgeRaycast.hasHitted == insideRaycastHit.hasHitted && !HasPassedTheEdgeDistanceDeadZone(insideRaycastHit, findEdgeRaycast)) {

                insideAngle = shootingAngle;
                insideHitPoint = findEdgeRaycast.hitPoint;

            }
            else {

                outsideAngle = shootingAngle;
                outsideHitPoint = findEdgeRaycast.hitPoint;

            }

        }

        return new EdgeHitInfo {

            insidePoint = insideHitPoint,
            outsidePoint = outsideHitPoint

        };

    }

    private void SetupViewMesh(List<Vector3> viewPoints) {

        int verticesCount = viewPoints.Count + 1;

        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[(verticesCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < verticesCount - 1; i++) {

            Vector3 localViewPoint = transform.InverseTransformPoint(viewPoints[i]);
            vertices[i + 1] = localViewPoint;

            if (i < verticesCount - 2) {

                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;

            }

        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }

}
