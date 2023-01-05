using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlade : MonoBehaviour
{
    public float damage = 2f;
    public float health = 100f;
    public GameObject blade;

    private Vector3 destination;

    private NavMeshHit navHit;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetDestination", 0f, 10f);

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
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

    private void SetDestination()
    {
        Instantiate(blade, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);

        Vector3 randomDirection = Random.insideUnitSphere * 10f;

        randomDirection += gameObject.transform.position;        

        NavMesh.SamplePosition(randomDirection, out navHit, 10f, -1);

        destination = navHit.position;
    }
}
