using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObject/Dialogue")]
public class Dialogue : ScriptableObject {
    public Speech[] speeches;
}