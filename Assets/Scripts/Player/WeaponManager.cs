using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponSocket;
    private GameObject weaponPrefab;

    private int currentAmmo;

    public float damage;

    // Boosts
    private float bulletSpeedMultiplier;
    private float bulletDamageMultiplier;
    private float spreadAmount;

    private void Start()
    {
        bulletDamageMultiplier = 1f;
        bulletSpeedMultiplier = 1f;
        spreadAmount = 0f;
    }

    public void AddWeapon()
    {
        Instantiate(weaponPrefab, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WeaponPickup")
        {
            if (weaponSocket.childCount > 0)
            {
                Destroy(weaponSocket.GetChild(0).gameObject);
            }
            weaponPrefab = other.GetComponent<WeaponPickup>().GetReference();
            Destroy(other.gameObject);
            AddWeapon();
        }
    }

    // Setters Getters
    public void setCurrentAmmo(int ammo)
    {
        currentAmmo = ammo;
    }
    public int getCurrentAmmo()
    {
        return currentAmmo;
    }

    public void increaseBulletSpeedMultiplier(float amount)
    {
        bulletSpeedMultiplier += amount;
    }
    public float getBulletSpeedMultiplier()
    {
        return bulletSpeedMultiplier;
    }

    public void increaseBulletDamageMultiplier(float amount)
    {
        bulletDamageMultiplier += amount;
    }
    public float getBulletDamageMultiplier()
    {
        return bulletDamageMultiplier;
    }

    public float getSpreadAmount()
    {
        return spreadAmount;
    }
    public void decreaseSpreadAmount(float amount)
    {
        spreadAmount += amount;
    }

    public void setDamage(float amount)
    {
        damage = bulletDamageMultiplier * amount;
    }
    public float GetDamage()
    {
        return damage;
    }
}
