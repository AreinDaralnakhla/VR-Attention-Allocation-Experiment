using UnityEngine;

public class RotateMovement : MonoBehaviour
{
    private static float rotationSpeed = 60f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
