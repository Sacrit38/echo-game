using UnityEngine;
using TMPro;
using System.Collections;

public class IntroDialogHandler : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;

    [Header("Intro Settings")]
    public string charName = "Odele";
    [TextArea(3, 10)]
    public string[] introLines;
    
   
    public BasicPlayerMovement playerMovement;

    private int index = 0;

    void Start()
    {
       
        if (playerMovement != null) playerMovement.enabled = false;

       Invoke("StartIntro", 1.0f);
        StartIntro();
    }

    void StartIntro()
    {
        dialogPanel.SetActive(true);
        nameText.text = charName;
        index = 0;
        DisplayLine();
    }

    void Update()
    {
      
        if (dialogPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            index++;
            if (index < introLines.Length)
            {
                DisplayLine();
            }
            else
            {
                EndIntro();
            }
        }
    }

    void DisplayLine()
    {
        messageText.text = introLines[index];
    }

    void EndIntro()
    {
        dialogPanel.SetActive(false);
        
       
        if (playerMovement != null) playerMovement.enabled = true;
        
     
        Destroy(this);
    }
}