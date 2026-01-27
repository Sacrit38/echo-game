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
    
    [Header("Standby Settings")]
    public float standbyInterval = 2.0f; 
    private float standbyTimer;

    public GameObject ripplePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        standbyTimer = 0; 
    }

    void Update()
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
            Destroy(ripple, 1.0f);
        }
    }
}