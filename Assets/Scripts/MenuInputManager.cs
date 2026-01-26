using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputManager : MonoBehaviour
{
    public GameObject optionPanel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ToggleOptions()
    {
        optionPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
