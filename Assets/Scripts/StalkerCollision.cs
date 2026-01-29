using UnityEngine;

public class StalkerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            GameOverHandler handler = FindObjectOfType<GameOverHandler>(true);
            if (handler != null)
            {
                handler.SetupGameOver();
            }
            else
            {
             
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            }
        }
    }
}