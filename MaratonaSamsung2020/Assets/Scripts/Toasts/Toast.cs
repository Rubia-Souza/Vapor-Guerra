using UnityEngine;

[CreateAssetMenu(fileName = "Toast", menuName = "Toasts/Toast")]
public class Toast : ScriptableObject {
    public string Title;
    
    [TextArea(4, 20)]
    public string Description;

    [Range(0, 10)]
    public float TimeInScreen;
}