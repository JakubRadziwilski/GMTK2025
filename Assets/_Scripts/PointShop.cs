using UnityEngine;

public class PointShop : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    HealthStat playerHealthStat;

    [SerializeField] int[] costOfUpgradesByLevel = {10, 20, 30};

    int maxHpLevel = 0;
    [SerializeField] int[] maxHpIncrements = {10, 20, 30};


    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        playerHealthStat = player.GetComponent<HealthStat>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool Pay(int cost)
    {
        if (cost <= gameManager.Points)
        {
            gameManager.SubtractPoints(cost);
            return true;
        }
        return false;
    }

    public void BuyHealth()
    {
        if (maxHpIncrements.Length > maxHpLevel)
        {
            playerHealthStat.IncreaseMaxHp(maxHpIncrements[maxHpLevel]);
            maxHpLevel += 1;
        }
        else
        {
            playerHealthStat.IncreaseMaxHp(maxHpIncrements[0]);
            maxHpLevel += 1;
        }
    }
}
