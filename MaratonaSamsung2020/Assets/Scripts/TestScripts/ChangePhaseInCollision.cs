using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePhaseInCollision : MonoBehaviour {

    private bool isLoading = false;

    public LevelTransition levelTransition;
    public int nextSceneIndex;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings && !isLoading) {

            isLoading = true;
            levelTransition.LoadScene(nextSceneIndex);

        }

    }

}
