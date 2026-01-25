using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Echolocation Settings")]
    public float stepInterval = 0.5f; // Jeda antar langkah kaki saat JALAN
    private float stepTimer;
    
    [Header("Standby Settings")]
    public float standbyInterval = 2.0f; // Jeda antar ripple saat DIAM
    private float standbyTimer;

    public GameObject ripplePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set standbyTimer agar saat game mulai langsung muncul ripple pertama
        standbyTimer = 0; 
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 0)
        {
            // LOGIKA SAAT JALAN
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                TriggerFootstepEcho();
                stepTimer = stepInterval;
            }
            // Reset standbyTimer supaya saat berhenti, jedanya mulai dari awal
            standbyTimer = standbyInterval; 
        }
        else
        {
            // LOGIKA SAAT DIAM (STANDBY)
            stepTimer = 0; // Reset stepTimer agar saat mulai jalan langsung bunyi

            standbyTimer -= Time.deltaTime;
            if (standbyTimer <= 0)
            {
                TriggerFootstepEcho();
                standbyTimer = standbyInterval;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void TriggerFootstepEcho()
    {
        if (ripplePrefab != null)
        {
		Vector3 spawnPos = rb.position;
            Debug.Log("Memicu Gelombang Ekolokasi!");
            GameObject ripple = Instantiate(ripplePrefab, transform.position, Quaternion.identity, this.transform);
            
            //  Jika  pakai RippleEmitter, ganti baris Instantiate di atas 
            // dengan: GetComponent<RippleEmitter>().EmitRipple("White");
		ripple.name = "AttachedRipple";

            Destroy(ripple, 1.0f);
        }
    }
}