using UnityEngine;

public class BackForthMovement : MonoBehaviour
{
    private static float speed = 1f;    // Shared speed for all BackForthMovement objects
    private static float distance = 1f; // Shared distance for all BackForthMovement objects
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        // Set the desired speed and distance here
        SetMovementParameters(1.5f, 2f); // Example: Speed = 3f, Distance = 5f
    }

    void Update()
    {
        float newX = startPos.x + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }

    // Function to control speed and distance programmatically
    public void SetMovementParameters(float newSpeed, float newDistance)
    {
        speed = newSpeed;
        distance = newDistance;
    }
}
