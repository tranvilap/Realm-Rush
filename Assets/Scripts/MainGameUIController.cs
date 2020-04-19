using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainGameUIController : MonoBehaviour, IMainGameEvent
{
    [SerializeField] TextMeshProUGUI hqHealthValueText = null;
    [SerializeField] TextMeshProUGUI moneyValueText = null;
    [SerializeField] GameObject GameOverWinPanel = null;
    [SerializeField] GameObject GameOVerLosePanel = null;

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
        EventSystemListener.main.AddListener(gameObject);
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

    public void OnGameOverLose(){
        GameOVerLosePanel.SetActive(true);
    }

    public void OnGameOverWin(){
        GameOverWinPanel.SetActive(true);
    }

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
