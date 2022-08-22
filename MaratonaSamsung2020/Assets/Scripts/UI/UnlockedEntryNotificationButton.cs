using UnityEngine;

public class UnlockedEntryNotificationButton : MonoBehaviour {

    public UnlockedDiaryEntryEvent unlockedDiaryEntryEvent;
    public DiaryOpenedEvent diaryOpenedEvent;

    public Animator imgButtonAnimator;
    public GameObject newEntryIcon;

    private void Start() {

        unlockedDiaryEntryEvent.RegisterCallback(TriggerButtonAnimation);
        unlockedDiaryEntryEvent.RegisterCallback(ShowNewEntryNotification);
        diaryOpenedEvent.RegisterCallback(HideNewEntryNotification);

    }

    private void TriggerButtonAnimation() {

        imgButtonAnimator.SetTrigger("EntryUnlocked");

    }

    public void ShowNewEntryNotification() {
        
        newEntryIcon.SetActive(true);

    }

    public void HideNewEntryNotification() {

        newEntryIcon.SetActive(false);

    }

}
