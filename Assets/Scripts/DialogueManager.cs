using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public Action OnDialogueEnd;

    [Header("UI References")]
    public GameObject dialogueUI;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    private DialogueData currentDialogue;
    private int currentLineIndex;
    private bool isDialogueActive;

    void Update()
    {
        if (!isDialogueActive) {
            dialogueUI.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        if (isDialogueActive) return;

        currentDialogue = dialogueData;
        currentLineIndex = 0;
        isDialogueActive = true;

        dialogueUI.SetActive(true);
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
        isDialogueActive = false;

        OnDialogueEnd?.Invoke();
        OnDialogueEnd = null;
    }
}
