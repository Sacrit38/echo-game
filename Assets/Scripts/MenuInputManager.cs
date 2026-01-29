using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputManager : MonoBehaviour
{
    public GameObject optionPanel;
    public Collider collidery;
    public AudioSource audioSource;
    public void Play()
    {
        audioSource.Play();
        SceneManager.LoadScene(1);
    }
    
    public void ToggleOptions()
    {
        audioSource.Play();
        optionPanel.SetActive(true);
    }

    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }


}
