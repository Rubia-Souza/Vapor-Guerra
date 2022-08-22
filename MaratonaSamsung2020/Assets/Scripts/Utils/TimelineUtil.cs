using UnityEngine;
using UnityEngine.Playables;

public class TimelineUtil : MonoBehaviour {
    public static void PauseTimeline(PlayableDirector director) {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public static void ResumeTimeline(PlayableDirector director) {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
