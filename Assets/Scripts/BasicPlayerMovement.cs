using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim; 

    [Header("Echolocation Settings")]
    public float stepInterval = 1.2f; 
    private float stepTimer;
    private float defaultStepInterval; 
    
    [Header("Standby Settings")]
    public float standbyInterval = 2.0f; 
    private float standbyTimer;

    [Header("Panic Settings")]
    public SpriteRenderer playerSprite; 
    private Transform enemyTransform;
    private Color currentRippleColor = Color.white;
    public float shakeIntensity = 0.05f; 

    public GameObject ripplePrefab;

    [Header("Camera Bounds")]
    public bool useBounds = true;
    public float minX, maxX, minY, maxY;
    
    private Vector3 currentShakeOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        
        defaultStepInterval = stepInterval;
        standbyTimer = standbyInterval; 
        stepTimer = stepInterval; 

        if (playerSprite == null) playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInput();

        if (anim != null) anim.SetFloat("Speed", moveInput.sqrMagnitude);

       
        if (moveInput != Vector2.zero)
        {
            if (anim != null)
            {
                anim.SetFloat("MoveX", moveInput.x);
                anim.SetFloat("MoveY", moveInput.y);
            }
            
      
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
       
            standbyTimer -= Time.deltaTime;
            if (standbyTimer <= 0)
            {
                TriggerFootstepEcho();
                standbyTimer = standbyInterval;
            }
    
            stepTimer = Mathf.MoveTowards(stepTimer, 0.1f, Time.deltaTime); 
        }

        HandlePanicSystem();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0) moveInput = new Vector2(horizontal, 0);
        else if (vertical != 0) moveInput = new Vector2(0, vertical);
        else moveInput = Vector2.zero;
    }

    void HandlePanicSystem()
    {
     
        if (enemyTransform == null || !enemyTransform.gameObject.activeInHierarchy) 
        {
            StalkerAI stalker = FindObjectOfType<StalkerAI>();
            
            if (stalker != null && stalker.gameObject.activeInHierarchy) 
            {
                enemyTransform = stalker.transform;
            }
            else
            {
          
                enemyTransform = null;
                currentRippleColor = Color.white;
                playerSprite.color = Color.white;
                stepInterval = defaultStepInterval;
                currentShakeOffset = Vector3.zero;
                return;
            }
        }

	playerSprite.color = new Color(1f, 0.8f, 0.8f, 1f); 
        currentRippleColor = new Color(1f, 0.2f, 0.2f, 1f);
        stepInterval = 0.2f;

        Vector2 shakePoint = Random.insideUnitCircle * shakeIntensity;
        currentShakeOffset = new Vector3(shakePoint.x, shakePoint.y, 0);
    }

    void LateUpdate()
    {
        if (Camera.main != null)
        {
            Vector3 finalPos = Camera.main.transform.position;
            if (useBounds)
            {
                finalPos.x = Mathf.Clamp(finalPos.x, minX, maxX);
                finalPos.y = Mathf.Clamp(finalPos.y, minY, maxY);
            }
            if (enemyTransform != null)
            {
                finalPos += currentShakeOffset;
            }
            Camera.main.transform.position = finalPos;
        }
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
            if (rippleSR != null) rippleSR.color = currentRippleColor;
            Destroy(ripple, 1.0f);
        }
    }
}