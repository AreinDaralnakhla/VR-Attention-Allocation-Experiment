using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    private static float speed =6f;     // Speed of the movement
    private static float height = 3f;    // Maximum vertical displacement
    private Vector3 startPos;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;          // Disable gravity
        rb.isKinematic = false;        // Allow physics interactions
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;  // Prevent tunneling
    }

    void FixedUpdate()
    {
        // Calculate the vertical position based on a sine wave
        float newY = startPos.y +(1+ Mathf.Sin(Time.time * speed))/2 * height;

        // Get the current velocity and adjust only the Y-axis
        Vector3 velocity = rb.velocity;
        velocity.y = (newY - rb.position.y) / Time.fixedDeltaTime;

        // Apply the velocity
        rb.velocity = velocity;
    }
}
