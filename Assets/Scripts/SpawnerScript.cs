using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Platforms to spawn (previously 'vetor')
    public GameObject[] platformPrefabs;

    // NEW: Collectibles to potentially spawn on top of the platform
    public GameObject[] collectiblePrefabs;

    // NEW: Chance (as a percentage 0-100) for a collectible to spawn
    [Range(0, 100)]
    public int collectibleSpawnChance = 50;

    public float spawnMin;
    public float spawnMax;
    public float spawnHeightOffset = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        // 1. --- Spawn the Platform ---

        // Pick a random platform
        GameObject platformToSpawn = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        // Calculate the base spawn position
        Vector3 platformSpawnPosition = transform.position;
        // The Y-offset from the spawner's transform is applied here. 
        // Note: For platforms, you usually want the offset to be relative 
        // to the spawner's X/Z location, but with the Y-coordinate determined by the game logic.
        // If your spawner is at Y=0 and you want platforms at Y=2, set spawnHeightOffset to 2.0f.
        platformSpawnPosition.y += spawnHeightOffset;

        // Instantiate the platform
        GameObject newPlatform = Instantiate(platformToSpawn, platformSpawnPosition, Quaternion.identity);


        // 2. --- Attempt to Spawn a Collectible on the Platform ---

        // Check if there are any collectibles defined AND if the chance passes
        if (collectiblePrefabs.Length > 0 && Random.Range(0, 100) < collectibleSpawnChance)
        {
            // Pick a random collectible
            GameObject collectibleToSpawn = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];

            // Calculate the collectible's position
            // We use the new platform's position, then add the height of the platform
            // and half the height of the collectible to place it on top.
            Vector3 collectibleSpawnPosition = newPlatform.transform.position;

            // This is a basic way to find the top of the platform.
            // You may need to adjust this depending on the pivot points/colliders of your prefabs.
            // Assuming platform height is roughly 1 unit and collectible height is 1 unit:
            collectibleSpawnPosition.y += 1.0f; // Adjust this value in the Inspector as needed

            // Instantiate the collectible
            Instantiate(collectibleToSpawn, collectibleSpawnPosition, Quaternion.identity);
        }


        // 3. --- Schedule the next spawn ---
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}