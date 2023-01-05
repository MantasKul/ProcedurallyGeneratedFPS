using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
     //Array of enemies to spawn
     public GameObject[] key;

    //Enemies will be spawned in random positions inside these boundaries
    //The minimum x and z position to spawn the enemies 
    public Vector2 minSpawnPos;
    //The maximum x and z position to spawn the enemies 
    public Vector2 maxSpawnPos;

    //The y position to spawn the enemies
    public float ySpawnPos = 1;
    //The time between enemy spawns
    public float spawnRate = 3f;
    //The minimum distance to spawn the last spawned enemy
    public float minSpawnDistance = 2f;

    Vector3 lastSpawnPosition;
    float lastSpawn;

    private void Start()
    {
        //Time check to see if it's time to spawn a new enemy
        
        
            SpawnEnemy();
        
    }
    private void SpawnEnemy()
    {
        for (int x = 0; x < 5; x++) { 
        Vector3 spawnPosition;
        //Selects a random enemy to spawn from the array of enemies
        int enemySpawnIndex = Random.Range(0, key.Length);

        //Will keep on generating a new spawn position until it's far enough away from the last one
        do
        {
            spawnPosition = new Vector3(Random.Range(transform.position.x + minSpawnPos.x, transform.position.x + maxSpawnPos.x), transform.position.y + ySpawnPos, Random.Range(transform.position.z + minSpawnPos.y, transform.position.z + maxSpawnPos.y));
        } while (Vector3.Distance(spawnPosition, lastSpawnPosition) < minSpawnDistance);

        //Spawns a new instance of an enemy
        GameObject instance = Instantiate(key[enemySpawnIndex], spawnPosition, Quaternion.identity, transform.parent);


        //"Resets" the spawn timer
        lastSpawn = Time.time;
        //Sets this enemies position as the last spawned enemy
        lastSpawnPosition = spawnPosition;
    }
    }
}
