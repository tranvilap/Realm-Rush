using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHQ : MonoBehaviour, IEnemyEvent
{
    [SerializeField] int hqHealth = 10;
    [SerializeField] private int money = 0;

    public delegate void OnAddMoney(int amount);
    public OnAddMoney OnAddMoneyEvent;   //For Add money animation on UI
    public delegate void OnSubtractMoney(int amount);
    public OnSubtractMoney OnSubtractMoneyEvent;   //For Subtract money animation on UI
    public delegate void OnChangeMoney(int amount);
    public OnChangeMoney OnChangeMoneyEvent;


    PlaceTowerController placeTowerController;

    public int HQHealth { get => hqHealth; set => hqHealth = value; }
    public int Money { get => money; }

    private void Start()
    {
        EventSystemListener.main.AddListener(gameObject);
        placeTowerController = FindObjectOfType<PlaceTowerController>();
        placeTowerController.SuccessPlacingTowerEvent += OnPlacingTowerEvent;
    }

    public void TakeDamage(int amount)
    {
        if (HQHealth > 0)
        {
            HQHealth -= amount;
            foreach (var go in EventSystemListener.main.Listeners)
            {
                ExecuteEvents.Execute<IMainGameEvent>(go, null, (x, y) => x.OnHQTakeDamage(this));
            }
            if (HQHealth <= 0)
            {
                HQHealth = 0;
                GameOverLose();
            }
        }
    }

    public void GameOverLose()
    {
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IMainGameEvent>(go, null, (x, y) => x.OnGameOverLose());
        }
        Debug.LogError("Player died");
    }

    public void OnEnemyReachedGoal(Enemy enemy)
    {
        Debug.Log("Enemy reached goal");
        TakeDamage(enemy.Damage);
    }

    public void OnEnemyDie(Enemy enemy) { }

    public void AddMoney(int amount)
    {
        ChangeMoney(money + amount);
        OnAddMoneyEvent?.Invoke(amount);
    }

    public void SubtractMoney(int amount)
    {
        ChangeMoney(money - amount);
        OnSubtractMoneyEvent?.Invoke(amount);
    }

    private void ChangeMoney(int amount)
    {
        Mathf.Min(0, amount);
        OnChangeMoneyEvent?.Invoke(amount);
    }

    private void OnPlacingTowerEvent(TowerData towerData)
    {
        SubtractMoney(towerData.price);
    }
}
