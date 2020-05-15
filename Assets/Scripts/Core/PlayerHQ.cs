using Game.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHQ : MonoBehaviour, IEnemyEvent
{
    [SerializeField] int hqHealth = 10;
    [SerializeField] private int money = 0;

    [SerializeField] SFX coinEarningSFX = null;
    [SerializeField] SFX coinSpendingSFX = null;

    GameController gameController;
    AudioSource audioSource;

    public event Action<int> OnEarningMoneyEvent;
    public event Action<int> OnSpendingMoneyEvent;
    public event Action<int> OnChangingMoneyEvent;

    public event Action<int> OnTakingDamage;

    public event Action<int> OnHealing;

    public int HQHealth { get => hqHealth; set => hqHealth = value; }
    public int Money { get => money; }

    private void Start()
    {
        EventSystemListener.main.AddListener(gameObject);
        gameController = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnEnemyReachedGoal(Enemy enemy)
    {
        TakeDamage(enemy.Damage);
    }
    public void OnEnemyDie(Enemy enemy)
    {
        EarnMoney(enemy.Money);
    }
    public void OnEnemySpawned(Enemy enemy) { }

    public void TakeDamage(int amount)
    {
        if (HQHealth > 0)
        {
            HQHealth -= amount;
            OnTakingDamage?.Invoke(amount);
            if (HQHealth <= 0)
            {
                HQHealth = 0;
                gameController.GameOverLose();
            }
        }
    }

    private void ChangeMoney(int amount)
    {
        if (amount <= 0)
        {
            amount = 0;
        }
        money = amount;
        OnChangingMoneyEvent?.Invoke(amount);
    }
    public void EarnMoney(int amount)
    {
        ChangeMoney(money + amount);
        AudioManager.PlayOneShotSound(audioSource, coinEarningSFX);
        OnEarningMoneyEvent?.Invoke(amount);
    }
    public void SpendMoney(int amount)
    {
        ChangeMoney(money - amount);
        AudioManager.PlayOneShotSound(audioSource, coinSpendingSFX);
        OnSpendingMoneyEvent?.Invoke(amount);
    }
}
