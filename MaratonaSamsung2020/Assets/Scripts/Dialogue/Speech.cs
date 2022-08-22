using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Speech {
    public string npcName;
    
    [TextArea(3, 10)]
    public string sentence;
}