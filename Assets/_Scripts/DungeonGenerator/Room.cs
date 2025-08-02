using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float roomWaitTime = 2f; // Time to wait before entering the room
    public CinemachineCamera roomCamera; // Camera associated with the room
    public int RoomHeight = 10; // Height of the room
    public int RoomWidth = 18; // Width of the room
    public int MaxDifficultyLevel = 10;
    public int minDifficultyLevel = 3;

    public List<Enemy> enemies = new (); // List to hold enemies in the room
    public List<Door> Doors()
    {
        ExitRoom(); // Ensure the room is exited before getting doors
        List<Door> doors = new(GetComponentsInChildren<Door>()); // Get all doors in the room
        if (doors.Count == 0)
        {
            Debug.LogWarning("No doors found in the room: " + gameObject.name);
            return null;
        }
        return doors; // Return the list of doors
    }

    public void EnterRoom()
    {
        // This method can be used to handle logic when entering the room, such as activating the camera
        if (roomCamera != null)
        {
            roomCamera.Priority = 2; // Set the camera priority to make it active
        }
        else
        {
            Debug.LogWarning("Room camera is not assigned for room: " + gameObject.name);
        }

        Invoke(nameof(RespawnEnemies),roomWaitTime); // Respawn enemies when entering the room
    }

    public void RespawnEnemies()
    {
        if (AllEnemiesDead())
        {
            // This method respawns all enemies in the room
            foreach (Enemy enemy in enemies)
            {
                enemy.Respawn(); // Respawn all enemies in the room
            }
        }
        else
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.gameObject.activeSelf) // Check if the enemy is active
                {
                    enemy.SetPlayerInThisRoom(true); // Set the player in this room to true for active enemies
                }
            }
        }
    }

    public bool AllEnemiesDead()
    {
        // This method checks if all enemies in the room are dead
        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeSelf) // Check if the enemy is still active
            {
                return false; // If any enemy is active, return false
            }
        }
        return true; // All enemies are dead, return true
    }

    public void ExitRoom()
    {
        // This method can be used to handle logic when exiting the room, such as deactivating the camera
        if (roomCamera != null)
        {
            roomCamera.Priority = 0; // Set the camera priority to deactivate it
        }
        else
        {
            Debug.LogWarning("Room camera is not assigned for room: " + gameObject.name);
        }
        foreach (Enemy enemy in enemies)
        {
            enemy.SetPlayerInThisRoom(false); // Set the player in this room to false for all enemies
        }
    }

    public void SpawnEnemies()
    {   
        // This method spawns enemies in the room based on the difficulty level
        int difficultyLevel = Random.Range(minDifficultyLevel, MaxDifficultyLevel + 1); // Randomly determine difficulty level
        
        List<Enemy> possible_enemies = Resources.LoadAll<Enemy>("Enemies").ToList(); // Load all enemy prefabs from Resources/Enemies folder

        if (possible_enemies.Count == 0)
        {
            Debug.LogWarning("No enemies found in Resources/Enemies folder.");
            return; // Exit if no enemies are found
        }
        // Spawn enemies based on the difficulty level
        while (difficultyLevel > 0)
        {
            //filter enemies based on difficulty level
            possible_enemies = possible_enemies.Where(e => e.GetComponent<Enemy>().difficultyLevel <= difficultyLevel).ToList();
            if (possible_enemies.Count == 0)
            {
                break; // Exit if no enemies match the difficulty level
            }
            // Randomly select an enemy from the filtered list
            Enemy enemyToSpawn = possible_enemies[Random.Range(0, possible_enemies.Count)];
            // Instantiate the enemy at a random position within the room
            Vector3 spawnPosition = RandomInRoomPosition();
            Enemy spawned_enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity, transform); // Create an instance of the enemy
            enemies.Add(spawned_enemy); // Add the enemy instance to the room's list of enemies
            difficultyLevel -= spawned_enemy.GetComponent<Enemy>().difficultyLevel; // Decrease the difficulty level based on the spawned enemy's difficulty
        }
    }

    public Vector3 RandomInRoomPosition()
    {
        // Generate a random position within the room's dimensions
        float x = Random.Range(-RoomWidth / 2f, RoomWidth / 2f);
        float y = Random.Range(-RoomHeight / 2f, RoomHeight / 2f);
        return new Vector3(transform.position.x + x, transform.position.y + y, 0); // Return the random position in the room
    }
}
