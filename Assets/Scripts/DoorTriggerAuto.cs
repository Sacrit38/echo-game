using UnityEngine;

public class DoorTriggerAuto : MonoBehaviour
{
    [Header("Transition Settings")]
    public Transform targetSpawn;
    public Vector2 camStart, camEnd;
    public GameObject currentRoom, nextRoom;
    
    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            
           
            if (SimpleDoor.Instance != null)
            {
                SimpleDoor.Instance.StartTransition(targetSpawn, camStart, camEnd, currentRoom, nextRoom);
                Debug.Log("Transisi Ruangan Otomatis Berhasil!");
            }
            

            this.gameObject.SetActive(false);
        }
    }
}