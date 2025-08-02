using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))] // Ensure the HealthStat component has a Collider for interaction detection
public class HealthStat : MonoBehaviour
{
    protected int maxHealth = 100;
    protected int currentHealth;

    public StatsDisplay statDisplay;

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        statDisplay.UpdateStatDisplay(currentHealth, currentHealth - amount);
    }
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); // Deactivate the GameObject when health reaches zero
        }
        statDisplay.UpdateStatDisplay(currentHealth, currentHealth + amount);
    }

    public void SetMaxHP(int amount)
    {
        currentHealth = amount; // Set the current health to the maximum health
        maxHealth = amount;
        statDisplay.UpdateStatDisplay(maxHealth, 0); // Initialize the stat display
    }

    public void IncreaseMaxHp(int amount)
    {
        SetMaxHP(maxHealth + amount);
    }
}
