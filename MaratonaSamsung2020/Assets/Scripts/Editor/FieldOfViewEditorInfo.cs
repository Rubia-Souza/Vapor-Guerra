using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditorInfo : Editor {

    private FieldOfView fieldOfView;

    private void OnSceneGUI() {

        fieldOfView = (FieldOfView) target;

        DrawFieldOfViewCircle();

        DrawFieldOfViewLines();

    }

    private void DrawFieldOfViewCircle() {

        Vector3 fieldOfViewPosition = fieldOfView.transform.position;
        Handles.DrawWireArc(fieldOfViewPosition, Vector3.forward, Vector2.right, 360, fieldOfView.Radius);

    }

    private void DrawFieldOfViewLines() {

        Vector3 fieldOfViewPosition = fieldOfView.transform.position;
        Vector2 fieldOfViewAngles = fieldOfView.transform.eulerAngles;

        Vector3 viewAngleDirectionLeft = MathExtension.GetDirectionFromAngle(-fieldOfView.Angle / 2, fieldOfViewAngles);
        Vector3 viewAngleDirectionRight = MathExtension.GetDirectionFromAngle(fieldOfView.Angle / 2, fieldOfViewAngles);

        Handles.DrawLine(fieldOfViewPosition, fieldOfViewPosition + viewAngleDirectionLeft * fieldOfView.Radius);
        Handles.DrawLine(fieldOfViewPosition, fieldOfViewPosition + viewAngleDirectionRight * fieldOfView.Radius);

    }

}
