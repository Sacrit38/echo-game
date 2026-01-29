using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerDialogSequence : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject dialogPanel;
    public CanvasGroup dialogCanvasGroup;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    
    [Header("Dialog Settings")]
    public string charName = "Odele";
    [TextArea(3, 10)]
    public string[] dialogLines;
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 1.5f;

    [Header("Player Reference")]
    public MonoBehaviour playerMovement;

    private bool hasTriggered = false;
    private bool isTyping = false;

    void Awake()
    {
    
        if (dialogPanel != null) dialogPanel.SetActive(false);
        if (dialogCanvasGroup != null) dialogCanvasGroup.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(StartDialogSequence());
        }
    }

    IEnumerator StartDialogSequence()
    {
   
        if (playerMovement != null) playerMovement.enabled = false;

     
        dialogPanel.SetActive(true);
        nameText.text = charName;
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 0, 1));

       
        foreach (string line in dialogLines)
        {
            yield return StartCoroutine(TypeText(line));
            
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return null; 
        }

  
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 1, 0));
        dialogPanel.SetActive(false);

       
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        messageText.text = "";
        foreach (char c in line.ToCharArray())
        {
            messageText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float start, float end)
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
}