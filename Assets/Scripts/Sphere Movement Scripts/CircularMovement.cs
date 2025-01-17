using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    private static float radius = 1.5f;
    private static float speed = 3.5f;
    private Vector3 centerPos;

    void Start()
    {
        centerPos = transform.position;
    }

    void Update()
    {
        float angle = Time.time * speed;
        float newX = centerPos.x + Mathf.Cos(angle) * radius;
        float newZ = centerPos.z + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(newX, centerPos.y, newZ);
    }
}
