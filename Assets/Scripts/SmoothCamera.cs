using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; 
    
    [Header("Movement Settings")]
    [Range(0, 1)]
    public float smoothSpeed = 0.05f; 
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Clamp Settings")]
    public bool clamped = false;
    public Vector2 start, end; 

    private void LateUpdate()
    {
        if (target == null) return;

    
        Vector3 desiredPosition = target.position + offset;

        if (clamped)
        {
            desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, start.x, end.x), Mathf.Clamp(desiredPosition.y, start.y, end.y), desiredPosition.z);
        }

        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
       
        transform.position = smoothedPosition;
    }
}