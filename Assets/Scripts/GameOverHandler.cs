using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverHandler : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Button restartButton;
    public GameObject gameOverPanel;
    public CanvasGroup panelCanvasGroup;

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 2f;

    public void SetupGameOver()
    {
      
        restartButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        
        string currentLevel = SceneManager.GetActiveScene().name;
        string dialog = "";

        if (currentLevel.Contains("Chapter I")) 
            dialog = "“Ah...You're as beautiful as the day I saw you, my dear.”";
        else if (currentLevel.Contains("Chapter 2"))
            dialog = "“You don't have to be scared anymore.”";
        else if (currentLevel.Contains("Chapter 3"))
            dialog = "“See? No one else came for you.”";
        else if (currentLevel.Contains("Chapter 4"))
            dialog = "“It's okay...stop running, You're safe with me now.”";
        else
            dialog = "“Aku tidak bisa melarikan diri...”";

       
        StartCoroutine(GameOverSequence(dialog));
        
        Time.timeScale = 0f; 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator GameOverSequence(string fullText)
    {
     
        float alpha = 0;
        panelCanvasGroup.alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.unscaledDeltaTime * fadeSpeed;
            panelCanvasGroup.alpha = alpha;
            yield return null;
        }

      
        dialogText.text = "";
        foreach (char c in fullText.ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

  
        restartButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartSequence());
    }

    IEnumerator RestartSequence()
    {
  
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.unscaledDeltaTime * fadeSpeed;
            panelCanvasGroup.alpha = alpha;
            yield return null;
        }

        Time.timeScale = 1f; 

 
        if (SimpleDoor.Instance != null && SimpleDoor.Instance.fadeImage != null)
        {
            SimpleDoor.Instance.fadeImage.color = new Color(0, 0, 0, 0);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}