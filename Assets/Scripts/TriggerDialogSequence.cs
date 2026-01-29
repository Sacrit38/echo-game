using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // Penting untuk fungsi pindah scene

public class TriggerDialogToScene1 : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject dialogPanel;
    public CanvasGroup dialogCanvasGroup;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    
    [Header("Dialog Settings")]
    public string charName = "Odele";
    [TextArea(3, 10)]
    public string[] dialogLines;
    public float typingSpeed = 0.05f;
    public float fadeSpeed = 1.5f;

    [Header("Player Reference")]
    public MonoBehaviour playerMovement;

    [Header("Ending Music")]
    public AudioClip endingBGM;
    private AudioSource bgmAudioSource;

    private bool hasTriggered = false;

    void Awake()
    {
        if (dialogPanel != null) dialogPanel.SetActive(false);
        if (dialogCanvasGroup != null) dialogCanvasGroup.alpha = 0;
        
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(StartDialogSequence());
        }
    }

    IEnumerator StartDialogSequence()
    {
        // 1. Matikan kontrol player
        if (playerMovement != null) playerMovement.enabled = false;

        // 2. Mainkan musik
        if (endingBGM != null && bgmAudioSource != null)
        {
            bgmAudioSource.clip = endingBGM;
            bgmAudioSource.Play();
        }

        // 3. Munculkan Panel (Fade In)
        dialogPanel.SetActive(true);
        nameText.text = charName;
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 0, 1));

        // 4. Proses Dialog Baris demi Baris
        foreach (string line in dialogLines)
        {
            yield return StartCoroutine(TypeText(line));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return null; 
        }

        // 5. Tutup Panel (Fade Out)
        yield return StartCoroutine(FadeCanvas(dialogCanvasGroup, 1, 0));
        dialogPanel.SetActive(false);

        // 6. LANGSUNG LOAD SCENE INDEX 1
        // Pastikan di Build Settings, scene tujuan ada di urutan nomor 1
        SceneManager.LoadScene(0);
    }

    IEnumerator TypeText(string line)
    {
        messageText.text = "";
        foreach (char c in line.ToCharArray())
        {
            messageText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float start, float end)
    {
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime * fadeSpeed;
            cg.alpha = Mathf.Lerp(start, end, timer);
            yield return null;
        }
        cg.alpha = end;
    }
}