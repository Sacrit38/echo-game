using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform target, player;
    
    [Header("Camera Constraint")]
    public SmoothCamera cams;
    public Vector2 start, end;

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
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator SceneTransition()
    {
        TransScript.Instance.Transitoned();
        yield return new WaitForSeconds(0.5f);

        cams.setStart(start);
        cams.setEnd(end);
        player.position = target.position;
        transform.parent.parent.gameObject.SetActive(false);
        target.parent.parent.gameObject.SetActive(true);
    }
}
