using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaserWanderer : MonoBehaviour
{
    public float health = 200f;

    // Movement
    private Vector3 destination;
    private NavMeshHit navHit;
    NavMeshAgent agent;


    void Start()
    {
        InvokeRepeating("SetDestination", 0f, 10f);

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
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
        Destroy(gameObject);
    }

    private void SetDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;

        randomDirection += gameObject.transform.position;

        NavMesh.SamplePosition(randomDirection, out navHit, 10f, -1);

        destination = navHit.position;
    }
}
