using UnityEngine;
using System;

public class BasicEnemy:Enemy
{

    public override void Upgrade()
    {
        // Increase attack power and range as an example of upgrading
        attackDamage += 1;
        Speed += 0.5f; // Increase movement speed
    }
}
