using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    public string trigger = "Cutscene1";
    public float delay = 0.5f;
    public int nextSceneIndex = 2;
    bool cutsceneStarted = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            cutsceneStarted = true;
            TransScript.Instance.isPlaying = true;
            TransScript.Instance.Transitioned(trigger, delay);
        }
    }

    void Update()
    {
        if (!TransScript.Instance.isPlaying && cutsceneStarted)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
    }

}
