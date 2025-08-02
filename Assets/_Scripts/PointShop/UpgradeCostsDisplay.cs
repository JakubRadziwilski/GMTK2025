using UnityEngine;
using TMPro;

public class UpgradeCostsDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text shootingSpeedText;
    [SerializeField] TMP_Text speedText;

    PointShop pointShop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointShop = FindAnyObjectByType<PointShop>();
        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
     public void UpdateTexts()
    {
        if (pointShop.maxHpLevel < pointShop.maxHpIncrements.Length)
        hpText.text = pointShop.costOfUpgradesByLevel[pointShop.maxHpLevel].ToString();
        if (pointShop.damageLevel < pointShop.damageIncrements.Length)
        damageText.text = pointShop.costOfUpgradesByLevel[pointShop.damageLevel].ToString();
        if (pointShop.shootingSpeedLevel < pointShop.shootingSpeedIncrements.Length)
        shootingSpeedText.text = pointShop.costOfUpgradesByLevel[pointShop.shootingSpeedLevel].ToString();
        if (pointShop.speedLevel < pointShop.speedIncrements.Length)
        speedText.text = pointShop.costOfUpgradesByLevel[pointShop.speedLevel].ToString();
    }
}
