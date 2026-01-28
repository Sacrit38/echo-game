using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform targetSpawn;
    public Vector2 camStart, camEnd;
    public GameObject currentRoom, nextRoom;
    private bool playerInside = false;

    void OnTriggerEnter2D(Collider2D other) { if(other.CompareTag("Player")) playerInside = true; }
    void OnTriggerExit2D(Collider2D other) { if(other.CompareTag("Player")) playerInside = false; }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
          
            SimpleDoor.Instance.StartTransition(targetSpawn, camStart, camEnd, currentRoom, nextRoom);
        }
    }
}