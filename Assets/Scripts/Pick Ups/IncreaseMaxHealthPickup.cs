using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealthPickup : MonoBehaviour
{
    [SerializeField] private float bounceFrequency = 10f;
    [SerializeField] private float bounceIntensity = 0.005f;

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
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
            playerHealth.increaseMaxHealth();
            Destroy(gameObject);
        }
    }
}
