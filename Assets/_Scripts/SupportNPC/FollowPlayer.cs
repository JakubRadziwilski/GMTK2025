using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDistance = 2f; // Distance to maintain from the player
    public float MoveSpeed = 1f;
    public float maxDistance = 10f; // Maximum distance to follow the player
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DistanceToPlayer() > maxDistance)
        {
            transform.position = player.position + (transform.position - player.position).normalized * maxDistance;
            return;
        }
        if (DistanceToPlayer() > followDistance)
        {         
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * MoveSpeed * Time.fixedDeltaTime);
        }
    }

    float DistanceToPlayer()
    {
               return Vector2.Distance(transform.position, player.position);
    }
}
