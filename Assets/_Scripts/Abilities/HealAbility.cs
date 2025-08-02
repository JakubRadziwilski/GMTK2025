using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Ability", menuName = "Abilities/Heal Ability")]
public class HealAbility: Ability
{
    [SerializeField] int healAmount = 10; // Amount of health to heal
    public override void Activate(Transform playerTransform)
    {
        playerTransform.GetComponent<PlayersHealthStat>().Heal(healAmount); // Heal the player by 10 health points
    }

    public override string Description()
    {
        return "Heals " + healAmount + " health points."; // Return a description of the heal ability
    }
}
