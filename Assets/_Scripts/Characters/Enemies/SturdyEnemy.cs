using UnityEngine;
using System;
using Unity.VisualScripting;

public class SturdyEnemy : Enemy
{
    public override void Upgrade()
    {
        base.Upgrade(); // Call the base Upgrade method to retain existing functionality
        maxhealth = Mathf.FloorToInt(maxhealth * 1.2f); // Increase max health by 20%
        attackDamage = Mathf.FloorToInt(attackDamage * 1.2f); // Increase attack damage by 20%
        GetComponent<HealthStat>().SetMaxHP(maxhealth); // Update the health stat with new max health
    }
}
