using UnityEngine;

public static class SceneTracker
{
    public static string[] sceneOrder;
    public static int currentSceneIndex = 0;  

    private static string[] baseScenes = { "Scene1", "Scene2", "Scene3", "Scene4", "Scene5", "Scene6", "Scene7", "Scene8", "Scene9", "Scene0" };
    private const int repetitions = 7;

    static SceneTracker()
    {
        var sceneList = new System.Collections.Generic.List<string> { "PracticeScene", "BufferScene" };
        for (int i = 0; i < repetitions; i++)
        {
            foreach (string scene in baseScenes)
            {
                sceneList.Add(scene);  
                sceneList.Add("RestScene"); 
            }
        }
        sceneList.Add("EndScene"); 
        sceneOrder = sceneList.ToArray();
    }
}
