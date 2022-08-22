using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour {
    private Queue<Speech> sentences = new Queue<Speech>();
    private float offsetAnimationHeight;
    private IEnumerator previousTypeCoroutine;

    public GameEvent DialogueEndedEvent;

    public GameObject dialoguePanel;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI sentenceText;

    void Start() {
        offsetAnimationHeight = Screen.height * 0.1f + 20f;
    }

    public void StartDialogue(Dialogue dialogue) {
        sentences.Clear();

        LeanTween.moveY(dialoguePanel, offsetAnimationHeight, .25f).setEase(LeanTweenType.easeOutBack);

        foreach (Speech sentence in dialogue.speeches) {
            sentences.Enqueue(sentence);
        }

        DisplayNexttSentence();
    }

    public void DisplayNexttSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
        } else {
            Speech currentSpeech = sentences.Dequeue();

            UpdateDialoguePanel(currentSpeech);
        }
    }

    private void UpdateDialoguePanel(Speech speech) {
        characterNameText.text = speech.npcName;

        if(previousTypeCoroutine != null)
            StopCoroutine(previousTypeCoroutine);

        IEnumerator delayedSentenceTypeCoroutine = TypeSentence(speech.sentence);

        previousTypeCoroutine = delayedSentenceTypeCoroutine;
        StartCoroutine(delayedSentenceTypeCoroutine);
    }

    private IEnumerator TypeSentence(string sentence) {
        sentenceText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            sentenceText.text += letter.ToString();
            yield return null;
        }
    }

    public void EndDialogue() {
        LeanTween.moveY(dialoguePanel, offsetAnimationHeight * -2f, .25f).setEase(LeanTweenType.easeOutBack);
        DialogueEndedEvent.Raise();
    }
}
