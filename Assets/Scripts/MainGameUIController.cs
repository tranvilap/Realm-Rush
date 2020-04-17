using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainGameUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hqHealthValueText = null;
    [SerializeField] TextMeshProUGUI moneyValueText = null;

    PlayerHQ playerHQ = null;

    private void OnEnable()
    {
        if (playerHQ == null)
        {
            playerHQ = FindObjectOfType<PlayerHQ>();
        }
        playerHQ.OnChangeMoneyEvent += UpdateMoneyText;
        playerHQ.TookDamage += OnHQTakeDamage;
    }

    private void Start()
    {
        UpdateHQHealthText(playerHQ.HQHealth);
        UpdateMoneyText(playerHQ.Money);
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

    private void OnDisable()
    {
        playerHQ.OnChangeMoneyEvent -= UpdateMoneyText;
        playerHQ.TookDamage -= OnHQTakeDamage;
    }
}
