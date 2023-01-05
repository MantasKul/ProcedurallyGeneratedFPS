using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> eliteEnemies = new List<GameObject>();

    public void Start()
    {
        int index = Random.Range(0, enemies.Count);
        int eliteIndex = Random.Range(0, eliteEnemies.Count);

        if (Random.Range(0f, 1f) <= 0.8f)
            Instantiate(enemies[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
        else
            Instantiate(eliteEnemies[eliteIndex], spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
