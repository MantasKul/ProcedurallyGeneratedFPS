using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaserFollower : MonoBehaviour
{
    public float health = 200f;
    public float turnSpeed = 10f;
    public GameObject head;

    private Transform playerPos;
    private Vector3 destination;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        if (Vector3.Distance(destination, playerPos.position) > 1.0f)
        {
            destination = playerPos.position;
            agent.destination = destination;
        }

        var step = turnSpeed * Time.deltaTime;
        Quaternion target = Quaternion.LookRotation(playerPos.position - head.transform.position);
        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, target, step);

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
}
