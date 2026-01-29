using UnityEngine;

public class PlayerSceneInitializer : MonoBehaviour
{
    void Start()
    {
     
        if (SimpleDoor.Instance != null)
        {
            SimpleDoor.Instance.player = this.transform;
            
         
            SimpleDoor.Instance.cams = Camera.main.GetComponent<SmoothCamera>();
        }
    }
}