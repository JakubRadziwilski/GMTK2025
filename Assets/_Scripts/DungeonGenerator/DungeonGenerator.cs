using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum RoomType
{
    UpExit, DownExit, LeftExit, RightExit
}

public class DungeonGenerator : MonoBehaviour
{
    public List<Room> rooms;
    public Room startingRoom;

    public GameObject UD_door_prefab; // Prefab for up/down door
    public GameObject LR_door_prefab; // Prefab for left/right door
    public float percentage_of_portal_rooms = 0.1f; // Percentage of rooms that should have portals


    public int RoomWidth = 18; // Width of each room
    public int RoomHeight = 10; // Height of each room

    public int maxRooms = 100; // Maximum number of rooms to generate
    public int minRooms = 10; // Minimum number of rooms to generate
    int roomCount = 1; // Current number of rooms generated
    public Dictionary<Vector2Int, Room> occupiedPositions = new Dictionary<Vector2Int, Room>(); // Dictionary to track occupied positions and their corresponding rooms
    public List<Vector2Int> vacantPositions = new List<Vector2Int>(); // List of vacant positions
    void Start()
    {
        LoadAllRooms();

        vacantPositions.Add(Vector2Int.zero); // Start with the starting room position

        int howManyRoomsToGenerate = Random.Range(minRooms, maxRooms + 1);
        Debug.Log($"Generating {howManyRoomsToGenerate} rooms...");

        while (roomCount <= howManyRoomsToGenerate && vacantPositions.Count > 0)
        {
            AddRoom();
        }

        if (vacantPositions.Count > 0) // there are still vacant positions left
        {
            AddDoors();
        }

        int portalRoomCount = Mathf.RoundToInt(roomCount * percentage_of_portal_rooms);
        while (portalRoomCount > 0 && occupiedPositions.Count > 0)
        {
            // Randomly select a room to turn into a portal room
            Vector2Int randomPosition = occupiedPositions.Keys.ElementAt(Random.Range(0, occupiedPositions.Count));
            EstablishPortal(randomPosition);
            // Remove the room from occupied positions to avoid reusing it
            occupiedPositions.Remove(randomPosition);
            portalRoomCount--;
        }
    }
    void LoadAllRooms()
    {
        // Load all Room ScriptableObjects from the Resources folder
        Object[] objects = Resources.LoadAll("ScriptableObjects/Rooms");
        rooms = objects.ToList().ConvertAll(obj => (Room)obj);
        if (rooms.Count == 0)
        {
            Debug.LogError("No rooms found in Resources/ScriptableObjects/Rooms");
            return;
        }
    }
    void AddRoom()
    {
        // Pick next vacant position
        Vector2Int position = vacantPositions[Random.Range(0, vacantPositions.Count)];
        vacantPositions.RemoveAll(p => p == position); // Remove from vacant positions

        //picking logic
        Room room = PickRoom(position);

        // Place the room at the selected position
        Instantiate(room.roomPrefab, new Vector3(position.x * RoomWidth, position.y * RoomHeight, 0), Quaternion.identity, this.transform);
        roomCount++;
        occupiedPositions.TryAdd(position, room); // Add to occupied positions
        AddNewVacantPositions(room, position); // Add new vacant positions based on the exits of the placed room
    }
    void AddNewVacantPositions(Room room, Vector2Int position)
    {
        // Add vacant positions based on the exits of the room
        if (room.hasUpExit && !occupiedPositions.ContainsKey(position + Vector2Int.up))
        {
            vacantPositions.Add(position + Vector2Int.up);
        }
        if (room.hasDownExit && !occupiedPositions.ContainsKey(position + Vector2Int.down))
        {
            vacantPositions.Add(position + Vector2Int.down);
        }
        if (room.hasLeftExit && !occupiedPositions.ContainsKey(position + Vector2Int.left))
        {
            vacantPositions.Add(position + Vector2Int.left);
        }
        if (room.hasRightExit && !occupiedPositions.ContainsKey(position + Vector2Int.right))
        {
            vacantPositions.Add(position + Vector2Int.right);
        }
    }
    Room PickRoom(Vector2Int position_to_occupy)
    {
        if (position_to_occupy == Vector2Int.zero)
        {
            // If the position is the starting position, return the starting room
            return startingRoom;
        }

        // Filter rooms based on the exits needed for the position
        List<Room> validRooms = new List<Room>(rooms);

        if (NeedsDownExit(position_to_occupy))
        {
            validRooms = validRooms.Where(r => r.hasDownExit).ToList(); // Need a room with a down exit
        }
        if (NeedsUpExit(position_to_occupy))
        {
            validRooms = validRooms.Where(r => r.hasUpExit).ToList(); // Need a room with an up exit
        }
        if (NeedsRightExit(position_to_occupy))
        {
            validRooms = validRooms.Where(r => r.hasRightExit).ToList(); // Need a room with a right exit
        }
        if (NeedsLeftExit(position_to_occupy))
        {
            validRooms = validRooms.Where(r => r.hasLeftExit).ToList(); // Need a room with a left exit
        }

        return validRooms[Random.Range(0, validRooms.Count)];
    }
    bool NeedsDownExit(Vector2Int position)
    {
        // Check if the position below is occupied and has an up exit
        Room room;
        if (occupiedPositions.TryGetValue(position + Vector2Int.down, out room))
        {
            return room.hasUpExit;
        }
        return false;

    }
    bool NeedsUpExit(Vector2Int position)
    {
        // Check if the position above is occupied and has a down exit
        Room room;
        if (occupiedPositions.TryGetValue(position + Vector2Int.up, out room))
        {
            return room.hasDownExit;
        }
        return false;
    }
    bool NeedsLeftExit(Vector2Int position)
    {
        // Check if the position to the left is occupied and has a right exit
        Room room;
        if (occupiedPositions.TryGetValue(position + Vector2Int.left, out room))
        {
            return room.hasRightExit;
        }
        return false;
    }
    bool NeedsRightExit(Vector2Int position)
    {
        // Check if the position to the right is occupied and has a left exit
        Room room;
        if (occupiedPositions.TryGetValue(position + Vector2Int.right, out room))
        {
            return room.hasLeftExit;
        }
        return false;
    }


