using UnityEngine;

public class PlayersHealthStat:HealthStat
{
    public override void TakeDamage(int amount)
    {
        // Reduce damage by player's damage reduction
        int reducedDamage = Mathf.RoundToInt(amount * (1 - PlayerStats.Instance.damageReduction));
        base.TakeDamage(reducedDamage); // Call the base method to handle health reduction and display update

        if(currentHealth <= 0)
        {
            PlayerStats.Instance.EndCurrentRun(); // Notify PlayerStats when health reaches zero
        }
    }
}
