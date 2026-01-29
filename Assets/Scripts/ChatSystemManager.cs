using UnityEngine;
using TMPro;
using System.Collections;

public class ChatSystemManager : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueLine
    {
        public string characterName;
        [TextArea(3, 10)] public string message;
        public CanvasGroup targetBubble; 
    }

    [Header("Dialog Data")]
    public DialogueLine[] dialogLines;
    
    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 5f;

    [Header("References")]
    public BasicPlayerMovement playerMovement;
    public GameObject objectToEnable;

    private int currentLineIndex = 0;
    private bool isDialogActive = false;
    private bool isPlayerInRange = false;
    private bool isTyping = false;
    private string currentFullText;

    void Update()
    {
      
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogActive)
            {
                StartDialog();
            }
            else if (!isTyping)
            {
                DisplayNextLine();
            }
            else
            {
            
                StopAllCoroutines();
                isTyping = false;
                DialogueLine line = dialogLines[currentLineIndex - 1];
                line.targetBubble.GetComponentInChildren<TextMeshProUGUI>().text = currentFullText;
            }
        }
    }

    void StartDialog()
    {
        isDialogActive = true;
        currentLineIndex = 0;
        if (playerMovement != null) playerMovement.enabled = false;
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
         
            if (currentLineIndex > 0) 
                StartCoroutine(FadeBubble(dialogLines[currentLineIndex - 1].targetBubble, 1, 0));

          
            DialogueLine line = dialogLines[currentLineIndex];
            StartCoroutine(HandleLine(line));
            currentLineIndex++;
        }
        else
        {
            EndDialog();
        }
    }

    IEnumerator HandleLine(DialogueLine line)
    {
        isTyping = true;
        currentFullText = line.message;
        
    
        TextMeshProUGUI textDisplay = line.targetBubble.GetComponentInChildren<TextMeshProUGUI>();
        textDisplay.text = "";
        
       
        yield return StartCoroutine(FadeBubble(line.targetBubble, 0, 1));

      
        foreach (char c in line.message.ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void EndDialog()
    {
     
        if (currentLineIndex > 0)
            StartCoroutine(FadeBubble(dialogLines[currentLineIndex - 1].targetBubble, 1, 0));

        isDialogActive = false;
        if (playerMovement != null) playerMovement.enabled = true;

        if (objectToEnable != null) objectToEnable.SetActive(true);
    }

    IEnumerator FadeBubble(CanvasGroup cg, float start, float end)
    {
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime * fadeSpeed;
            cg.alpha = Mathf.Lerp(start, end, timer);
            yield return null;
        }
        cg.alpha = end;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            EndDialog();
        }
    }
}