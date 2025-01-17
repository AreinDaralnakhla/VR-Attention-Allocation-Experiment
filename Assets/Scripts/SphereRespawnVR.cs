using UnityEngine;

public class SphereRespawnVR : MonoBehaviour
{
    public Collider respawnZone;  
    private float timer;
    private bool isTimerRunning;

    void Start()
    {
        RespawnSphere();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
        }
    }

    public void OnSphereClicked()
    {
        Debug.Log("Sphere clicked! Time elapsed: " + timer + " seconds.");
        Invoke(nameof(RespawnSphere), 1f);  // Delay respawning by 1 second
    }

    void RespawnSphere()
    {
        timer = 0f;
        isTimerRunning = true;

        // Randomize position within zone 
        if (respawnZone != null)
        {
            Bounds bounds = respawnZone.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            float z = Random.Range(bounds.min.z, bounds.max.z);

            // Apply new position with offset to avoid clipping
            transform.position = new Vector3(x, y, z) + new Vector3(0, 1f, 0);
        }
    }
}
