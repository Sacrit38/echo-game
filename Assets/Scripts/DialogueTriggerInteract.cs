using UnityEngine;

public class DialogueTriggerInteract : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private GetDialogue getDialogue;

    void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
        getDialogue = GetComponent<GetDialogue>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (getDialogue != null && getDialogue.dialogueData != null)
            {
                dialogueManager.StartDialogue(getDialogue.dialogueData);
            }
            else
            {
                Debug.LogWarning("Missing getDialogue or DialogueData!");
            }
        }
    }
}