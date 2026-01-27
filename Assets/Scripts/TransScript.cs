using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransScript : MonoBehaviour
{
    public static TransScript Instance {get; private set;}
    void Awake()
    {
        Instance = this;
    }
    public Animator transAnimator;
    public bool isPlaying;
    public void Transitoned(string animationTrigger)
    {
        transAnimator.SetTrigger(animationTrigger);
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
