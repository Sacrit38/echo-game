using UnityEngine;

public class GroundType : MonoBehaviour
{

    public string groundType = "concrete"; // "concrete" or "wood"

    void Start()
    {
        Debug.Log("RoomGroundSetter trying to set: " + groundType);
        BasicPlayerMovement player = FindObjectOfType<BasicPlayerMovement>();
        if (player != null)
        {
            Debug.Log("Player found! Setting ground type.");
            player.SetGroundType(groundType);
        }
        else
        {
            Debug.Log("ERROR: Player not found!"); // ADD THIS
        }
    }
}
