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
    public event OnSubtractMoney OnSubtractMoneyEvent;   //For Subtract money animation on UI

    public delegate void OnChangeMoney(int amount);
    public event OnChangeMoney OnChangeMoneyEvent;

    public delegate void OnTakeDamage(PlayerHQ playerHQ);
    public event OnTakeDamage TookDamage;
    
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
            TookDamage?.Invoke(this);
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
        Debug.LogError("Game Over Lose");
    }

    public void GameOverWin()
    {
        Debug.LogError("WIN");
    }

    public void OnEnemyReachedGoal(Enemy enemy)
    {
        Debug.Log("Enemy reached goal");
        TakeDamage(enemy.Damage);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        AddMoney(enemy.Money);
    }

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
        if(amount <= 0)
        {
            amount = 0;
        }
        money = amount;
        OnChangeMoneyEvent?.Invoke(amount);
    }

    private void OnPlacingTowerEvent(TowerData towerData)
    {
        SubtractMoney(towerData.price);
    }

    public void OnSellingTower(Tower tower)
    {
        AddMoney(tower.SellingPrice);
        Debug.Log("Sold " + tower);
    }

    public void OnUpgradeTower(Tower tower, int moneyToUpgrade)
    {
        SubtractMoney(moneyToUpgrade);
    }
}
