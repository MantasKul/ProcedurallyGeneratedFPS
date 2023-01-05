using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float damage = 20f;
    public float rotationSpeed = 50f;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void Update()
    {
        if (Time.timeScale == 0) return;
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
