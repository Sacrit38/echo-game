using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionInputManager : MonoBehaviour
{
    public Slider bgMusic, sfx;
    
    public AudioSource audioSource;
    public void CallToggleOptions()
    {
        StartCoroutine(ToggleOptions());
    }
    public IEnumerator ToggleOptions()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    void Start()
    {
        bgMusic.value = GlobalScript.Instance.bgMusic;
        sfx.value = GlobalScript.Instance.sfx;
        bgMusic.onValueChanged.AddListener(delegate {bgmSlider();});
        sfx.onValueChanged.AddListener(delegate {sfxSlider();});
    }

    void bgmSlider()
    {
        audioSource.Play();
        GlobalScript.Instance.setBgMusic(bgMusic.value);
    }
    void sfxSlider()
    {
        audioSource.Play();
        GlobalScript.Instance.setSfx(sfx.value);
    }
}
