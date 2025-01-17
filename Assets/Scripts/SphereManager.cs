using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class SphereManager : MonoBehaviour
{
    public Material greyMaterial;
    private Renderer sphereRenderer;
    private bool isClicked = false;
    private float timer = 0f;
    private Vector3 startPosition;
    private float dist;
    private float peripheralAngle; 
    private bool hasLogged = false;

    public Transform player;
    private static string filePath;

    private static readonly string[] movementScripts = { "JitterMovement", "BackForthMovement", "VerticalMovement", "CircularMovement", "StaticMovement", "RotateMovement", "HorizontalMovement" };

    private static List<SphereManager> allSpheres = new List<SphereManager>();

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        startPosition = transform.position;

        if (!allSpheres.Contains(this))
        {
            allSpheres.Add(this);
        }

        AssignMovement();  

        string sceneName = SceneManager.GetActiveScene().name;

        if (string.IsNullOrEmpty(filePath) || !filePath.Contains(sceneName))
        {
            filePath = CreateCSV(SceneTracker.currentSceneIndex.ToString());
        }

        LogAllSpheresAtStart();

        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSphereClicked);
        }
    }

    void Update()
    {
        if (!isClicked)
        {
            timer += Time.deltaTime;
        }

        if (player != null)
        {
            Vector3 directionToSphere = (startPosition - player.position).normalized;

            peripheralAngle = Vector3.Angle(player.forward, directionToSphere);

            Vector3 diff = (player.position - startPosition);

            dist = diff.magnitude;
        }
    }

    public void OnSphereClicked(SelectEnterEventArgs args)
    {
        if (!isClicked)
        {
            sphereRenderer.material = greyMaterial;
            isClicked = true;
            UpdateSphereData();
        }
    }

    void AssignMovement()
    {
        int adjustedIndex = SceneTracker.currentSceneIndex - 2; 
        if (adjustedIndex < 0) adjustedIndex = 0; 

        int currentRepetition = adjustedIndex / (10 * 2); 
        int movementIndex = (currentRepetition + allSpheres.IndexOf(this)) % movementScripts.Length;

        System.Type scriptType = System.Type.GetType(movementScripts[movementIndex]);
        if (scriptType != null && GetComponent(scriptType) == null)
        {
            gameObject.AddComponent(scriptType);
        }
    }


    void LogAllSpheresAtStart()
    {
        foreach (SphereManager sphere in allSpheres)
        {
            if (!sphere.hasLogged)
            {
                string data = sphere.gameObject.name + "," +
                              sphere.gameObject.tag + "," +
                              (sphere.isClicked ? sphere.timer.ToString("F2") : "10.00") + "," +  
                              sphere.peripheralAngle.ToString("F2") + "," +  
                              sphere.dist.ToString("F3") + "," +  
                              sphere.transform.position.x.ToString("F3") + "," +
                              sphere.transform.position.y.ToString("F3") + "," +
                              sphere.transform.position.z.ToString("F3") + "," +
                              sphere.player.position.x.ToString("F3") + "," +
                              sphere.player.position.y.ToString("F3") + "," +
                              sphere.player.position.z.ToString("F3") + "," +
                              (sphere.isClicked ? "Yes" : "No") + "\n"; 

                File.AppendAllText(filePath, data);

                sphere.hasLogged = true;
            }
        }
    }


    void UpdateSphereData()
    {
        string[] csvLines = File.ReadAllLines(filePath);

        for (int i = 0; i < csvLines.Length; i++)
        {
            if (csvLines[i].StartsWith(gameObject.name + ","))
            {
                csvLines[i] = gameObject.name + "," +
                              gameObject.tag + "," +
                              timer.ToString("F2") + "," +  
                              peripheralAngle.ToString("F2") + "," +
                              dist.ToString("F3") + "," +
                              transform.position.x.ToString("F3") + "," +
                              transform.position.y.ToString("F3") + "," +
                              transform.position.z.ToString("F3") + "," +
                              player.position.x.ToString("F3") + "," +
                              player.position.y.ToString("F3") + "," +
                              player.position.z.ToString("F3") + "," +
                              "Yes";
                break;
            }
        }

        File.WriteAllLines(filePath, csvLines);
        Debug.Log("Updated Click Data for Sphere: " + gameObject.name);
    }



    string CreateCSV(string sceneName)
    {
        string playerID = PlayerIDManager.GetPlayerID();
        string folderPath = Path.Combine(Application.dataPath, "Data", playerID, sceneName);
        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "SphereData.csv");
        if (!File.Exists(filePath))
        {
            string header = "SphereName,SphereType,ClickTime,PeripheralAngle,Distance,SpherePosX,SpherePosY,SpherePosZ,PlayerPosX,PlayerPosY,PlayerPosZ,Clicked\n";
            File.AppendAllText(filePath, header);
        }
        return filePath;
    }
}
