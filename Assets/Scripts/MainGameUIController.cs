using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainGameUIController : MonoBehaviour, IMainGameEvent
{
    [Header("Health")]
    [SerializeField] TextMeshProUGUI hqHealthValueText = null;

    [Header("Money")]
    [SerializeField] TextMeshProUGUI moneyValueText = null;

    [Header("Wave")]
    [SerializeField] TextMeshProUGUI currentWaveText = null;
    [SerializeField] TextMeshProUGUI waveCapText = null;

    [SerializeField] GameObject GameOverWinPanel = null;
    [SerializeField] GameObject GameOVerLosePanel = null;

    PlayerHQ playerHQ = null;
    SpawningController spawningController;

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

        spawningController = FindObjectOfType<SpawningController>();

        SetUpUI();

    }

    private void UpdateHQHealthText(int currentHealth)
    {
        hqHealthValueText.text = currentHealth.ToString();
    }

    private void UpdateCurrentWaveText(int currentWave)
    {
        currentWaveText.text = currentWave.ToString();
    }
    private void UpdateWaveCapText(int waveCap)
    {
        waveCapText.text = waveCap.ToString();
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

    private void SetUpUI()
    {
        UpdateHQHealthText(playerHQ.HQHealth);
        UpdateMoneyText(playerHQ.Money);
        UpdateCurrentWaveText(spawningController.WaveIndex + 1);
        UpdateWaveCapText(spawningController.WaveQuantity);
    }

    

    private void OnDisable()
    {
        playerHQ.OnChangeMoneyEvent -= UpdateMoneyText;
        playerHQ.TookDamage -= OnHQTakeDamage;
    }
}
