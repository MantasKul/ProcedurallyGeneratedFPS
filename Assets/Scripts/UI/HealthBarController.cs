using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;

    private PlayerHealth playerHealth;

    public void Start()
    {
        healthBar = GetComponent<Image>();

        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        healthBar.fillAmount = 1;
    }

    public void Update()
    {
        healthBar.fillAmount = playerHealth.getHealthAmount() / 100f;
    }
}
