using UnityEngine.Playables;

public class TextClipAsset : PlayableAsset {
    public string text;

    public override Playable CreatePlayable(PlayableGraph graph, UnityEngine.GameObject owner) {
        TextClipBehaviour textClipBehaviour = new TextClipBehaviour();
        textClipBehaviour.text = text;
        
        return ScriptPlayable<TextClipBehaviour>.Create(graph, textClipBehaviour);
    }
}
