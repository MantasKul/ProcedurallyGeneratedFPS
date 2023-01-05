using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public List<GameObject> pickups;
    public Transform spawnPoint;

    private void Start()
    {
        int spawn = Random.Range(0, 3);
        Instantiate(pickups[spawn], spawnPoint.position, spawnPoint.rotation, spawnPoint);
    }
}
