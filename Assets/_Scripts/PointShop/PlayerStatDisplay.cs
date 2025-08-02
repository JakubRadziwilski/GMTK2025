using TMPro;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text shootingSpeedText;
    [SerializeField] TMP_Text speedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTexts()
    {
        hpText.text = PlayerStats.Instance.maxHealth.ToString();
        damageText.text = PlayerStats.Instance.damage.ToString();
        shootingSpeedText.text = PlayerStats.Instance.shootingSpeed.ToString();
        speedText.text = PlayerStats.Instance.speed.ToString();
    }
}
