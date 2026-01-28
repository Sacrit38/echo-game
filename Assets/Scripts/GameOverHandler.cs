using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Button restartButton;
    public GameObject gameOverPanel;

    public void SetupGameOver()
    {
        
        gameOverPanel.SetActive(true);
        
       
        string currentLevel = SceneManager.GetActiveScene().name;
        string dialog = "";

        switch (currentLevel)
        {
            case "Chapter I":
                dialog = "“Suara apa itu? Kenapa dia terus mengikutiku?”";
                break;
            case "Chapter 2":
                dialog = "“Kau tahu kau membutuhkanku... Biarkan aku ada di sini untukmu.”";
                break;
            case "Gameplay":
                dialog = "“gokgokgok kalah”";
                break;
            case "Level4":
                dialog = "“Kau aman bersamaku sekarang.”";
                break;
            case "Level5":
                dialog = "“... Dan aku tidak akan pernah meninggalkan sisimu, selamanya.”";
                break;
            default:
                dialog = "“Aku tidak bisa melarikan diri...”";
                break;
        }

        dialogText.text = dialog;


        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartLevel);

       
        Time.timeScale = 0f; 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}