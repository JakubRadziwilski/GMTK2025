using UnityEngine;
using System;

public class BasicEnemy:Enemy
{

    public override void Upgrade()
    {
        base.Upgrade(); // Call the base upgrade method to maintain any inherited functionality
        // Increase attack power and range as an example of upgrading
        attackDamage += 1;
        Speed += 0.5f; // Increase movement speed
    }
}
