using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    private static float speed = 1.5f;
    private static float width = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newZ = startPos.z + Mathf.Sin(Time.time * speed) * width;
        transform.position = new Vector3(startPos.x, startPos.y, newZ);
    }
}
