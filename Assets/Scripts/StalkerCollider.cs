using UnityEngine;
using System.Collections;

public class StalkerCollider : MonoBehaviour
{
    [Header("Stalker Reference")]
    public GameObject stalkerPrefab; 

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            ActivateStalker();
        }
    }

    void ActivateStalker()
    {
        if (stalkerPrefab != null)
        {
           
            stalkerPrefab.SetActive(true);
            
      
        }

       
        this.gameObject.SetActive(false);
    }
}