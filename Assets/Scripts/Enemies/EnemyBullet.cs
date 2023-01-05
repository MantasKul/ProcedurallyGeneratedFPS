using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float damage;

    void Start()
    {
        Destroy(gameObject, 60f);
    }

    public void Update()
    {
        if (Time.timeScale == 0) return;
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Objective")
        {
        }
        else Destroy(gameObject);
    }
}
