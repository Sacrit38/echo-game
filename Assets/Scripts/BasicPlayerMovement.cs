using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Echolocation Settings")]
    public float stepInterval = 0.5f; // Jeda antar langkah kaki
    private float stepTimer;
    
    // public EcholocationSystem echoSystem; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 0)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                TriggerFootstepEcho();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void TriggerFootstepEcho()
    {
        Debug.Log("Langkah kaki terdeteksi: Memicu Gelombang Ekolokasi!");
        // memanggil fungsi untuk memunculkan visual gelombang (Putih/Normal)
        // echoSystem.CreateRipple(transform.position, Color.white);
    }
}