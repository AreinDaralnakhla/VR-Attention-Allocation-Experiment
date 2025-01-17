using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneTracker.currentSceneIndex++;

        if (SceneTracker.currentSceneIndex < SceneTracker.sceneOrder.Length)
        {
            string nextScene = SceneTracker.sceneOrder[SceneTracker.currentSceneIndex];
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("No more scenes to load.");
        }
    }
}
