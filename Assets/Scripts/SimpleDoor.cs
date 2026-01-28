using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleDoor : MonoBehaviour
{
    public static SimpleDoor Instance;
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    [Header("References (Optional for Scene Move)")]
    public Transform player;
    public SmoothCamera cams;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            if (fadeImage != null && fadeImage.canvas != null) 
                DontDestroyOnLoad(fadeImage.canvas.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

  
    public void StartTransition(Transform targetSpawn, Vector2 start, Vector2 end, GameObject current, GameObject next)
    {
        StartCoroutine(DoRoomTransition(targetSpawn, start, end, current, next));
    }

    IEnumerator DoRoomTransition(Transform targetSpawn, Vector2 start, Vector2 end, GameObject current, GameObject next)
    {
        yield return StartCoroutine(Fade(1)); // Gelap

        player.position = targetSpawn.position;
        if (cams != null) {
            cams.setStart(start);
            cams.setEnd(end);
        }
        if(next != null) next.SetActive(true);
        if(current != null) current.SetActive(false);

        yield return new WaitForSecondsRealtime(0.3f);
        yield return StartCoroutine(Fade(0)); // Terang
    }

   
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(DoSceneTransition(sceneName));
    }

    IEnumerator DoSceneTransition(string sceneName)
    {
        yield return StartCoroutine(Fade(1)); // Gelap
        SceneManager.LoadScene(sceneName);
       
        yield return new WaitForSecondsRealtime(0.5f);
        yield return StartCoroutine(Fade(0)); // Terang
    }

    IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null) yield break;
        float startAlpha = fadeImage.color.a;
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.unscaledDeltaTime * fadeSpeed;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer);
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}