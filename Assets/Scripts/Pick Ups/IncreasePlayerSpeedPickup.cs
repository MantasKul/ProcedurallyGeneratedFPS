using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerSpeedPickup : MonoBehaviour
{
    [SerializeField] private float bounceFrequency = 10f;
    [SerializeField] private float bounceIntensity = 0.005f;

    PlayerController playerController;

    private float healingAmount;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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
            playerController.increaseSpeed();
            Destroy(gameObject);
        }
    }
}
