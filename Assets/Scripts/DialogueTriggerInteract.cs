using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerInteract : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;

    [Header("Interaction Settings")]
    public KeyCode interactKey = KeyCode.E;
    public float interactionRange = 2f; 

    private bool hasPlayed = false;
    private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void Update()
    {
        if (hasPlayed) return;
        if (player == null) return;

        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= interactionRange && Input.GetKeyDown(interactKey))
        {
            TriggerDialogue();
        }
    }

    void TriggerDialogue()
    {
        BasicPlayerMovement playerMovement = player.GetComponent<BasicPlayerMovement>();
        if (playerMovement != null)
            playerMovement.canMove = false;

        dialogueManager.StartDialogue(dialogueData);

        dialogueManager.OnDialogueEnd += () =>
        {
            if (playerMovement != null)
                playerMovement.canMove = true;

            hasPlayed = true; 
            dialogueManager.OnDialogueEnd -= null; 
        };
    }
}

