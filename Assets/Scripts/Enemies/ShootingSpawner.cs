using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingSpawner : MonoBehaviour
{
    public float damage = 4f;
    public float health = 130f;
    public GameObject head;
    public Transform spawnPoint;
    public List<GameObject> enemyToSpawn;

    private Vector3 destination;
    private Transform playerPos;
    NavMeshAgent agent;

    // Raycast variables
    public GameObject shootingPoint;
    private RaycastHit hit;

    private float nextShot;
    public float fireRate = 2f;

    public GameObject bullet;
    void Start()
    {
        InvokeRepeating("Spawn", 0f, 15f);
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        head.transform.LookAt(playerPos);

        if (Physics.Raycast(shootingPoint.transform.position, shootingPoint.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Player" && Time.time > nextShot)
            {
                nextShot = Time.time + fireRate;
                Instantiate(bullet, shootingPoint.transform.position, shootingPoint.transform.rotation);
            }
        }

        if (Vector3.Distance(destination, playerPos.position) > 1.0f)
        {
            destination = playerPos.position;
            agent.destination = destination;
        }

        if (health <= 0)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>().GetDamage();
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    private void Spawn()
    {
        int index = Random.Range(0, enemyToSpawn.Count);
        Instantiate(enemyToSpawn[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
