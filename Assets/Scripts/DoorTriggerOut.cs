using UnityEngine;

public class DoorTriggerOut : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad = "Chapter4Outdoor";
    private bool playerInside = false;

    void OnTriggerEnter2D(Collider2D other) 
    { 
        if(other.CompareTag("Player")) playerInside = true; 
    }
    
    void OnTriggerExit2D(Collider2D other) 
    { 
        if(other.CompareTag("Player")) playerInside = false; 
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
           
            SimpleDoor.Instance.ChangeScene(sceneToLoad);
        }
    }
}