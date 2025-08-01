using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public enum DoorType
{
    Up, // Represents an upward door
    Down, // Represents a downward door
    Left, // Represents a leftward door
    Right, // Represents a rightward door
}
public class Door : MonoBehaviour
{
    [SerializeField] Door connected_to; // Reference to the door this door is connected to
    [SerializeField] DoorType type; // True if this is an up door, false if it's a down door

    public void SetType(DoorType type)
    {
        this.type = type; // Set the type of the door
    }
    public void SetConnection(Door door)
    {
        connected_to = door; // Set the door this door is connected to
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the door's trigger area
        if (other.CompareTag("Player"))
        {
            // If so, teleport the player to the connected door's spawn position
            other.transform.position = connected_to.SpawnPos();
            // Call ExitRoom on the current door's room
            ExitRoom();
            // Call the EnterRoom method on the connected door's room
            Invoke("EnterTheOtherRoom",2);
        }
    }

    public Vector3 SpawnPos()
    {
        // Return the position of the door based on its type
        switch (type)
        {
            case DoorType.Up:
                return transform.position + 3 * Vector3.down;
            case DoorType.Down:
                return transform.position + 3 * Vector3.up;
            case DoorType.Left:
                return transform.position + 3 * Vector3.right;
            case DoorType.Right:
                return transform.position + 3 * Vector3.left;
            default:
                return transform.position; // Default case if no type matches
        }
    }

    public void EnterTheOtherRoom()
    {
        // Call the EnterRoom method on the connected door's room
        connected_to.GetComponentInParent<Room>().EnterRoom();
    }
    public void ExitRoom()
    {
        // Call the ExitRoom method on the room this door is in
        GetComponentInParent<Room>().ExitRoom();
    }

    public DoorType GetDoorType()
    {
        // Return the type of the door
        return type;
    }
}
