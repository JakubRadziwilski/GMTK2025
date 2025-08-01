using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    public CinemachineCamera roomCamera; // Camera associated with the room
    public List<Door> Doors()
    {
        ExitRoom(); // Ensure the room is exited before getting doors
        List<Door> doors = new List<Door>(GetComponentsInChildren<Door>()); // Get all doors in the room
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
    }
}
