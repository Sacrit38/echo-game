using System.Collections.Generic;
using UnityEngine;

public class AlleywayGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    public GameObject alleywayPrefab;
    public GameObject endLevelPrefab;
    public int totalLevelLength = 20;

    [Header("Configuration")]
    public Transform player;
    public Transform startPoint;
    public float tileWidth = 15f;
    public int bufferCount = 3;

    private float currentSpawnX;
    private int tilesSpawnedCount = 0;
    private List<GameObject> activeTiles = new List<GameObject>();
    private bool levelGenerated = false;

    void Start()
    {
        if (startPoint != null)
            currentSpawnX = startPoint.position.x;
        else
            currentSpawnX = 0;

        for (int i = 0; i < bufferCount; i++)
        {
            SpawnNextTile();
        }
    }

    void Update()
    {
        if (player == null) return;

        // SPAWNING LOGIC 
        if (!levelGenerated && activeTiles.Count > 0)
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];
            float lastTileX = lastTile.transform.position.x;

            if (player.position.x < (lastTileX + tileWidth * 2f))
            {
                SpawnNextTile();
            }
        }

        // DESPAWNING LOGIC
        if (activeTiles.Count > bufferCount)
        {
            GameObject oldestTile = activeTiles[0];
            if (player.position.x < (oldestTile.transform.position.x - tileWidth * 1.5f))
            {
                Destroy(oldestTile);
                activeTiles.RemoveAt(0);
            }
        }
    }

    void SpawnNextTile()
    {
        if (tilesSpawnedCount >= totalLevelLength)
        {
            if (!levelGenerated && endLevelPrefab != null)
            {
                SpawnEndObject();
            }
            levelGenerated = true;
            return;
        }

        Vector3 pos = new Vector3(currentSpawnX, 0, 0);
        GameObject newTile = Instantiate(alleywayPrefab, pos, Quaternion.identity, transform);

        activeTiles.Add(newTile);

        // Move the spawn point to the left
        currentSpawnX -= tileWidth;
        tilesSpawnedCount++;
    }

    void SpawnEndObject()
    {
        Vector3 pos = new Vector3(currentSpawnX + 7, 0, 0);
        Instantiate(endLevelPrefab, pos, Quaternion.identity, transform);
    }
}