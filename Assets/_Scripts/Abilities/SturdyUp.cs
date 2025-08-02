using UnityEngine;
[CreateAssetMenu(fileName = "SturdyUp", menuName = "Abilities/SturdyUp", order = 1)]
public class SturdyUp: Ability
{
    [SerializeField] float damageReductionAmount = 0.2f; // Amount of damage reduction
    [SerializeField] float speedReductionAmount = 0.2f; // Amount of speed reduction

    public override void Activate(Transform playerTransform)
    {
        PlayerStats.Instance.damageReduction += damageReductionAmount; // Increase player's damage reduction
        PlayerStats.Instance.speed *= (1 - speedReductionAmount); // Reduce player's speed
    }

    public override void Deactivate(Transform playerTransform)
    {
        PlayerStats.Instance.damageReduction -= damageReductionAmount; // Reset player's damage reduction
        PlayerStats.Instance.speed /= (1 - speedReductionAmount); // Reset player's speed
    }

    public override string Description()
    {
        return "Reduces damage taken by " + (damageReductionAmount-1)*100 + "%, but slows you down by " + (speedReductionAmount - 1) * 100 + "%";
    }
}
