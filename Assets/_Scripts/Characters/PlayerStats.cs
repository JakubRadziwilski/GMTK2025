using UnityEngine;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

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
    public float damageReduction = 0f; // Damage reduction percentage for the player
    public float AbilityCooldown = 5f; // Cooldown time for abilities

    public event Action onRunEnd; // Event to notify when player stats change
    public List<Ability> abilities = new(); // List of abilities the player can use
    int currentAbilityIndex = 0; // Index of the current ability being used

    private void Start()
    {
        player.GetComponent<PlayersHealthStat>().SetMaxHP(maxHealth); // Set the player's maximum health at the start
        player.GetComponent<PlayerMovement>().SetMoveSpeed(speed); // Set the player's movement speed at the start
        player.GetComponentInChildren<PlayerShooting>().ShootingSpeed = 1/shootingSpeed; // Set the player's shooting speed at the start
        StartNextRun(); // Start the first run of abilities
    }

    public void StartNextRun()
    {
        isRunning = true; // Set the running flag to true

        InvokeRepeating(nameof(ActivateNextAbility), 0f, AbilityCooldown); // Start activating abilities at the defined cooldown

        Debug.Log("Starting next run with player stats: " +
            $"Max Health: {maxHealth}, Speed: {speed}, Shooting Speed: {shootingSpeed}, Damage: {damage}");
    }

    public void EndCurrentRun()
    {
        if (isRunning)
        {
            isRunning = false; // Set the running flag to false
            CancelInvoke(nameof(ActivateNextAbility)); // Stop invoking the ability activation method

            onRunEnd?.Invoke(); // Invoke the event when the run ends
            Debug.Log("Ending current run.");
        }
    }

    private void ActivateNextAbility()
    {
        if (abilities.Count == 0) return; // If there are no abilities, return

        abilities[currentAbilityIndex].Deactivate(player); // Deactivate the current ability before activating the next one
        currentAbilityIndex = (currentAbilityIndex + 1) % abilities.Count; // Move to the next ability in the list

        Ability currentAbility = abilities[currentAbilityIndex]; // Get the current ability
        currentAbility.Activate(player); // Activate the ability on the player transform
        Debug.Log($"Activated ability: {currentAbility.AbilityName()} - {currentAbility.Description()}");
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
