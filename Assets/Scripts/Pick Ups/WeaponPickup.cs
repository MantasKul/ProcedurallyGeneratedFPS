using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private float bounceFrequency = 10f;
    [SerializeField] private float bounceIntensity = 0.005f;


    public GameObject weapon;

    public GameObject GetReference()
    {
        return weapon;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        transform.Rotate(0f, 20f * Time.deltaTime, 0f, Space.Self);
        transform.position += Vector3.up * Mathf.Cos(Time.time * bounceFrequency) * bounceIntensity;
    }
}
