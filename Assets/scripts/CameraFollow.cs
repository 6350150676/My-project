using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target the camera should follow
    public Transform target;

    // The initial offset distance from the target
    private Vector3 offset;

    // Optional smoothness of the camera movement
    public float smoothSpeed = 0.125f;

    void Start()
    {
        // Calculate the initial offset
        if (target != null)
        {
            offset = transform.position - target.position;
        }
        else
        {
            Debug.LogError("Target not set for CameraFollow script.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired position based on the initial offset
            Vector3 desiredPosition = target.position + offset;

            // Smooth the camera movement
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the new position to the camera
            transform.position = smoothedPosition;
        }
    }
}
