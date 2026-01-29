using UnityEngine;

public class TurnOff : MonoBehaviour
{
    [Header("Stalker Settings")]
    public GameObject oldStalker;      

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

      

       
        this.gameObject.SetActive(false);
    }
}