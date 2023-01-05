using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    [Header("Bullet Prefab and shootyPoints")]
    public GameObject bulletPrefab;
    public Transform leftGunEnd;
    public Transform rightGunEnd;

    [Header("Spread")]
    public float baseSpreadAmount = 2f;
    private float spreadAmount;
    private Vector3 offset;

    [Header("Ammo/Bullet variables")]
    public float baseBulletDamage = 3f;
    public float baseBulletSpeed = 50f;
    public int maxAmmo = 6;
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
        spreadAmount = baseSpreadAmount;

        currentAmmo = maxAmmo;
        playerCamera = GetComponentInParent<Camera>();

        weaponManager = GetComponentInParent<WeaponManager>();

        bulletSpeed = baseBulletSpeed + weaponManager.getBulletSpeedMultiplier();

        layerMask = ~layerMask;
    }

    private void Update()
    {
        offset = new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount));
        Shoot();
        Reload();

        weaponManager.setDamage(baseBulletDamage);
        weaponManager.setCurrentAmmo(currentAmmo);
        if(spreadAmount > 0)
            spreadAmount = baseSpreadAmount - weaponManager.getSpreadAmount();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && currentAmmo > 0)
        {
            currentAmmo--;
            nextFire = Time.time + fireRate;

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 5f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                // Vector3 added to spawn position so they wouldn't get destroyed when spawning in the same place because of the wonky ontriggerenter as spawning multiple objects in same spot destroys them even with tags
                // Instantiating 3 bullets for the left barrel
                GameObject leftBulletObject01 = Instantiate(bulletPrefab, leftGunEnd.transform.position + new Vector3(0f, 0.2f, 0f), leftGunEnd.transform.rotation);
                GameObject leftBulletObject02 = Instantiate(bulletPrefab, leftGunEnd.transform.position + new Vector3(-0.2f, 0f, 0f), leftGunEnd.transform.rotation);
                GameObject leftBulletObject03 = Instantiate(bulletPrefab, leftGunEnd.transform.position + new Vector3(0.2f, 0f, 0f), leftGunEnd.transform.rotation);
                leftBulletObject01.transform.forward = (hit.point - leftGunEnd.transform.position + offset);// - leftGunEnd.transform.position);// + offset);
                GetNewOffset();
                leftBulletObject02.transform.forward = (hit.point - leftGunEnd.transform.position + offset);// - leftGunEnd.transform.position);// + offset);
                GetNewOffset();
                leftBulletObject03.transform.forward = (hit.point - leftGunEnd.transform.position + offset);// - leftGunEnd.transform.position);// + offset);
                GetNewOffset();

                // Instantiating 3 bullets for the right barrel
                GameObject rightBulletObject01 = Instantiate(bulletPrefab, rightGunEnd.transform.position + new Vector3(0f, 0.2f, 0f), rightGunEnd.transform.rotation);
                GameObject rightBulletObject02 = Instantiate(bulletPrefab, rightGunEnd.transform.position + new Vector3(-0.2f, 0f, 0f), rightGunEnd.transform.rotation);
                GameObject rightBulletObject03 = Instantiate(bulletPrefab, rightGunEnd.transform.position + new Vector3(0.2f, 0f, 0f), rightGunEnd.transform.rotation);
                rightBulletObject01.transform.forward = (hit.point - rightGunEnd.transform.position + offset);// - rightGunEnd.transform.position);// + offset);
                GetNewOffset();
                rightBulletObject02.transform.forward = (hit.point - rightGunEnd.transform.position + offset);// - rightGunEnd.transform.position);// + offset);
                GetNewOffset();
                rightBulletObject03.transform.forward = (hit.point - rightGunEnd.transform.position + offset);// - rightGunEnd.transform.position);// + offset);
                GetNewOffset();
            }
            GameObject.FindWithTag("Bullet").GetComponent<Bullet>().setSpeed(bulletSpeed);
        }
    }

    private void GetNewOffset()
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
