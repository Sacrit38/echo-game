using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance {get; private set;}

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
        }
    }

    public float bgMusic;
    public float sfx;
}
