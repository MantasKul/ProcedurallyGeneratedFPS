using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01 : MonoBehaviour
{
    public float health = 300f;

    public GameObject laser1;
    public GameObject laser2;

    public GameObject head1;
    public GameObject head2;
    public GameObject head3;
    public GameObject head4;

    public GameObject bullet;

    private float nextShot1;
    private float nextShot2;
    private float nextShot3;
    private float nextShot4;
    public float fireRate = 2f;

    public GameObject shootingPoint1;
    private RaycastHit hit1;
    public GameObject shootingPoint2;
    private RaycastHit hit2;
    public GameObject shootingPoint3;
    private RaycastHit hit3;
    public GameObject shootingPoint4;
    private RaycastHit hit4;

    private Transform playerPos;

    private Transform spawnPoint;
    public GameObject tele;

    private void Start()
    {
        spawnPoint = GetComponent<Transform>();
        health = 120f;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        laser1.transform.Rotate(0f, 60f * Time.deltaTime, 0f, Space.Self);
        laser2.transform.Rotate(0f, -60f * Time.deltaTime, 0f, Space.Self);

        head1.transform.LookAt(playerPos);
        head2.transform.LookAt(playerPos);
        head3.transform.LookAt(playerPos);
        head4.transform.LookAt(playerPos);

        if (Physics.Raycast(shootingPoint1.transform.position, shootingPoint1.transform.forward, out hit1, Mathf.Infinity))
        {
            if (hit1.collider.gameObject.tag == "Player" && Time.time > nextShot1)
            {
                nextShot1 = Time.time + fireRate;
                Instantiate(bullet, shootingPoint1.transform.position, shootingPoint1.transform.rotation);
            }
        }
        if (Physics.Raycast(shootingPoint2.transform.position, shootingPoint2.transform.forward, out hit2, Mathf.Infinity))
        {
            if (hit2.collider.gameObject.tag == "Player" && Time.time > nextShot2)
            {
                nextShot2 = Time.time + fireRate;
                Instantiate(bullet, shootingPoint2.transform.position, shootingPoint2.transform.rotation);
            }
        }
        if (Physics.Raycast(shootingPoint3.transform.position, shootingPoint3.transform.forward, out hit3, Mathf.Infinity))
        {
            if (hit3.collider.gameObject.tag == "Player" && Time.time > nextShot3)
            {
                nextShot3 = Time.time + fireRate;
                Instantiate(bullet, shootingPoint3.transform.position, shootingPoint3.transform.rotation);
            }
        }
        if (Physics.Raycast(shootingPoint4.transform.position, shootingPoint4.transform.forward, out hit4, Mathf.Infinity))
        {
            if (hit4.collider.gameObject.tag == "Player" && Time.time > nextShot4)
            {
                nextShot4 = Time.time + fireRate;
                Instantiate(bullet, shootingPoint4.transform.position, shootingPoint4.transform.rotation);
            }
        }

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
