using UnityEngine;

[RequireComponent(typeof(HealthStat))] // Ensure the HealthStat component is present on the player
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health of the player
    [SerializeField] private float speed = 1000f; // Speed of the player

    private void Start()
    {
        GetComponent<HealthStat>().SetMaxHP(maxHealth); // Set the player's maximum health at the start
        GetComponent<PlayerMovement>().SetMoveSpeed(speed); // Set the player's movement speed at the start
    }
}
