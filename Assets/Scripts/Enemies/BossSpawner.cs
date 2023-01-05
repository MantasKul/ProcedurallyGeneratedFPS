using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject boss;

    public void Start()
    {
        Instantiate(boss, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
