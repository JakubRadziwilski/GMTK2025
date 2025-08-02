using System;
using UnityEditor.Compilation;
using UnityEngine;

[RequireComponent(typeof(HealthStat))] // Ensure the enemy has a CharacterStats component
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxhealth = 100; // Health of the enemy
    public int difficultyLevel = 1; // Difficulty level of the enemy, used for scaling difficulty
    [SerializeField] protected int attackDamage = 10; // Damage dealt by the enemy
    [SerializeField] protected float attackRange = 5f; // Range within which the enemy can attack the player
    [SerializeField] protected float attackCooldown = 1f; // Cooldown time between attacks
    protected float lastAttackTime = 0f; // Time when the last attack was made
    [SerializeField] protected float Speed = 2f; // Movement speed of the enemy

    bool playerInThisRoom = false; // Flag to check if the player is in the same room as the enemy

    protected Transform player; // Reference to the player's transform
    protected Transform support;  // Reference to the support NPC's transform
    protected Transform currentTarget; // Current target for the enemy to interact with


    protected Action action; // Action to be performed by the enemy
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerStats.Instance.onRunStart += SetUpMaxHP; // Initialize the health stat with max health
        SetUpMaxHP(); // Set up the maximum health for the enemy
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
    }

    void Update()
    {
        if (playerInThisRoom == false)
        {
            return;
        }
        else
        {
            DecideTarget(); // Decide the target to interact with
            action = DecideAction();
            action?.Invoke(); // Invoke the action to be performed
        }
}

    public virtual void DecideTarget()
    {
        currentTarget = player; // Default target is the player
    }
    public virtual Action DecideAction()
    {
        if (Vector3.Distance(currentTarget.position,transform.position)< attackRange)
        {
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                return Atack; // If within attack range and cooldown is over, return the attack action
            }
            else
            {
                return null; // If within attack range but on cooldown, return no action
            }
        }
        else
        {
            return FollowTarget; // Otherwise, return the follow action
        }
    }

    public virtual void FollowTarget()
    {
        // Default follow behavior: move towards the current target
        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            transform.position += Speed * Time.deltaTime * direction;
        }
        else
        {
            Debug.LogWarning("No target to follow for " + gameObject.name);
        }
    }
    public virtual void Atack()
    {
        if (currentTarget != null)
        {
            lastAttackTime = Time.time; // Update the last attack time
            currentTarget.GetComponent<HealthStat>().TakeDamage(attackDamage); // Deal damage to the target
        }
        else
        {
            Debug.LogWarning("No target to attack for " + gameObject.name);
        }
    }
    public virtual void Upgrade()
    {
        difficultyLevel++; // Increase the difficulty level of the enemy
    }

    public void Respawn()
    {
        // Respawn the enemy by resetting its health and position
        Upgrade(); // Upgrade the enemy if necessary
        GetComponent<HealthStat>().SetMaxHP(maxhealth); // Reset health to max
        SetPlayerInThisRoom(true); // Ensure the player is in the room
        gameObject.SetActive(true); // Reactivate the enemy GameObject
    }

    public void SetPlayerInThisRoom(bool value)
    {
        playerInThisRoom = value; // Set the flag indicating if the player is in the same room
    }
    public void SetUpMaxHP()
    {
        GetComponent<HealthStat>().SetMaxHP(maxhealth); // Set the maximum health of the enemy
    }
}
