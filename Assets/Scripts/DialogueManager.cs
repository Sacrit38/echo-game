using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    private DialogueData currentDialogue;
    private int currentLineIndex;

    void Update()
    {
        if (currentDialogue == null) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        currentDialogue = dialogueData;
        currentLineIndex = 0;
        DisplayLine();
    }

    void DisplayLine()
    {
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            DialogueLine line = currentDialogue.lines[currentLineIndex];
            speakerText.text = line.speakerName;
            dialogueText.text = line.lines;
        }
        else
        {
            EndDialogue();
        }
    }

    void NextLine()
    {
        currentLineIndex++;
        DisplayLine();
    }

    void EndDialogue()
    {
        speakerText.text = "";
        dialogueText.text = "";
        currentDialogue = null;
        Debug.Log("Dialogue ended");
    }
}
