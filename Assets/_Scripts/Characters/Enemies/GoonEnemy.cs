using UnityEngine;
using System;

public class GoonEnemy: Enemy
{

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
