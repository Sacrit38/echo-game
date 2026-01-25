using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance {get; private set;}
    public float bgMusic {get; private set;}
    public float sfx {get; private set;}

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            bgMusic = PlayerPrefs.GetFloat("bgMusic");
            sfx = PlayerPrefs.GetFloat("sfx");
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setBgMusic(float bgMusic)
    {
        this.bgMusic = bgMusic;
        PlayerPrefs.SetFloat("bgMusic", bgMusic);
    }

    public void setSfx(float sfx)
    {
        this.sfx = sfx;
        PlayerPrefs.SetFloat("sfx", sfx);
    }

}
