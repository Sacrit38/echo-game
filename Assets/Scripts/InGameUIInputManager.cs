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
    public AudioSource audioSource;
    public void TogglePause()
    {
        audioSource.Play();
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = !pausePanel.activeSelf ? 1.0f : 0f;
        restartConfirmationPanel.SetActive(false);
        pauseButtonImage.sprite = pauseButtonImage.sprite.name == pauseImage.name ? playImage : pauseImage;
    }

    public void ToggleOption()
    {
        audioSource.Play();
        optionPanel.SetActive(true);
    }
    
    public void ExitToTitle()
    {
        audioSource.Play();
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        audioSource.Play();
	Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void RestartConfirmationToggle()
    {
        audioSource.Play();
        restartConfirmationPanel.SetActive(!restartConfirmationPanel.activeSelf);
        Time.timeScale = Time.timeScale == 0f && !pausePanel.activeSelf ? 1.0f : 0f;
    }
}
