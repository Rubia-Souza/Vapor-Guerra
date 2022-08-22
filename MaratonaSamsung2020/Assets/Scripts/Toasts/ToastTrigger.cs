using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastTrigger : MonoBehaviour {
    public Transform playerTransform;
    public ToastNotification toastNotification; 
    public Toast toast;

    private int triggerDistance = 5;
    private bool wasToastCalled = false;

    void Update() {
        float distance = Vector2.Distance(playerTransform.position, transform.position);

        if(distance <= triggerDistance && !wasToastCalled) {
            toastNotification.Open(toast);  
            wasToastCalled = true;
        }
    }
}