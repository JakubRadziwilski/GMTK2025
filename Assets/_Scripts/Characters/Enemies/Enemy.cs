using System;
using UnityEngine;

public enum AbilityState
{
    Idle,
    Active,
    Cooldown
}
[RequireComponent(typeof(HealthStat))] // Ensure the enemy has a CharacterStats component
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxInteractionDistance = 20f; // Maximum distance at which the enemy can interact with the player
    
    protected Transform player; // Reference to the player's transform
    protected Transform support;  // Reference to the support NPC's transform


    protected Action action; // Action to be performed by the enemy
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        support = GameObject.FindGameObjectWithTag("Support").transform; // Find the support NPC by tag
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > maxInteractionDistance)
        {
            // If the player is too far away, do nothing
            return;
        }
        DecideAction();
        action.Invoke(); // Invoke the action to be performed
    }


    public virtual void DecideAction()
    {
        // Default behavior: do nothing
    }

    public virtual void Upgrade()
    {
        // Default behavior: do nothing
        Debug.LogWarning("Upgrade method not implemented in " + gameObject.name);

    }
}
