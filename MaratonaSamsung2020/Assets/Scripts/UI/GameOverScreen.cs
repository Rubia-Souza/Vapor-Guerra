using UnityEngine;

public class GameOverScreen : MonoBehaviour {
    public GameOverEvent gameOverEvent;
    public GameObject gameOverPanel;

    private void Start() {
        gameOverEvent.RegisterCallback(OpenGameOverScreen);
        gameOverPanel.SetActive(false);
    }

    public void OpenGameOverScreen() {
        gameOverPanel.SetActive(true);
    }
}