using UnityEngine;
using TMPro;
using System.Collections;

public class StalkerTrigger : MonoBehaviour
{
    [Header("Stalker Reference")]
    public GameObject stalkerPrefab;

    [Header("Dialog Settings")]
    public GameObject dialogPanel;
    public CanvasGroup dialogCanvasGroup;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    public string charName = "Odele";
    public string[] alertLines;
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 2f;

    [Header("Player Reference")]
    public BasicPlayerMovement playerMovement;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            StartCoroutine(TriggerSequence());
        }
    }

    IEnumerator TriggerSequence()
    {
        if (playerMovement != null) playerMovement.enabled = false;

      
        dialogPanel.SetActive(true);
        nameText.text = charName;
        messageText.text = "";
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 0, 1));

       
        foreach (string line in alertLines)
        {
            yield return StartCoroutine(TypeText(line));
            
        
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return null; 
        }

       
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 1, 0));
        dialogPanel.SetActive(false);

   
        if (stalkerPrefab != null)
        {
            stalkerPrefab.SetActive(true);
        }

        if (playerMovement != null) playerMovement.enabled = true;
        this.gameObject.SetActive(false);
    }

    IEnumerator TypeText(string line)
    {
        messageText.text = "";
        foreach (char c in line.ToCharArray())
        {
            messageText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    IEnumerator FadeCanvas(CanvasGroup cg, float start, float end)
    {
        float timer = 0;
        cg.alpha = start;
        while (timer < 1f)
        {
            timer += Time.deltaTime * fadeSpeed;
            cg.alpha = Mathf.Lerp(start, end, timer);
            yield return null;
        }
        cg.alpha = end;
    }
}