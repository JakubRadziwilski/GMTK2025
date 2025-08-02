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

    bool HandlePayment(int cost)  //check if you can afford and subtract the amount, returns true if payed sucesfully
    {
        if (cost <= gameManager.Points)
        {
            gameManager.SubtractPoints(cost);
            return true;
        }
        Debug.Log("not enoguh cash to buy this upgrade");
        return false;
    }

    public void BuyHealth()
    {
        if (HandlePayment(costOfUpgradesByLevel[maxHpLevel]))
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
}
