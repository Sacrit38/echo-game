using UnityEngine;
using System.Collections;

public class ConversationManager : MonoBehaviour
{
    [Header("Characters")]
    public ChatBubble odeleBubble;
    public ChatBubble npcBubble;

    [Header("Player Movement")]
    public BasicPlayerMovement playerMovement;

    [System.Serializable]
    public struct DialogueLine
    {
        public bool isOdele; 
        [TextArea] public string message;
        public float delayAfter;
    }

    public DialogueLine[] conversation;

    private bool isTalking = false;

    public void StartConversation()
    {
        if (!isTalking)
        {
            StartCoroutine(PlayConversation());
        }
    }

    IEnumerator PlayConversation()
    {
        isTalking = true;
        if (playerMovement != null) playerMovement.enabled = false;

        foreach (DialogueLine line in conversation)
        {
            if (line.isOdele)
                odeleBubble.Speak(line.message, 2f);
            else
                npcBubble.Speak(line.message, 2f);

        
            yield return new WaitForSeconds(line.delayAfter + (line.message.Length * 0.05f));
        }

        if (playerMovement != null) playerMovement.enabled = true;
        isTalking = false;
    }
}