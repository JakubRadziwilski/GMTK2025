using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedBoost Ability", menuName = "Abilities/SpeedBoost")]
public class SpeedBoost:Ability
{
    [SerializeField] float speedBoostAmount = 1.2f; // Amount of speed to boost

    public override void Activate(Transform playerTransform)
    {
        PlayerStats.Instance.speed *= speedBoostAmount; // Increase player's speed
    }
    public override void Deactivate(Transform playerTransform)
    {
        PlayerStats.Instance.speed /= speedBoostAmount; // Reset player's speed to original value
    }
    public override string Description()
    {
        return "Boosts speed by " + ((speedBoostAmount-1)*100) + "% until next ability"; // Return a description of the speed boost ability
    }
}
