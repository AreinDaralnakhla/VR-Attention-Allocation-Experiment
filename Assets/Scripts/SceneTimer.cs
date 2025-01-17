using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTimer : MonoBehaviour
{
    private const float sceneDuration = 800f;
    private float timer;
    public TextMeshProUGUI timerText;

    void Start()
    {
        timer = sceneDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timer).ToString();
        }

        if (timer <= 0)
        {
            LoadRestOrEndScene();
        }
    }

    void LoadRestOrEndScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.StartsWith("Scene10") && SceneTracker.currentSceneIndex == SceneTracker.sceneOrder.Length - 2)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            SceneTracker.currentSceneIndex++;
            SceneManager.LoadScene("RestScene");
        }
    }
}
