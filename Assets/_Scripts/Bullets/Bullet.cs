using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HealthStat>(out HealthStat characterStats))
        {
            characterStats.TakeDamage(PlayerStats.Instance.damage); // Deal 10 damage to the character
        }

        Destroy(gameObject); // Destroy the bullet when it collides
    }
}
