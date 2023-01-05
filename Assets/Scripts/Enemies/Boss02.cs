using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss02 : MonoBehaviour
{
    public float health = 400f;
    public float bladeSpawnRate = 5f;
    private float timeToSpawn;

    public GameObject blade;

    private Vector3 destination;
    private NavMeshHit navHit;
    NavMeshAgent agent;

    private Vector3 playerPos;

    private Transform spawnPoint;
    public GameObject tele;
    void Start()
    {
        spawnPoint = GetComponent<Transform>();
        InvokeRepeating("getAndSpawn", 0f, 5f);
        agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (transform.position == destination)
            SetDestination();
        Debug.Log(playerPos);
        agent.destination = destination;

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
        Instantiate(tele, spawnPoint);
        Destroy(gameObject);
    }

    private void SetDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;

        randomDirection += gameObject.transform.position;

        NavMesh.SamplePosition(randomDirection, out navHit, 10f, -1);

        destination = navHit.position;
    }

    private void getAndSpawn()
    {
        Instantiate(blade, playerPos, Quaternion.Euler(0f,0f,0f));
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }
}
