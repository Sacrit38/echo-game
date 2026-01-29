using UnityEngine;

public class StalkerAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 1.0f;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private BasicPlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
        playerMovement = player.GetComponent<BasicPlayerMovement>();
    }

    void Update()
    {
        if (playerTransform != null && playerMovement != null)
        {
            if (!playerMovement.canMove) return;
            Vector2 direction = (Vector2)playerTransform.position - (Vector2)transform.position;
            direction.Normalize();
            moveDirection = direction;

           
            if (moveDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
             
                rb.rotation = angle - 90f; 
            }
        }
    }

    void FixedUpdate()
    {
       
        rb.velocity = moveDirection * speed;
    }
}