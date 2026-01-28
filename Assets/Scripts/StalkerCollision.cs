using UnityEngine;
using UnityEngine.SceneManagement; 

public class StalkerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
     
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}