using UnityEngine;

public class ObstacleCuller : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform obstacleContainer;

    [Header("Settings")]
    public float cullingDistance = 20f;

    void Update()
    {
        if (player == null || obstacleContainer == null) return;

        foreach (Transform obstacle in obstacleContainer)
        {
            float distance = Mathf.Abs(obstacle.position.x - player.position.x);

            // Enable if close, disable if far
            bool shouldBeActive = distance <= cullingDistance;

            if (obstacle.gameObject.activeSelf != shouldBeActive)
            {
                obstacle.gameObject.SetActive(shouldBeActive);
            }
        }
    }
}