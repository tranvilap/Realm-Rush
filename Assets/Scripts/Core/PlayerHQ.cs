using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHQ : MonoBehaviour, IEnemyEvent, ITowerEvent, IUpgradeTowerEvent
{
    [SerializeField] int hqHealth = 10;
    [SerializeField] private int money = 0;

    public delegate void OnAddMoney(int amount);
    public event OnAddMoney OnAddMoneyEvent;   //For Add money animation on UI

    public delegate void OnSubtractMoney(int amount);
    public event OnSubtractMoney OnDeductMoneyEvent;   //For Subtract money animation on UI

    public delegate void OnChangeMoney(int amount);
    public event OnChangeMoney OnChangeMoneyEvent;

    public delegate void OnTakeDamage(int amount);
    public event OnTakeDamage TookDamage;

    PlaceTowerController placeTowerController;
    GameController gameController;

    public int HQHealth { get => hqHealth; set => hqHealth = value; }
    public int Money { get => money; }

    private void OnEnable()
    {
        if (placeTowerController == null)
        {
            placeTowerController = FindObjectOfType<PlaceTowerController>();
        }
        placeTowerController.SuccessPlacingTowerEvent += OnPlacingTowerEvent;
    }

    private void Start()
    {
        EventSystemListener.main.AddListener(gameObject);

        gameController = FindObjectOfType<GameController>();
    }
    
    public void OnEnemyReachedGoal(Enemy enemy)
    {
        TakeDamage(enemy.Damage);
    }
    public void OnEnemyDie(Enemy enemy)
    {
        AddMoney(enemy.Money);
    }
    public void OnEnemySpawned(Enemy enemy) { }

    public void TakeDamage(int amount)
    {
        if (HQHealth > 0)
        {
            HQHealth -= amount;
            TookDamage?.Invoke(amount);
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
        OnChangeMoneyEvent?.Invoke(amount);
    }
    public void AddMoney(int amount)
    {
        ChangeMoney(money + amount);
        OnAddMoneyEvent?.Invoke(amount);
    }
    public void SubtractMoney(int amount)
    {
        ChangeMoney(money - amount);
        OnDeductMoneyEvent?.Invoke(amount);
    }

    private void OnPlacingTowerEvent(TowerData towerData)
    {
        SubtractMoney(towerData.price);
    }
    public void OnSellingTower(Tower tower)
    {
        AddMoney(tower.SellingPrice);
    }
    public void OnUpgradeTower(Tower tower, int moneyToUpgrade)
    {
        SubtractMoney(moneyToUpgrade);
    }

    private void OnDisable()
    {
        placeTowerController.SuccessPlacingTowerEvent -= OnPlacingTowerEvent;
    }
    
}
