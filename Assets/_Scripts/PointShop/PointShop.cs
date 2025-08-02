using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PointShop : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] PlayerStatDisplay playerStatDisplay;
    [SerializeField] UpgradeCostsDisplay upgradeCostsDisplay;

    [SerializeField] Canvas canvas;

    public int[] costOfUpgradesByLevel = { 10, 20, 30, 30, 40 };    // cost of all upgrades on each level


    public int maxHpLevel = 0;  //level of upgrade, starting from level 0
    public int[] maxHpIncrements = { 10, 20, 30 };  //how much the stat increases on each level

    public int damageLevel = 0;
    public int[] damageIncrements = { 10, 20, 30 };

    public int shootingSpeedLevel = 0;
    public int[] shootingSpeedIncrements = { 10, 20, 30 };

    public int speedLevel = 0;
    public int[] speedIncrements = { 10, 20, 30 };


    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    bool HandlePayment(int cost)  //check if you can afford and subtract the amount, returns true if payed sucesfully
    {
        if (cost <= PlayerStats.Instance.points)
        {
            PlayerStats.Instance.SubtractPoints(cost);
            return true;
        }
        Debug.Log("not enoguh cash to buy this upgrade");
        return false;
    }

    public void BuyHealth()
    {

            if (maxHpIncrements.Length > maxHpLevel)
        {
            if (HandlePayment(costOfUpgradesByLevel[maxHpLevel]))
            {
                PlayerStats.Instance.maxHealth += maxHpIncrements[maxHpLevel];
                maxHpLevel += 1;
            }
        }
        else
        {
            if (HandlePayment(costOfUpgradesByLevel[costOfUpgradesByLevel.Length - 1]))
            {
                PlayerStats.Instance.maxHealth += maxHpIncrements[0];
                maxHpLevel += 1;

            }
        }
        UpdateTexts();
    }

    public void BuyDamage()
    {
        if (damageIncrements.Length > damageLevel)
        {
            if (HandlePayment(costOfUpgradesByLevel[damageLevel]))
            {
                PlayerStats.Instance.damage += damageIncrements[damageLevel];
                damageLevel += 1;
            }
        }
        else
        {
            if (HandlePayment(costOfUpgradesByLevel[costOfUpgradesByLevel.Length - 1]))
            {
                PlayerStats.Instance.damage += damageIncrements[0];
                damageLevel += 1;
            }
        }
        UpdateTexts();
    }
    public void BuyShootingSpeed()
    {
        if (shootingSpeedIncrements.Length > shootingSpeedLevel)
        {
            if (HandlePayment(costOfUpgradesByLevel[shootingSpeedLevel]))
            {
                PlayerStats.Instance.shootingSpeed += shootingSpeedIncrements[shootingSpeedLevel];
                shootingSpeedLevel += 1;
            }
        }
        else
        {
            if (HandlePayment(costOfUpgradesByLevel[costOfUpgradesByLevel.Length - 1]))
            {
                PlayerStats.Instance.shootingSpeed += shootingSpeedIncrements[0];
                shootingSpeedLevel += 1;
            }
        }
        UpdateTexts();
    }
    public void BuySpeed()
    {
        if (speedIncrements.Length > speedLevel)
        {
            if (HandlePayment(costOfUpgradesByLevel[speedLevel]))
            {
                PlayerStats.Instance.speed += speedIncrements[speedLevel];
                speedLevel += 1;
            }
        }
        else
        {
            if (HandlePayment(costOfUpgradesByLevel[costOfUpgradesByLevel.Length - 1]))
            {
                PlayerStats.Instance.speed += speedIncrements[0];
                speedLevel += 1;
            }
        }
        UpdateTexts();
    }

    public void EnableCanvas(bool enable)
    {
        if (enable == true)
        {
            canvas.gameObject.SetActive(true);
        }
        else canvas.gameObject.SetActive(false);
    }

    public void UpdateTexts()
    {
        playerStatDisplay.UpdateTexts();
        upgradeCostsDisplay.UpdateTexts();
        //update money display
    } 
}
