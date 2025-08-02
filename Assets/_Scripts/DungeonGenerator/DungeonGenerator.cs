using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class DungeonGenerator : MonoBehaviour
{
    
    public int minRooms = 10; // Minimum number of rooms in the dungeon
    public int maxRooms = 100; // Maximum number of rooms in the dungeon

    public Vector2Int maxRoomSize = new (20, 20); // Maximum size of a room
    Vector2Int DungeonUpperRightCorner = new(5, 5); // Upper corner of the dungeon
    Vector2Int DungeonLowerLeftCorner = new (-5, -5); // Upper corner of the dungeon

    public Vector2Int StartingPos = Vector2Int.zero; // Center position of the dungeon
    Room StartingRoom; // Reference to the starting room

    List<GameObject> PossibleRooms; // List of rooms in the dungeon
    List<Door> Doors = new(); // List of doors in the dungeon
    
    private void Start()
    {
        PossibleRooms = Resources.LoadAll<GameObject>("Rooms").ToList(); // Load all room prefabs from the Resources/Rooms folder
        GenerateDungeon(); // Start the dungeon generation process when the script starts
    }

    public void GenerateDungeon()
    {
        CalculateDungeonCorners(); // Call the method to calculate the corners of the dungeon
        PlaceRooms(); // Call the method to place rooms in the dungeon
        ConnectDoors(); // Call the method to connect doors in the dungeon
        StartingRoom.EnterRoom(); // Call the method to enter the starting room
    }

    void CalculateDungeonCorners()
    {
        int dungeon_side = Mathf.CeilToInt(Mathf.Sqrt(maxRooms)); // Calculate the side length of the dungeon based on the maximum number of rooms
        // Calculate the upper right corner of the dungeon based on the starting position and dungeonside
        DungeonUpperRightCorner = new Vector2Int(StartingPos.x + dungeon_side/2 +1, StartingPos.y + dungeon_side/2 + 1);
        // Calculate the lower left corner of the dungeon based on the starting position and dungeonside
        DungeonLowerLeftCorner = new Vector2Int(StartingPos.x - dungeon_side/2 - 1, StartingPos.y - dungeon_side/2 - 1);
    }
    void PlaceRooms()
    {
        if (InvalidParameters()) return; // Check for invalid parameters before placing rooms

        int roomCount = Random.Range(minRooms, maxRooms + 1); // Randomly determine the number of rooms to place

        List<Vector2Int> free_position = new(); // List of free positions in the dungeon
        for (int x = DungeonLowerLeftCorner.x; x < DungeonUpperRightCorner.x; x++)
            for (int y = DungeonLowerLeftCorner.y; y < DungeonUpperRightCorner.y; y++)
                free_position.Add(new Vector2Int(x, y)); // Add each position in the dungeon to the list of free positions

        // Place the starting room at the center position
        PlaceStartingRoom(); // Call the method to place the starting room
        free_position.Remove(StartingPos); // Remove the starting position from the list of free positions
        roomCount--; // Decrease the room count since the starting room is already placed

        //place rooms randomly in the dungeon
        while (roomCount>0 && free_position.Count>0)
        {
            // Select a random position from the list of free positions
            int index = Random.Range(0, free_position.Count);
            Vector2Int position = free_position[index];
            free_position.RemoveAt(index); // Remove the selected position from the list of free positions
            PlaceRoomAtDungeonPosition(position.x, position.y); // Place a room at the selected position
             roomCount--; // Decrease the room count
        }

    }
    void PlaceStartingRoom()
    {
        // Randomly select a room from the list of possible rooms
        Room room = PossibleRooms[Random.Range(0, PossibleRooms.Count)].GetComponent<Room>();
        // Instantiate the room at the current position
        Room newRoom = Instantiate(room, new Vector3(StartingPos.x * maxRoomSize.x, StartingPos.y * maxRoomSize.y, 0), Quaternion.identity);
        // Add the doors from the new room to the list of doors
        newRoom.SpawnEnemies(); // Spawn enemies in the starting room
        Doors.AddRange(newRoom.Doors());
        newRoom.roomCamera.Priority = 2; // Set the camera priority for the starting room
        StartingRoom = newRoom; // Set the starting room reference
    }
    void PlaceRoomAtDungeonPosition(int x, int y)
    {
        // Randomly select a room from the list of possible rooms
        Room room = PossibleRooms[Random.Range(0, PossibleRooms.Count)].GetComponent<Room>();
        // Instantiate the room at the current position
        Room newRoom = Instantiate(room,new Vector3(x*maxRoomSize.x,y*maxRoomSize.y,0), Quaternion.identity);
        // Add the doors from the new room to the list of doors
        Doors.AddRange(newRoom.Doors());
        newRoom.SpawnEnemies(); // Spawn enemies in the newly placed room
    }
    void ConnectDoors()
    {
        // Iterate through each door in the list of doors
        foreach(Door door in Doors)
        {
            // Find a random door that is not the current door
            // and has a different type than the current door
            List<Door> available_connections = Doors.Where(d => d != door && d.GetDoorType() != door.GetDoorType()).ToList();
            Door connectedDoor = available_connections[Random.Range(0, available_connections.Count)];
            // Set the connection between the two doors
            door.SetConnection(connectedDoor);
        }
    }
    bool InvalidParameters()
    {

        if (PossibleRooms == null || PossibleRooms.Count == 0)
        {
            Debug.LogError("No rooms available to place in the dungeon.");
            return true;
        }

        if(DungeonLowerLeftCorner.x >= DungeonUpperRightCorner.x || DungeonLowerLeftCorner.y >= DungeonUpperRightCorner.y)
        {
            Debug.LogError("Invalid dungeon boundaries. Lower left corner must be less than upper right corner.");
            return true;
        }

        if((DungeonUpperRightCorner.x-DungeonLowerLeftCorner.x)*(DungeonUpperRightCorner.y-DungeonLowerLeftCorner.y) < maxRooms)
        {
            Debug.LogError("Dungeon size is too small to accommodate the maximum number of rooms.");
            return true;
        }

        if (maxRooms < minRooms)
        {
            Debug.LogError("Maximum number of rooms cannot be less than the minimum number of rooms.");
            return true;
        }

        if (maxRoomSize.x <= 0 || maxRoomSize.y <= 0)
        {
            Debug.LogError("Invalid room size. Width and height must be greater than zero.");
            return true;
        }

        return false; // No invalid parameters found
    }

}
