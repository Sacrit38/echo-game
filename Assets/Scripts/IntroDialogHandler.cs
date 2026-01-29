using UnityEngine;
using TMPro;
using System.Collections;

public class IntroDialogHandler : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    
    [Header("Chapter Transition Reference")]
    public TextMeshProUGUI chapterTitleText; 
    public CanvasGroup blackOverlay;

    [Header("Intro Settings")]
    public string charName = "Odele";
    [TextArea(3, 10)]
    public string[] introLines;
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 1f;
    
    public BasicPlayerMovement playerMovement;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        // Setup awal
        if (playerMovement != null) playerMovement.enabled = false;
        
        chapterTitleText.text = "";
        chapterTitleText.alpha = 0;
        blackOverlay.alpha = 1; 
        dialogPanel.SetActive(false);

        Invoke("StartIntro", 1.0f);
    }

    void StartIntro()
    {
        dialogPanel.SetActive(true);
        nameText.text = charName;
        index = 0;
        StartCoroutine(TypeText(introLines[index]));
    }

    void Update()
    {
        if (dialogPanel.activeSelf && Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            index++;
            if (index < introLines.Length)
            {
                StartCoroutine(TypeText(introLines[index]));
            }
            else
            {
                StartCoroutine(EndIntroSequence());
            }
        }
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

    IEnumerator EndIntroSequence()
    {
    
        dialogPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);

    
        chapterTitleText.text = "Chapter 4";
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * fadeSpeed;
            chapterTitleText.alpha = t;
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

   
        while (blackOverlay.alpha > 0)
        {
            blackOverlay.alpha -= Time.deltaTime * fadeSpeed;
            chapterTitleText.alpha = blackOverlay.alpha;
            yield return null;
        }

    
        if (playerMovement != null) playerMovement.enabled = true;
        Destroy(this.gameObject);
    }
}