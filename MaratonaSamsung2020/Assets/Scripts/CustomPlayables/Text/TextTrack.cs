using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

[TrackBindingType(typeof(TextMeshProUGUI))]
[TrackClipType(typeof(TextClipAsset))]
public class TextTrack : TrackAsset {
    public override Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, int inputCount) {
        return ScriptPlayable<TextClipMixer>.Create(graph, inputCount);
    }
}
