using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    [Header("Bullet Prefab and shootyPoints")]
    public GameObject bulletPrefab;
    public Transform gunEnd;

    [Header("Spread")]
    public float baseSpreadAmount = 0.5f;
    public float spreadAmount;
    private Vector3 offset;

    [Header("Ammo/Bullet variables")]
    public float baseBulletDamage = 3f;
    public float baseBulletSpeed = 40f;
    public int maxAmmo = 30;
    public float fireRate = 0.25f;
    private float bulletSpeed;

    private Camera playerCamera;
    private float nextFire;
    private int currentAmmo;

    private int layerMask = 1 << 8;

    // for showing current ammo in UI
    private WeaponManager weaponManager;

    private void Start()
    {
        spreadAmount = baseSpreadAmount;

        currentAmmo = maxAmmo;
        playerCamera = GetComponentInParent<Camera>();

        weaponManager = GetComponentInParent<WeaponManager>();

        layerMask = ~layerMask;
    }

    private void Update()
    {
        Shoot();
        Reload();

        weaponManager.setCurrentAmmo(currentAmmo);
        if(spreadAmount > 0)
            spreadAmount = baseSpreadAmount - weaponManager.getSpreadAmount();

        weaponManager.setDamage(baseBulletDamage);
        bulletSpeed = baseBulletSpeed + weaponManager.getBulletSpeedMultiplier();
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire && currentAmmo > 0)
        {
            currentAmmo--;
            nextFire = Time.time + fireRate;

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, Mathf.Infinity, layerMask))
            {                
                GameObject bulletObject = Instantiate(bulletPrefab, gunEnd.transform.position, gunEnd.transform.rotation);
                bulletObject.transform.forward = (hit.point - gunEnd.transform.position + offset);
                SetOffset();
            }
            else
            {
                /*GameObject bulletObject = */Instantiate(bulletPrefab, gunEnd.transform.position, gunEnd.transform.rotation);
                //bulletObject.transform.forward = gunEnd.transform.position + offset;
            }
            GameObject.FindWithTag("Bullet").GetComponent<Bullet>().setSpeed(bulletSpeed);
        }
    }

    public void SetOffset()
    {
        offset = new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount));
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo < maxAmmo)
                currentAmmo = maxAmmo;
        }
    }
}
