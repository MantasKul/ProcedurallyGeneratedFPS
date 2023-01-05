using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    private WeaponManager weaponManager;

    void Start()
    {
        Destroy(gameObject, 60f);

        weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
    }

    public void Update()
    {
        speed *= weaponManager.getBulletSpeedMultiplier();
        damage *= weaponManager.getBulletDamageMultiplier();
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Objective" || other.gameObject.tag == "Enemy")
        {
        }
        else Destroy(gameObject);
    }

    public void setDamage(float amount)
    {
        damage = amount;
    }
    public void setSpeed(float amount)
    {
        speed = amount;
    }
}
