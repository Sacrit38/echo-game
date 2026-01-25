using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInputManager : MonoBehaviour
{
    public GameObject optionPanel;
    public Slider bgMusic, sfx;
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

    void Start()
    {
        bgMusic.value = GlobalScript.Instance.bgMusic;
        sfx.value = GlobalScript.Instance.sfx;
        bgMusic.onValueChanged.AddListener(delegate {bgmSlider();});
        sfx.onValueChanged.AddListener(delegate {sfxSlider();});
    }

    void bgmSlider(){ GlobalScript.Instance.setBgMusic(bgMusic.value);}
    void sfxSlider(){ GlobalScript.Instance.setSfx(sfx.value);}

}
