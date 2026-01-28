using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDoor : MonoBehaviour
{
   
    public static SimpleDoor Instance;

    public Transform player;
    public SmoothCamera cams;
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    private void Awake() { Instance = this; }

   
    public void StartTransition(Transform targetSpawn, Vector2 start, Vector2 end, GameObject current, GameObject next)
    {
        StartCoroutine(DoTransition(targetSpawn, start, end, current, next));
    }

    IEnumerator DoTransition(Transform targetSpawn, Vector2 start, Vector2 end, GameObject current, GameObject next)
    {
      
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.unscaledDeltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

      
        player.position = targetSpawn.position;
        cams.setStart(start);
        cams.setEnd(end);

     
        next.SetActive(true);
        current.SetActive(false);

        yield return new WaitForSecondsRealtime(0.3f);

       
        while (alpha > 0)
        {
            alpha -= Time.unscaledDeltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
    }
}