using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DialogControlClip : PlayableAsset {
    public DialogControlBehaviour template = new DialogControlBehaviour();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
       return ScriptPlayable<DialogControlBehaviour>.Create(graph, template);
    }
}
