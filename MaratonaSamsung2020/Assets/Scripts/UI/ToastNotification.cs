using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastNotification : Singleton<ToastNotification> {
    public GameObject ToastTemplate;
    public ModalController modalController;

    private TextMeshProUGUI toastTile;
    private TextMeshProUGUI toastDescription;
    private float offsetWidthAnimation;

    void Start() {
        TextMeshProUGUI[] textComponents = ToastTemplate.GetComponentsInChildren<TextMeshProUGUI>();

        toastTile = textComponents[0];
        toastDescription = textComponents[1];
        offsetWidthAnimation = Screen.width * 0.9f - 80f;
    }

    public void Open(Toast toast) {
        toastTile.text = "";
        toastTile.text = toast.Title;

        toastDescription.text = ""; 
        toastDescription.text = toast.Description;

        StartCoroutine(HandleToastOpening(toast));
    }

    IEnumerator HandleToastOpening(Toast toast) {
        ShowToast();
        yield return new WaitForSeconds(toast.TimeInScreen);
        CloseToast();
    }

    void ShowToast() {
        modalController.OpenModal(ToastTemplate);
        LeanTween.moveX(ToastTemplate, offsetWidthAnimation, .25f).setEase(LeanTweenType.easeOutBack);
    }

    void CloseToast() {
        LeanTween.moveX(ToastTemplate, offsetWidthAnimation * 2f, .25f).setOnComplete(() => {
            modalController.CloseModal(ToastTemplate);
        });
    }
}