using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollower : MonoBehaviour
{
    public float damage = 2f;
    public float health = 100f;

    private Vector3 destination;
    private Transform playerPos;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
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
}
