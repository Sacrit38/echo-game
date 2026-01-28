using UnityEngine;

public class LaurentTrigger : MonoBehaviour
{
    public LaurentEvent laurentScript;
// tambahin ke gameobject yg dikasih collider 2d utk ngetrigger laurent nya jalan ke mc

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            laurentScript.StartApproaching();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}