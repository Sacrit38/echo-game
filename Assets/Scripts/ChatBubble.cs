using UnityEngine;
using TMPro;
using System.Collections;

public class ChatBubble : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.04f;

    public void Speak(string message, float duration)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeText(message, duration));
    }

    IEnumerator TypeText(string message, float duration)
    {
        textDisplay.text = "";
        foreach (char c in message.ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}