using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransScript : MonoBehaviour
{
    [Serializable]
    public class SoundPair<TKey, TValue>
    {
        public TKey trigger;
        public TValue value;

        public SoundPair(TKey trigger, TValue value)
        {
            this.trigger = trigger;
            this.value = value;
        }
    }
    public static TransScript Instance {get; private set;}
    [Header("SoundList")]
    public List<SoundPair<string, AudioClip>> soundPairs;
    Dictionary<string, AudioClip> soundDict = new();
    void Awake()
    {
        Instance = this;
        foreach (SoundPair<string, AudioClip> item in soundPairs)
        {
            if (!soundDict.ContainsKey(item.trigger))
            {
                soundDict.Add(item.trigger, item.value);
            }
        }
    }
    
    [Header("SoundControl")]
    public AudioSource audioSource;
    [Header("AnimationControl")]
    public Animator transAnimator;
    public bool isPlaying;

    public void Transitioned(string animationTrigger)
    {
        transAnimator.SetTrigger(animationTrigger);
        audioSource.clip = soundDict[animationTrigger];
        audioSource.Play();
        StartCoroutine(CheckAnimationEnd());
    }
    
    IEnumerator CheckAnimationEnd()
    {
        while (transAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        isPlaying = false;
    }



}
