using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim; 

    [Header("Echolocation Settings")]
    public float stepInterval = 0.5f; 
    private float stepTimer;
    private float defaultStepInterval; 
    
    [Header("Standby Settings")]
    public float standbyInterval = 2.0f; 
    private float standbyTimer;

    [Header("Panic Settings")]
    public SpriteRenderer playerSprite; 
    private Transform enemyTransform;
    private Color currentRippleColor = Color.white;

    public GameObject ripplePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        
        defaultStepInterval = stepInterval;
        standbyTimer = 0; 

        if (playerSprite == null) playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInput();

        anim.SetFloat("Speed", moveInput.sqrMagnitude);

        if (moveInput != Vector2.zero)
        {
            anim.SetFloat("MoveX", moveInput.x);
            anim.SetFloat("MoveY", moveInput.y);
            
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                TriggerFootstepEcho();
                stepTimer = stepInterval;
            }
            standbyTimer = standbyInterval; 
        }
        else
        {
            stepTimer = 0; 
            standbyTimer -= Time.deltaTime;
            if (standbyTimer <= 0)
            {
                TriggerFootstepEcho();
                standbyTimer = standbyInterval;
            }
        }

        HandlePanicSystem();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0) 
        {
            moveInput = new Vector2(horizontal, 0);
        }
        else if (vertical != 0) 
        {
            moveInput = new Vector2(0, vertical);
        }
        else 
        {
            moveInput = Vector2.zero;
        }
    }

    void HandlePanicSystem()
    {
        if (enemyTransform == null) 
        {
            StalkerAI stalker = FindObjectOfType<StalkerAI>();
            if (stalker) 
            {
                enemyTransform = stalker.transform;
            }
            else
            {
                currentRippleColor = Color.white;
                playerSprite.color = Color.white;
                stepInterval = defaultStepInterval;
                return;
            }
        }

        // Jika stalker ada di scene, langsung buat merah & detak cepat
        currentRippleColor = Color.red;
        playerSprite.color = Color.red;
        stepInterval = 0.2f; // Detak jantung cepat tanda dikejar
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void TriggerFootstepEcho()
    {
        if (ripplePrefab != null)
        {
            GameObject ripple = Instantiate(ripplePrefab, transform.position, Quaternion.identity, this.transform);
            ripple.name = "AttachedRipple";

            SpriteRenderer rippleSR = ripple.GetComponent<SpriteRenderer>();
            if (rippleSR != null)
            {
                rippleSR.color = currentRippleColor;
            }

            Destroy(ripple, 1.0f);
        }
    }
}