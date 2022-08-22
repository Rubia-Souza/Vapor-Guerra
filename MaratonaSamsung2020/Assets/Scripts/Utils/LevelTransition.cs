using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

public class LevelTransition : MonoBehaviour {

    public Slider progressBarSlider;
    public TextMeshProUGUI progressBarPercentageText;
    public GameOverEvent gameOverEvent;

    public Coroutine loadPhase;

    private void Awake() {

        gameOverEvent.RegisterCallback(StopLoading);
        
    }

    public void LoadScene(int sceneBuildIndex) {
        loadPhase = StartCoroutine(LoadSceneAsync(sceneBuildIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneBuildIndex) {
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneBuildIndex);

        sceneLoader.allowSceneActivation = false;

        while (!sceneLoader.isDone) {
            if(progressBarSlider) {
                int currentLoadingProgress = Convert.ToInt16(sceneLoader.progress * 100);

                progressBarPercentageText.text = currentLoadingProgress + "%";
                progressBarSlider.value = currentLoadingProgress;
            }

            if (sceneLoader.progress == 0.9f) {
                sceneLoader.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void StopLoading() {

        if (loadPhase != null) {

            StopCoroutine(loadPhase);
            loadPhase = null;

        }

    }
}