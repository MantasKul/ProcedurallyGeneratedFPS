using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Health : MonoBehaviour
{
    public float health = 600f;

    private Transform spawnPoint;
    public GameObject tele;

    private void Start()
    {
        spawnPoint = GetComponent<Transform>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
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
}
