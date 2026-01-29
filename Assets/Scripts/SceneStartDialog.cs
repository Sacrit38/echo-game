using UnityEngine;
using TMPro;
using System.Collections;

public class SceneStartDialog : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject dialogPanel;
    public CanvasGroup dialogCanvasGroup;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    
    [Header("Screen Fade")]
    public CanvasGroup blackOverlay; 

    [Header("Dialog Settings")]
    public string charName = "Odele";
    [TextArea(3, 10)]
    public string[] startLines;
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 1.5f;

    [Header("Player Reference")]
    public BasicPlayerMovement playerMovement;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
       
        if (playerMovement != null) playerMovement.enabled = false;

       
        dialogPanel.SetActive(false);
        dialogCanvasGroup.alpha = 0;
        
    
        if(blackOverlay != null) blackOverlay.alpha = 1; 

       
        StartCoroutine(StartSceneSequence());
    }

    IEnumerator StartSceneSequence()
    {
        yield return new WaitForSeconds(0.5f);

     
        dialogPanel.SetActive(true);
        nameText.text = charName;
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 0, 1));

    
        foreach (string line in startLines)
        {
            yield return StartCoroutine(TypeText(line));
            
         
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return null; 
        }

     
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 1, 0));
        dialogPanel.SetActive(false);

      
        if (blackOverlay != null)
        {
            yield return StartCoroutine(FadeCanvas(blackOverlay, 1, 0));
        }

     
        if (playerMovement != null) playerMovement.enabled = true;
        
       
        this.gameObject.SetActive(false);
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