using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public HealthUI healthUI;
    public GameManager GM;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        // Flash red
        StartCoroutine(FlashRed());

        if(currentHealth <= 0)
        {
            // dead
            GM.PlayerDeath();
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
