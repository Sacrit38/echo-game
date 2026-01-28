using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LaurentEvent : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform mcTarget; 
    public float walkSpeed = 2.5f;
    private bool isApproaching = false;
    private bool hasArrived = false;

    [Header("Dialog Settings")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    public string[] conversation;
    private int dialogIndex = 0;

    [Header("Scene Transition")]
    public string nextSceneName;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (anim != null) anim.SetBool("isWalking", false);
    }

    void Update()
    {
        if (isApproaching && !hasArrived)
        {
            MoveToMC();
        }

        if (hasArrived && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

    public void StartApproaching()
    {
        if (!isApproaching)
        {
            isApproaching = true;
          
            if (anim != null) anim.SetBool("isWalking", true);
        }
    }

    void MoveToMC()
    {
float distance = Vector2.Distance(transform.position, mcTarget.position);
        
        if (distance > 1.2f) 
        {
            Vector2 direction = (mcTarget.position - transform.position).normalized;
            rb.velocity = direction * walkSpeed;

            // --- LOGIKA ROTASI/FLIP ---
            // Jika bergerak ke kanan (x positif) dan sprite menghadap kiri, balik ke kanan
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            // Jika bergerak ke kiri (x negatif) dan sprite menghadap kanan, balik ke kiri
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            // ---------------------------
        }
        else
        {
            rb.velocity = Vector2.zero;
            hasArrived = true;

            if (anim != null) anim.SetBool("isWalking", false);
            
            StartCoroutine(ShowDialogWithDelay());
        }
    }

    IEnumerator ShowDialogWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        dialogPanel.SetActive(true);
        nameText.text = "Laurent";
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (dialogIndex < conversation.Length)
        {
            messageText.text = conversation[dialogIndex];
            dialogIndex++;
        }
        else
        {
            EndConversation();
        }
    }

    void EndConversation()
    {
        dialogPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }
}