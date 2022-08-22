using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PauseController", menuName = "Controllers/PauseController")]
public class PauseController : ScriptableObject {
    public bool isGamePaused { get; private set; }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        ResumeGame();
    }

    void Start() {
        isGamePaused = false;
    }

    public void PauseGame() {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        isGamePaused = false;
    }
}