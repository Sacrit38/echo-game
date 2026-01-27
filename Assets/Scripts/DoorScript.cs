using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform target, player;
    
    [Header("Camera Constraint")]
    public SmoothCamera cams;
    public Vector2 start, end;

    [Header("Transition Animation")]
    public string animationTrigger = "Transitioned";
    public float actionDelay = 0.5f;

    bool interact = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            interact = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            interact = false;
        }
    }

    void Update()
    {
        if (interact && Input.GetButtonDown("Fire1") && !TransScript.Instance.isPlaying)
        {
            TransScript.Instance.isPlaying = true;
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator SceneTransition()
    {
        TransScript.Instance.Transitoned(animationTrigger);
        yield return new WaitForSeconds(actionDelay);

        cams.setStart(start);
        cams.setEnd(end);
        player.position = target.position;
        transform.parent.parent.gameObject.SetActive(false);
        target.parent.parent.gameObject.SetActive(true);
    }
}