    void AddDoors()
    {
        List<Door> doors = new List<Door>();
        foreach (Vector2Int occupied_position in occupiedPositions.Keys)
        {
            Room room = occupiedPositions[occupied_position];
            // Check if the room needs an up door
            if (NeedsUpDoor(room, occupied_position))
            {
                GameObject door = Instantiate(UD_door_prefab, new Vector3(occupied_position.x * RoomWidth, (occupied_position.y + 0.5f) * RoomHeight, 0), Quaternion.identity, this.transform);
                door.GetComponent<Door>().SetType(DoorType.Up);
                doors.Add(door.GetComponent<Door>()); // Add door to the list
            }
            // Check if the room needs a down door
            if (NeedsDownDoor(room, occupied_position))
            {
                GameObject door = Instantiate(UD_door_prefab, new Vector3(occupied_position.x * RoomWidth, (occupied_position.y - 0.5f) * RoomHeight, 0), Quaternion.identity, this.transform);
                door.GetComponent<Door>().SetType(DoorType.Down);
                doors.Add(door.GetComponent<Door>()); // Add door to the list
            }
            // Check if the room needs a left door
            if (NeedsLeftDoor(room, occupied_position))
            {
                GameObject door = Instantiate(LR_door_prefab, new Vector3((occupied_position.x - 0.5f) * RoomWidth, occupied_position.y * RoomHeight, 0), Quaternion.identity, this.transform);
                door.GetComponent<Door>().SetType(DoorType.Left);
                doors.Add(door.GetComponent<Door>()); // Add door to the list
            }
            // Check if the room needs a right door
            if (NeedsRightDoor(room, occupied_position))
            {
                GameObject door = Instantiate(LR_door_prefab, new Vector3((occupied_position.x + 0.5f) * RoomWidth, occupied_position.y * RoomHeight, 0), Quaternion.identity, this.transform);
                door.GetComponent<Door>().SetType(DoorType.Right);
                doors.Add(door.GetComponent<Door>()); // Add door to the list
            }
        }

        foreach(Door door in doors)
        {
            //Randomly connect doors to each other
            Door connectedDoor = doors[Random.Range(0, doors.Count)];
            while (connectedDoor == door) // Ensure the connected door is not the same as the current door
            {
                connectedDoor = doors[Random.Range(0, doors.Count)];
            }
            door.SetConnection(connectedDoor); // Set the connection
        }
    }

    bool NeedsUpDoor(Room room, Vector2Int position)
    {
        // Check if the position above is empty and current room has a up exit
        if (vacantPositions.Contains(position + Vector2Int.up))
        {
            return room.hasUpExit;
        }
        return false;
    }
    bool NeedsDownDoor(Room room, Vector2Int position)
    {
        // Check if the position below is empty and current room has a down exit
        if (vacantPositions.Contains(position + Vector2Int.down))
        {
            return room.hasDownExit;
        }
        return false;
    }
    bool NeedsLeftDoor(Room room, Vector2Int position)
    {
        // Check if the position to the left is empty and current room has a left exit
        if (vacantPositions.Contains(position + Vector2Int.left))
        {
            return room.hasLeftExit;
        }
        return false;
    }
    bool NeedsRightDoor(Room room, Vector2Int position)
    {
        // Check if the position to the right is empty and current room has a right exit
        if (vacantPositions.Contains(position + Vector2Int.right))
        {
            return room.hasRightExit;
        }
        return false;
    }

    void EstablishPortal(Vector2Int position)
    {
        // Logic to establish portals in the dungeon
        // This can be implemented based on specific requirements for portal rooms
        // For example, you might want to randomly select rooms and set them as portal rooms
        // or create a special room that acts as a portal hub.
    }
}
