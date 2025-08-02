using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Points;
    void Awake()
    {

    }

    void Start()
    {

    }

    public void SubtractPoints(int amount)
    {
        Points -= amount;
    }
    public void AddPoints(int amount)
    {
        Points += amount;
    }
}
