using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the box
    private int currentHealth; // Current health of the box

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle what happens when the box's health reaches zero (e.g., destroy the box)
        Destroy(gameObject);
    }
}