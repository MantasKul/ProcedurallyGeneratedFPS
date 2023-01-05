using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boos03Prot : MonoBehaviour
{
    public float health = 50f;

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
        gameObject.SetActive(false);
    }
}
