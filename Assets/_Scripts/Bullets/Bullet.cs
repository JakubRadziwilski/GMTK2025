using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            // If the bullet collides with another bullet, do nothing
            return;
        }
        if (other.TryGetComponent<HealthStat>(out HealthStat characterStats))
        {
            characterStats.TakeDamage(PlayerStats.Instance.damage); // Deal damage to the character

            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                PlayerStats.Instance.AddPoints(enemy.difficultyLevel); // Add points to the player's score if the enemy is hit
            }
        }

        Destroy(gameObject); // Destroy the bullet when it collides
    }
}
