using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class MainGameUIController : MonoBehaviour, IMainGameEvent
{
    [Header("Health")]
    [SerializeField] TextMeshProUGUI hqHealthValueText = null;

    [Header("Money")]
    [SerializeField] TextMeshProUGUI moneyValueText = null;

    [Header("Wave")]
    [SerializeField] TextMeshProUGUI currentWaveText = null;
    [SerializeField] TextMeshProUGUI waveCapText = null;

    [Header("Game Over UI")]
    [SerializeField] GameOverCanvas gameOverLoseCanvas = null;

    PlayerHQ playerHQ = null;
    SpawningController spawningController;

    private void OnEnable()
    {
        if (playerHQ == null)
        {
            playerHQ = FindObjectOfType<PlayerHQ>();
        }
        if (spawningController == null)
        {
            spawningController = FindObjectOfType<SpawningController>();
        }
        playerHQ.OnChangeMoneyEvent += UpdateMoneyText;
        playerHQ.TookDamage += OnHQTakeDamage;
        spawningController.OnSpawnedNextWave += OnSpawnedNextWave;
        
    }

    private void Start()
    {
        EventSystemListener.main.AddListener(gameObject);
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
    public void OnHQTakeDamage(int amount)
    {
        UpdateHQHealthText(playerHQ.HQHealth);
    }

    public void OnGameOverLose(){
        OpenGameOverMenu(gameOverLoseCanvas);
    }

    public void OnGameOverWin(){
        //GameOverWinPanel.SetActive(true);
    }

    public void UpdateMoneyText(int amount)
    {
        moneyValueText.text = amount.ToString();
    }

    private void OnSpawnedNextWave()
    {
        currentWaveText.text = (spawningController.CurrentWaveIndex+1).ToString();
    }

    private void SetUpUI()
    {
        UpdateHQHealthText(playerHQ.HQHealth);
        UpdateMoneyText(playerHQ.Money);
        UpdateCurrentWaveText(spawningController.CurrentWaveIndex + 1);
        UpdateWaveCapText(spawningController.WaveQuantity);
    }

    private void OpenGameOverMenu(GameOverCanvas gameOverCanvas)
    {
        gameOverCanvas.ActiveCanvas();
    }

    private void OnDisable()
    {
        playerHQ.OnChangeMoneyEvent -= UpdateMoneyText;
        playerHQ.TookDamage -= OnHQTakeDamage;
        spawningController.OnSpawnedNextWave -= OnSpawnedNextWave;
    }
}
