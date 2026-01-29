using UnityEngine;

public class SwitchStalk : MonoBehaviour
{
    [Header("Stalker Settings")]
    public GameObject oldStalker;      
    public GameObject newStalkerPrefab; 

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            SwitchStalker();
        }
    }

    void SwitchStalker()
    {
      
        if (oldStalker != null)
        {
            oldStalker.SetActive(false); 
           
        }

       
        if (newStalkerPrefab != null)
        {
            newStalkerPrefab.SetActive(true);
        }

       
        this.gameObject.SetActive(false);
    }
}