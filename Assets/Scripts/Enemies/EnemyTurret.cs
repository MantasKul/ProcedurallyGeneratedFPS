using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public float health = 120f;

    private Transform playerPos;
    public GameObject bullet;

    private float nextShot;
    public float fireRate = 2f;

    // Raycast variables
    public GameObject shootingPoint;
    private RaycastHit hit;

    public GameObject head;

    private void Start()
    {
        health = 120f;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        head.transform.LookAt(playerPos);

        if(Physics.Raycast(shootingPoint.transform.position, shootingPoint.transform.forward, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject.tag == "Player" && Time.time > nextShot)
            {
                nextShot = Time.time + fireRate;
                Instantiate(bullet, shootingPoint.transform.position, shootingPoint.transform.rotation);
            }
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
}
