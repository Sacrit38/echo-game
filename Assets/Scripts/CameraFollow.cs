using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; // Sema
    public Vector3 offset; // Atur jarak kamera (biasanya z = -10)

    void LateUpdate() // LateUpdate bagus untuk kamera agar tidak jittery
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}