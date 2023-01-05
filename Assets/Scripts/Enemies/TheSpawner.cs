using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSpawner : MonoBehaviour
{
    public float health = 50f;
    public Transform spawnPoint;
    public List<GameObject> enemyToSpawn;

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 15f);
    }

    private void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    private void Spawn()
    {
        if (Time.timeScale == 0) return;
        int index = Random.Range(0, enemyToSpawn.Count);
        Instantiate(enemyToSpawn[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>().GetDamage();
            Destroy(other.gameObject);
        }
    }
}
