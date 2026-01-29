using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TransScript : MonoBehaviour
{
    [Serializable]
    public class Pair<TKey, TValue>
    {
        public TKey trigger;
        public TValue value;

        public Pair(TKey trigger, TValue value)
        {
            this.trigger = trigger;
            this.value = value;
        }
    }
    public static TransScript Instance {get; private set;}
    [Header("SoundList")]
    public List<Pair<string, AudioClip>> soundPairs;
    Dictionary<string, AudioClip> soundDict = new();

    [Header("ActionList")]
    public List<Pair<string, List<Pair<string, GameObject>>>> actionPairs;
    Dictionary<string, List<Pair<string, GameObject>>> actionDict = new();

    void Awake()
    {
        Instance = this;
        foreach (Pair<string, AudioClip> item in soundPairs)
        {
            if (!soundDict.ContainsKey(item.trigger))
            {
                soundDict.Add(item.trigger, item.value);
            }
        }
        foreach (Pair<string, List<Pair<string, GameObject>>> item in actionPairs)
        {
            if (!actionDict.ContainsKey(item.trigger))
            {
                actionDict.Add(item.trigger, item.value);
            }
        }
    }
    
    [Header("SoundControl")]
    public AudioSource audioSource;
    [Header("AnimationControl")]
    public Animator transAnimator;
    public bool isPlaying;

    public void Transitioned(string trigger, float delay)
    {
        transAnimator.SetTrigger(trigger);
        if (soundDict.ContainsKey(trigger))
        {
            audioSource.clip = soundDict[trigger];
            audioSource.Play();
        }

        StartCoroutine(CheckAnimationEnd(trigger, delay));
    }
    
    IEnumerator CheckAnimationEnd(string trigger, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (actionDict.ContainsKey(trigger))
        {
            foreach (Pair<string, GameObject> item in actionDict[trigger])
            {
                switch (item.trigger)
                {
                    case "activate":
                        item.value.SetActive(true);
                        break;
                    case "deactivate":
                        item.value.SetActive(false);
                        break;
                    
                    
                    default:
                        TextMeshProUGUI textMeshProUGUI;
                        if (item.value.TryGetComponent(out textMeshProUGUI))
                        {
                            textMeshProUGUI.SetText(item.trigger);
                        }
                        break;
                }
            }
        }
        while (transAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        isPlaying = false;
    }



}
