using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIInputManager : MonoBehaviour
{
    public GameObject pausePanel, optionPanel, restartConfirmationPanel;
    public Sprite pauseImage, playImage;
    public Image pauseButtonImage;
    public void TogglePause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = Time.timeScale == 0f && !restartConfirmationPanel.activeSelf ? 1.0f : 0f;
        restartConfirmationPanel.SetActive(false);
        pauseButtonImage.sprite = pauseButtonImage.sprite.name == pauseImage.name ? playImage : pauseImage;
    }

    public void ToggleOption()
    {
        optionPanel.SetActive(true);
    }
    
    public void ExitToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void RestartConfirmationToggle()
    {
        restartConfirmationPanel.SetActive(!restartConfirmationPanel.activeSelf);
        Time.timeScale = Time.timeScale == 0f && !pausePanel.activeSelf ? 1.0f : 0f;
    }
}
