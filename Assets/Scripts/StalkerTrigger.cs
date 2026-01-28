using UnityEngine;
using TMPro;
using System.Collections;

public class StalkerTrigger : MonoBehaviour
{
    [Header("Stalker Reference")]
    public GameObject stalkerPrefab;

    [Header("Dialog Settings")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    public string[] alertLines;

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
        nameText.text = "Odele";
        
        foreach (string line in alertLines)
        {
            messageText.text = line;
           
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return null; 
        }

        dialogPanel.SetActive(false);

       
        if (stalkerPrefab != null)
        {
            stalkerPrefab.SetActive(true);
         
        }

     
        if (playerMovement != null) playerMovement.enabled = true;

      
        this.gameObject.SetActive(false);
    }
}