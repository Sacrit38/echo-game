using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioRandomizer : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    int random;
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            random = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[random];
            audioSource.Play();
        }
    }
}
