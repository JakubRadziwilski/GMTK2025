using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; } // Singleton instance of PlayerStats

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    bool isRunning = false; // Flag to check if the player is currently running

    public Transform player; // Reference to the player's transform

    public int points = 0;  //Points to spend in the shop 
    public int scoreEarned = 0; // Points earned in the whole game, does not count spending
    public int maxHealth = 100; // Maximum health of the player
    public float speed = 1000f; // Speed of the player
    public float shootingSpeed = 1f; // Shooting speed of the player
    public int damage = 10; // Damage dealt by the player


    public event Action onRunEnd; // Event to notify when player stats change

    private void Start()
    {
        player.GetComponent<HealthStat>().SetMaxHP(maxHealth); // Set the player's maximum health at the start
        player.GetComponent<PlayerMovement>().SetMoveSpeed(speed); // Set the player's movement speed at the start
    }

    public void StartNextRun()
    {
        isRunning = true; // Set the running flag to true
        Debug.Log("Starting next run with player stats: " +
            $"Max Health: {maxHealth}, Speed: {speed}, Shooting Speed: {shootingSpeed}, Damage: {damage}");
    }

    public void EndCurrentRun()
    {
        if (isRunning)
        {
            isRunning = false; // Set the running flag to false
            onRunEnd?.Invoke(); // Invoke the event when the run ends
            Debug.Log("Ending current run.");
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        scoreEarned += amount;
    }
    public void SubtractPoints(int amount)
    {
        points -= amount;
    }
}
