using UnityEngine;

[CreateAssetMenu(fileName = "DiaryEntry", menuName = "Diary/DiaryEntry")]
[System.Serializable]

public class DiaryEntry : ScriptableObject {
    public bool IsLocked;

    public Sprite DemonstrationImage;

    public string Title;

    [TextArea(4, 20)]
    public string Description;
}
