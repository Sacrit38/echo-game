using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingTesting : MonoBehaviour
{
    [Header("Gerakgerak")]
    public float speed = 2f;
    public float patrolDistance = 3f; 
    public bool pauseAtEnds = false;
    public float pauseDuration = 0.5f;

    [Header("Visual")]
    public bool flipSprite = true;
    public bool flipUsingScale = false; 

    private Rigidbody2D rb;
    private Vector2 startPos;
    private int dir = 1; // 1 ke kanan, -1 ke kiri
    private float pauseTimer = 0f;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        // Pause di ujung
        if (pauseTimer > 0f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            pauseTimer -= Time.fixedDeltaTime;
            return;
        }

        // Gerak
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);

        // Cek batas kiri/kanan relatif ke startPos.x
        float offsetX = transform.position.x - startPos.x;
        if (dir == 1 && offsetX >= patrolDistance)
        {
            ChangeDirection(-1);
        }
        else if (dir == -1 && offsetX <= -patrolDistance)
        {
            ChangeDirection(1);
        }
    }

    void ChangeDirection(int newDir)
    {
        dir = newDir;

        // Flip visual
        if (flipSprite && sr != null)
            sr.flipX = (dir < 0);

        if (flipUsingScale)
        {
            var s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (dir < 0 ? -1 : 1);
            transform.localScale = s;
        }

        if (pauseAtEnds) pauseTimer = pauseDuration;
    }

    private void OnValidate()
    {
        if (pauseDuration < 0) pauseDuration = 0;
        if (speed < 0) speed = 0;
        if (patrolDistance < 0) patrolDistance = 0;
    }
}

