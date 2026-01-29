using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private TestDialogue testDialogue;

    void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
        testDialogue = GetComponent<TestDialogue>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (testDialogue != null && testDialogue.dialogueData != null)
            {
                dialogueManager.StartDialogue(testDialogue.dialogueData);
            }
            else
            {
                Debug.LogWarning("Missing TestDialogue or DialogueData!");
            }
        }
    }
}