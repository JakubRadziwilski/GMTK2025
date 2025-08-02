using UnityEngine;

public class BasicEnemy:Enemy
{
    // This class represents a basic enemy that targets the player and attaqcks him in meele range.
    [SerializeField] private int attackPower = 5;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float cooldownTime = 1f; // Time between attacks
    private float cooldownLeft = 0f; // Time left before the enemy can attack again
    private AbilityState attackState = AbilityState.Idle; // Current state of the enemy's ability

    [SerializeField] private float moveSpeed = 2f; // Speed at which the enemy moves towards the player
    public override void DecideAction()
    {
        if(player == null)
        {
            Debug.LogWarning("Player not found. Cannot perform action.");
            return;
        }
        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if(cooldownLeft <= 0f && attackState == AbilityState.Idle)
            {
                action = Attack; // Set action to attack
                cooldownLeft = cooldownTime; // Reset cooldown timer
            }
            else
            {
                cooldownLeft -= Time.deltaTime; // Decrease cooldown timer
                action = () => 
                {
                    // Do nothing while in cooldown
                    if (cooldownLeft <= 0f)
                    {
                        attackState = AbilityState.Idle; // Reset state when cooldown is over
                    }
                };
            }
        }
        else
        {
            action = MoveTowardsPlayer;
        }
    }

    private void Attack()
    {
        player.GetComponent<HealthStat>().TakeDamage(attackPower); // Deal damage to the player
    }

    private void MoveTowardsPlayer()
    {
        // Implement movement logic here, e.g., move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime; // Move towards the player
    }

    public override void Upgrade()
    {
        // Increase attack power and range as an example of upgrading
        attackPower += 1;
        moveSpeed += 0.5f; // Increase movement speed
    }
}
