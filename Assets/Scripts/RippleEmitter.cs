using UnityEngine;

public class RippleEmitter : MonoBehaviour
{
    public GameObject ripplePrefab;

    public void EmitRipple(string colorTag)
    {
      
        GameObject ripple = Instantiate(ripplePrefab, transform.position, Quaternion.identity);
        
        
        Animator anim = ripple.GetComponent<Animator>();

        // 3. Mainkan animasi berdasarkan tag warna
        if (colorTag == "White") anim.SetTrigger("playWhite");
        else if (colorTag == "Red") anim.SetTrigger("playRed");
        else if (colorTag == "Green") anim.SetTrigger("playGreen");
        else if (colorTag == "Yellow") anim.SetTrigger("playYellow");

       
        Destroy(ripple, 1.0f);
    }
}