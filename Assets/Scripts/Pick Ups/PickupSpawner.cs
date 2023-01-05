using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public List<GameObject> pickups;
    public Transform spawnPoint;

    private void Start()
    {
        if (Random.Range(0f, 1f) <= 0.8f)
            Instantiate(pickups[0], spawnPoint.position, spawnPoint.rotation, spawnPoint);
        else
        {
            int spawn = Random.Range(1, 6);
            Instantiate(pickups[spawn], spawnPoint.position, spawnPoint.rotation, spawnPoint);
        }
    }
}
