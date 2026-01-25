using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; 
    
    [Header("Movement Settings")]
    [Range(0, 1)]
    public float smoothSpeed = 0.05f; 
    public Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        if (target == null) return;

    
        Vector3 desiredPosition = target.position + offset;

        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
       
        transform.position = smoothedPosition;
    }
}