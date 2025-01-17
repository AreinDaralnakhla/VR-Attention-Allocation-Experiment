using UnityEngine;

public class JitterMovement : MonoBehaviour
{
    private static float intensity =6f;  // Intensity controlled through script, not inspector
    private Vector3 startPos;  // Initial position to lock the y-axis
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;  // Save the initial position
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody exists and is set up correctly
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;  // Disable gravity for jittering
        rb.isKinematic = false;  // Enable physics interaction
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;  // Prevent tunneling
    }

    void FixedUpdate()
    {
        // Generate random offsets for jittering
        float offsetX = Random.Range(-intensity, intensity);
        float offsetZ = Random.Range(-intensity, intensity);

        // Apply random jitter in the x and z directions, keeping y fixed
        Vector3 jitterVelocity = new Vector3(offsetX, 0, offsetZ);
        rb.velocity = jitterVelocity;

        // Reset the position to lock the y-axis
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
    }
}
