using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputManager : MonoBehaviour
{
    public GameObject optionPanel;
    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }
    
    public void ToggleOptions()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }

    

}
