using UnityEngine;


[CreateAssetMenu(fileName = "Room", menuName = "Scriptable Objects/Room")]
public class Room : ScriptableObject
{
    public GameObject roomPrefab;

    public bool hasUpExit;
    public bool hasDownExit;
    public bool hasLeftExit;
    public bool hasRightExit;
}
