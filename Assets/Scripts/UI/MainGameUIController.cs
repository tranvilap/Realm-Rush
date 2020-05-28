using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Game.Sound;

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
    [SerializeField] GameOverCanvas gameOverWinCanvas = null;

    [Header("Pause")]
    [SerializeField] GameObject pausePanel = null;
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider sfxSlider = null;

    PlayerHQ playerHQ = null;
    GameController gameController;
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
        playerHQ.OnChangingMoneyEvent += UpdateMoneyText;
        playerHQ.OnTakingDamage += OnHQTakeDamage;
        spawningController.OnSpawnedNextWave += OnSpawnedNextWave;
        
    }

    private void Start()
    {
        EventSystemListener.main.AddListener(gameObject);
        SetUpUI();
        gameController = FindObjectOfType<GameController>();
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
        OpenGameOverMenu(gameOverWinCanvas);
    }

    public void UpdateMoneyText(int amount)
    {
        moneyValueText.text = amount.ToString();
    }
    public void OnClickPauseButton()
    {
        if (gameController.IsGameOver) { return; }
        pausePanel.SetActive(true);
        musicSlider.value = PlayerPrefs.GetFloat(Constants.GLOBAL_MUSIC, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(Constants.GLOBAL_SFX, 1f);
        Time.timeScale = 0f;
    }
    public void OnClickClosePausePanel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnChangeMusicVolume(float volume)
    {
        AudioManager.ChangeGlobalMusicVolume(volume);
    }
    public void OnChangeSFXVolume(float volume)
    {
        AudioManager.ChangeGlobalSFXVolume(volume);
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
        playerHQ.OnChangingMoneyEvent -= UpdateMoneyText;
        playerHQ.OnTakingDamage -= OnHQTakeDamage;
        spawningController.OnSpawnedNextWave -= OnSpawnedNextWave;
    }
}
