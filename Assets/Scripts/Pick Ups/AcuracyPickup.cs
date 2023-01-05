using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcuracyPickup : MonoBehaviour
{
    [SerializeField] private float bounceFrequency = 10f;
    [SerializeField] private float bounceIntensity = 0.005f;

    public float increaseAmount = 0.1f;

    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        transform.Rotate(0f, 20f * Time.deltaTime, 0f, Space.Self);
        transform.position += Vector3.up * Mathf.Cos(Time.time * bounceFrequency) * bounceIntensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weaponManager.decreaseSpreadAmount(increaseAmount);
            Destroy(gameObject);
        }
    }
}
