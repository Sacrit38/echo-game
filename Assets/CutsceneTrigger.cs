using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    public string trigger = "Cutscene1";
    public float delay = 0.5f;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            TransScript.Instance.isPlaying = true;
            TransScript.Instance.Transitioned(trigger, delay);
        }
    }

    void Update()
    {
        if (!TransScript.Instance.isPlaying)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }

}
