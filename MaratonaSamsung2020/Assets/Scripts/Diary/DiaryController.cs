using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiaryController", menuName = "Controllers/DiaryEntry")]
[System.Serializable]
public class DiaryController : ScriptableObject {
    public List<DiaryEntry> DiaryEntries;
    public UnlockedDiaryEntryEvent unlockedDiaryEntryEvent;

    public List<DiaryEntry> GetUnlockedEntries() {
        return DiaryEntries.FindAll(entry => !entry.IsLocked);
    }

    public DiaryEntry FindEntry(int entryIndex) {
        DiaryEntry chosenEntry = DiaryEntries[entryIndex];

        if(!chosenEntry) {
            return null;
        }

        return chosenEntry;
    }

    public int FindEntryIndex(string entryTitle) {
        int chosenEntryIndex = DiaryEntries.FindIndex(entry => 
            entry.Title.ToLower() == entryTitle.ToLower()
        );

        return chosenEntryIndex;
    }
    
    public void UnlockEntry(int entryIndex) {
        DiaryEntry entry = DiaryEntries[entryIndex];

        if (entry.IsLocked) {
            entry.IsLocked = false;
            unlockedDiaryEntryEvent.Raise();
        }
    }
}