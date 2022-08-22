using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModalController", menuName = "Controllers/ModalController")]
public class ModalController : ScriptableObject {
    public void OpenModal(GameObject modal) {
        modal.SetActive(true);
    }

    public void CloseModal(GameObject modal) {
        modal.SetActive(false);
    }

    public void ToggleModal(GameObject modal) {
        bool currentModalState = modal.activeSelf;

        modal.SetActive(!currentModalState);
    }
}