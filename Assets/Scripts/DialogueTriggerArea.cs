using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerArea : MonoBehaviour
{
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed) return;
        if (!other.CompareTag("Player")) return;

        BasicPlayerMovement playerMovement = other.GetComponent<BasicPlayerMovement>();
        if (playerMovement != null)
            playerMovement.canMove = false;

        dialogueManager.StartDialogue(dialogueData);

        dialogueManager.OnDialogueEnd += () =>
        {
            if (playerMovement != null)
                playerMovement.canMove = true;

            DisableTrigger();
        };
    }


    void DisableTrigger()
    {
        hasPlayed = true;
        dialogueManager.OnDialogueEnd -= DisableTrigger;
    }
}

