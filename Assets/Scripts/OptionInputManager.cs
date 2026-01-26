using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionInputManager : MonoBehaviour
{
    public Slider bgMusic, sfx;
    
    public void ToggleOptions()
    {
        gameObject.SetActive(false);
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
