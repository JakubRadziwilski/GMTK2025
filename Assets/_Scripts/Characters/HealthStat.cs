using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))] // Ensure the HealthStat component has a Collider for interaction detection
public class HealthStat: MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public StatsDisplay statDisplay;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        if (statDisplay == null)
        {
            Debug.LogError("StatsDisplay component is not assigned in the inspector for " + gameObject.name);
        }
        else
        {
            statDisplay.UpdateStatDisplay(currentHealth, 0); // Initialize the stat display
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        statDisplay.UpdateStatDisplay(currentHealth, currentHealth - amount);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the game object if health is zero or less
        }
        statDisplay.UpdateStatDisplay(currentHealth, currentHealth + amount);
    }
}
