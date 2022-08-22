using UnityEngine;
using UnityEngine.Playables;
using TMPro;
public class TextClipMixer : PlayableBehaviour {
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        TextMeshProUGUI displayedText = playerData as TextMeshProUGUI;
        string currentText = "";
        float currentAlpha = 0f;

        if(!displayedText) {
            return;
        }

        int clipCount = playable.GetInputCount();

        for(int i = 0; i < clipCount; i++) {
            float inputWeight = playable.GetInputWeight(i);

            if(inputWeight > 0f) {
                ScriptPlayable<TextClipBehaviour> clipPlayable = (ScriptPlayable<TextClipBehaviour>) playable.GetInput(i);
                TextClipBehaviour behaviour = clipPlayable.GetBehaviour();

                currentText = behaviour.text;
                currentAlpha = inputWeight;
            }
        }


        displayedText.text = currentText;
        displayedText.color = new Color(1, 1, 1, currentAlpha);
    }
}
