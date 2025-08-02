using UnityEngine;
using System;

public class GoonEnemy: Enemy
{
    public override void DecideTarget()
    {
        // GoonEnemy has a specific target selection logic
        if (Vector3.Distance(support.position, transform.position) < Vector3.Distance(player.position, transform.position))
        {
            currentTarget = support; // If the support is closer, target the support
        }
        else
        {
            currentTarget = player; // Otherwise, target the player
        }
    }

    public override void Upgrade()
    {

        base.Upgrade(); // Call the base upgrade method to maintain any inherited functionality
        Speed += 1f; // Increase speed
        if(attackCooldown>= 0.5f)
        {
            attackCooldown -= 0.1f; // Decrease cooldown time
        }
    }
}
