using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerBoostAbility", menuName = "Abilities/Power Boost Ability", order = 1)]
public class PowerBoostAbility : Ability
{
    [SerializeField] int powerBoostAmount = 10; // Amount of power to boost

    public override void Activate(Transform playerTransform)
    {
        PlayerStats.Instance.damage += powerBoostAmount; // Increase player's damage

    }

    public override void Deactivate(Transform playerTransform)
    {
        PlayerStats.Instance.damage -= powerBoostAmount; // Reset player's damage to original value
    }

    public override string Description()
    {
        return "Boosts damage by" + powerBoostAmount + " until next ability";
    }
}
