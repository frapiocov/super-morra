using System;
using UnityEngine;

public class LifePoint : MonoBehaviour, Item
{
    public HealthUI healthUI;
    public PlayerHealth playerHealth;
    public void Collect()
    {
        int currentHealth = playerHealth.GetHealth();
        if (currentHealth < 3) 
        {
            healthUI.UpdateHearts(currentHealth + 1);
        }
        Destroy(gameObject);
    }
}
