using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Todo
 * Add animation to reloading, delay to not be able to shoot while reloading
 */

public class PistolController : MonoBehaviour
{
    [Header("Bullet Prefab and shootyPoints")]
    public GameObject bulletPrefab;
    public Transform gunEnd;

    [Header("Ammo/Bullet variables")]
    public float baseBulletDamage = 5f;
    public float baseBulletSpeed = 30f;
    public int maxAmmo = 10;
    public float fireRate = 0.25f;
    private Bullet bulletScript;
    private float bulletSpeed;

    private Camera playerCamera;
    private float nextFire;
    private int currentAmmo;

    private int layerMask = 1 << 8;

    // for showing current ammo in UI
    private WeaponManager weaponManager;

    private void Start()
    {
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

        weaponManager.setDamage(baseBulletDamage);
        bulletSpeed = baseBulletSpeed + weaponManager.getBulletSpeedMultiplier();
    }

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextFire && currentAmmo > 0)
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

                if (Vector3.Distance(hit.point, rayOrigin) <= 2)
                    bulletObject.transform.forward = ((hit.point + Vector3.forward*2) - gunEnd.transform.position).normalized;
                else
                    bulletObject.transform.forward = (hit.point - gunEnd.transform.position).normalized;
            }
            else
            {
                Instantiate(bulletPrefab, gunEnd.transform.position, gunEnd.transform.rotation);
            }
            GameObject.FindWithTag("Bullet").GetComponent<Bullet>().setSpeed(bulletSpeed);
        }
    }

    private void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo < maxAmmo)
                currentAmmo = maxAmmo;
        }
    }
}
