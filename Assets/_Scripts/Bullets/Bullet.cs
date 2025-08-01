using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<HealthStat>(out HealthStat characterStats))
        {
            characterStats.TakeDamage(10); // Deal 10 damage to the character
        }

        Destroy(gameObject); // Destroy the bullet when it collides
    }
}
