using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy:Enemy
{
    public int healAmount = 10; // Amount of health to heal allies
    public float healCooldown = 5f; // Cooldown time for healing ability
    private float lastHealTime = 0f; // Time when the last heal was performed

    public override void DecideTarget()
    {
        List<Enemy> allies = GetComponentInParent<Room>().enemies; // Get all allies in the room

        if(allies.Count > 1) // Ensure there are allies to heal
        {
            //Target random ally from the list of allies
            int randomIndex = UnityEngine.Random.Range(0, allies.Count);
            currentTarget = allies[randomIndex].transform; // Set the current target to a random ally
        }
        else
        {
            currentTarget = player; // Default to player if no allies are available
        }
    }

    public override Action DecideAction()
    {
        if (currentTarget.TryGetComponent<Enemy>(out Enemy ally))
        {
            //if current target is an ally, heal them if cooldown allows
            if (Time.time > lastHealTime + healCooldown)
            {
                lastHealTime = Time.time; // Update the last heal time
                return Heal; // Return the heal action if cooldown is over
            }
            else
            {
                return null; // If on cooldown, return no action
            }
        }
        else
        {
             return base.DecideAction(); // Call the base class method to decide action
        }
    }

    void Heal()
    {
        currentTarget.GetComponent<HealthStat>().Heal(healAmount); // Heal the ally
    }

    public override void Upgrade()
    {
        healAmount += 5; // Increase heal amount on upgrade
        if(healCooldown > 2f) // Ensure cooldown does not go below 2 second
            healCooldown -= 0.5f; // Decrease cooldown time on upgrade
    }

}
