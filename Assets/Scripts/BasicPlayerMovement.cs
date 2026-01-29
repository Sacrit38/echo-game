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

    [Header("Panic Audio")]
    public AudioClip[] breathingSounds;
    public AudioClip[] heartbeatSounds;
    private AudioSource panicAudioSource;
    private bool isInPanicMode = false;
    private int breathingIndex = 0;
    private int heartbeatIndex = 0;

    [Header("Camera Bounds")]
    public bool useBounds = true;
    public float minX, maxX, minY, maxY;
    
    private Vector3 currentShakeOffset;

    [Header("Control")]
    public bool canMove = true;
    [Header("Footstep Audio")]
    public AudioClip[] concreteFootsteps;
    public AudioClip[] woodFootsteps;
    private AudioClip[] currentFootsteps;
    private int footstepIndex = 0;
    private AudioSource footstepAudioSource;
    private bool wasMovingLastFrame = false;
    private bool inputJustPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        footstepAudioSource = GetComponent<AudioSource>();
        panicAudioSource = gameObject.AddComponent<AudioSource>();

        currentFootsteps = concreteFootsteps;
        ShuffleFootsteps();

        defaultStepInterval = stepInterval;
        standbyTimer = standbyInterval;

        if (playerSprite == null) playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInput();

        // audio
        if (moveInput != Vector2.zero && !wasMovingLastFrame)
        {
            PlayFootstepSound();
            stepTimer = stepInterval;
        }
        wasMovingLastFrame = (moveInput != Vector2.zero);

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
                PlayFootstepSound();
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
        if (!canMove)
        {
            moveInput = Vector2.zero; // stop player from moving
            return;
        }

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
                if (isInPanicMode)
                {
                    isInPanicMode = false;
                    panicAudioSource.Stop();
                }

                enemyTransform = null;
                currentRippleColor = Color.white;
                playerSprite.color = Color.white;
                stepInterval = defaultStepInterval;
                currentShakeOffset = Vector3.zero;
                return;
            }
        }

        if (!panicAudioSource.isPlaying)
        {
            PlayNextPanicSound();
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
        if (!canMove) return;
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetGroundType(string type)
    {
        Debug.Log("SetGroundType called with: " + type); // ADD THIS
        AudioClip[] newFootsteps = (type == "wood") ? woodFootsteps : concreteFootsteps;

        if (newFootsteps != currentFootsteps)
        {
            currentFootsteps = newFootsteps;
            footstepIndex = 0;
            ShuffleFootsteps();
            Debug.Log("Ground type changed! Current footsteps: " + currentFootsteps.Length); // ADD THIS
        }
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

    void ShuffleFootsteps()
    {
        if (currentFootsteps == null || currentFootsteps.Length == 0) return;

        for (int i = 0; i < currentFootsteps.Length; i++)
        {
            int rand = Random.Range(i, currentFootsteps.Length);
            AudioClip temp = currentFootsteps[i];
            currentFootsteps[i] = currentFootsteps[rand];
            currentFootsteps[rand] = temp;
        }
    }

    void PlayFootstepSound()
    {
        if (currentFootsteps != null && currentFootsteps.Length > 0 && footstepAudioSource != null)
        {
            footstepAudioSource.PlayOneShot(currentFootsteps[footstepIndex]);

            footstepIndex++;
            if (footstepIndex >= currentFootsteps.Length)
            {
                footstepIndex = 0;
                ShuffleFootsteps();
            }
        }
    }

    void ShufflePanicSounds()
    {
        if (breathingSounds != null && breathingSounds.Length > 0)
        {
            for (int i = 0; i < breathingSounds.Length; i++)
            {
                int rand = Random.Range(i, breathingSounds.Length);
                AudioClip temp = breathingSounds[i];
                breathingSounds[i] = breathingSounds[rand];
                breathingSounds[rand] = temp;
            }
        }

        if (heartbeatSounds != null && heartbeatSounds.Length > 0)
        {
            for (int i = 0; i < heartbeatSounds.Length; i++)
            {
                int rand = Random.Range(i, heartbeatSounds.Length);
                AudioClip temp = heartbeatSounds[i];
                heartbeatSounds[i] = heartbeatSounds[rand];
                heartbeatSounds[rand] = temp;
            }
        }

        breathingIndex = 0;
        heartbeatIndex = 0;
    }

    void PlayNextPanicSound()
    {
        bool playBreathing = Random.value > 0.5f;

        if (playBreathing && breathingSounds != null && breathingSounds.Length > 0)
        {
            panicAudioSource.PlayOneShot(breathingSounds[breathingIndex]);
            breathingIndex++;
            if (breathingIndex >= breathingSounds.Length)
            {
                breathingIndex = 0;
                ShuffleArray(breathingSounds);
            }
        }
        else if (heartbeatSounds != null && heartbeatSounds.Length > 0)
        {
            panicAudioSource.PlayOneShot(heartbeatSounds[heartbeatIndex]);
            heartbeatIndex++;
            if (heartbeatIndex >= heartbeatSounds.Length)
            {
                heartbeatIndex = 0;
                ShuffleArray(heartbeatSounds);
            }
        }
    }

    void ShuffleArray(AudioClip[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            AudioClip temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }
}