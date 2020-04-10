using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainGameUIController : MonoBehaviour, IMainGameEvent
{
    [SerializeField] TextMeshProUGUI hqHealthValueText = null;
    [SerializeField] TextMeshProUGUI moneyValueText = null;

    PlayerHQ playerHQ = null;
    private void Start()
    {
        playerHQ = FindObjectOfType<PlayerHQ>();
        UpdateHQHealthText(playerHQ.HQHealth);
        UpdateMoneyText(playerHQ.Money);
        playerHQ.OnChangeMoneyEvent += UpdateMoneyText;
    }
    private void UpdateHQHealthText(int currentHealth)
    {
        hqHealthValueText.text = currentHealth.ToString();
    } 

    public void OnHQTakeDamage(PlayerHQ playerHQ)
    {
        UpdateHQHealthText(playerHQ.HQHealth);
    }

    public void OnGameOverLose(){}

    public void OnGameOverWin(){}

    public void UpdateMoneyText(int amount)
    {
        moneyValueText.text = amount.ToString();
    }
}
