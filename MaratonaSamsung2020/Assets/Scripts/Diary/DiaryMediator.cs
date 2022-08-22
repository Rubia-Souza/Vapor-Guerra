using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMediator : MonoBehaviour {
    public DiaryController diaryController;

    public DiaryOpenedEvent diaryOpened;

    public Image diaryImage;
    public TextMeshProUGUI titleTextElement;
    public TextMeshProUGUI descriptionTextElement;
    public ScrollRect contentScroll;
    public GameObject entryListItemTemplate;

    private void OnEnable() {
        ShowEntry(0);
        CreateEntriesShowcase();
        diaryOpened.Raise();
    }

    public void ShowEntry(int entryIndex) {
        DiaryEntry chosenEntry = diaryController.FindEntry(entryIndex);

        diaryImage.sprite = chosenEntry.DemonstrationImage;
        titleTextElement.text = chosenEntry.Title;
        descriptionTextElement.text = chosenEntry.Description;

        contentScroll.verticalNormalizedPosition = 1;
    }

    public void CreateEntriesShowcase() {
        List<DiaryEntry> unlockedEntries = diaryController.GetUnlockedEntries();

        for(int i = 0; i < unlockedEntries.Count; i++) {
            DiaryEntry currentEntry = unlockedEntries[i];
            int currentEntryIndex = diaryController.FindEntryIndex(currentEntry.Title);

            Transform itemListParent = entryListItemTemplate.transform.parent;
            GameObject entryListItem = Instantiate(entryListItemTemplate) as GameObject;
            Button entryListButton = entryListItem.GetComponent<Button>();
            Image showcaseEntryImage = entryListItem.GetComponentInChildren<Image>();

            entryListItem.SetActive(true);
            entryListItem.transform.SetParent(itemListParent, false);

            entryListButton.onClick.AddListener(() => ShowEntry(currentEntryIndex));

            showcaseEntryImage.sprite = currentEntry.DemonstrationImage;

            Vector3 entryListItemPosition = entryListItem.transform.position;

            entryListItemPosition.y -= i * 90;
            entryListItem.transform.position = entryListItemPosition;
        }
    }
}