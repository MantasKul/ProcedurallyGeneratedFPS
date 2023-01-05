using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWandererLaser : MonoBehaviour
{
    public float damage = 1f;

    private RaycastHit hit;
    private LineRenderer laser;

    private PlayerHealth playerHealth;

    public void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        laser = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        laser.SetPosition(0, transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            laser.SetPosition(1, hit.point);
            laser.enabled = true;

            if (hit.rigidbody.gameObject.tag == "Player")
               playerHealth.TakeDamage(damage);
        }
    }
}
