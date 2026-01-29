using UnityEngine;
using TMPro;

public class DialogNOn : MonoBehaviour
{
    [Header("Dialog Settings")]
    public string npcName;
    [TextArea(3, 10)]
    public string[] dialogLines;
    
    [Header("UI Reference")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;

    [Header("Event Settings")]
    [Tooltip("Object ini akan aktif setelah dialog selesai")]
    public GameObject objectToEnable; 

    private bool isPlayerInRange = false;
    private int currentLineIndex = 0;
    private bool isDialogActive = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogActive)
            {
                StartDialog();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }

    void StartDialog()
    {
        isDialogActive = true;
        dialogPanel.SetActive(true);
        currentLineIndex = 0;
        nameText.text = npcName;
        
        Time.timeScale = 0f; 

        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            messageText.text = dialogLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        isDialogActive = false;
        dialogPanel.SetActive(false);
        Time.timeScale = 1f;

       
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log(objectToEnable.name + " telah diaktifkan!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
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