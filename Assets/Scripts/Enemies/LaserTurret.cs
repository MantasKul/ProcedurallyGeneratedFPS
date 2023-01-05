using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float health = 200f;
    public float turnSpeed = 25f;
    public GameObject head;

    private Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (Time.timeScale == 0) return;
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
