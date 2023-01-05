using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Todo
 * Get values from enemy bullet and use em instead of hardcodded
 * ^ what he said but for healing
 */

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    // Boosts
    private float healingBoost;

    public void Start()
    {
        currentHealth = maxHealth;

        maxHealth = 100f;
        healingBoost = 0f;
    }

    private void Update()
    {
        if (currentHealth <= 0)
            GameObject.FindWithTag("GameController").GetComponent<GameController>().GameOver();
    }

    public void TakeDamage(float damage)
    {
        if(transform.gameObject.GetComponent<PlayerController>().shieldEnabled == false)
            currentHealth -= damage;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    #region Getters and Setters
    public float getHealthAmount()
    {
        return currentHealth;
    }

    public float getIncreasedHealing()
    {
        return healingBoost;
    }
    public void increaseMaxHealth()
    {
        maxHealth = maxHealth + 10f;
    }
    #endregion
}
