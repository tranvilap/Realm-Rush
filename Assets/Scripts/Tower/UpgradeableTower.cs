using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UpgradeableTower : Tower
{
    [Header("Upgrade")]
    [SerializeField] protected int upgradeLevelCap = 3;

    [SerializeField]
    [Tooltip("List size must be equal to Upgrade Level Cap. For describing upgrade price for each level")]
    List<int> upgradePrices = new List<int>();



    private int currentTowerUpgradeLevel = 0;
    public int CurrentTowerUpgradeLevel
    {
        get
        {
            return currentTowerUpgradeLevel;
        }
        protected set
        {
            currentTowerUpgradeLevel = Mathf.Clamp(value, 0, upgradeLevelCap);
        }
    }

    public int MoneyToNextUpgrade
    {
        get
        {
            if (CurrentTowerUpgradeLevel > upgradePrices.Count - 1)
            {
                return 0;
            }
            else
            {
                return upgradePrices[CurrentTowerUpgradeLevel];
            }
        }
    }

    public virtual bool Upgrade()
    {
        if (CurrentTowerUpgradeLevel >= upgradeLevelCap)
        {
            Debug.LogWarning("Tower reached level cap", gameObject);
            return false;
        }
        if (playerHQ.Money < MoneyToNextUpgrade)
        {
            Debug.LogWarning("Not enough money to upgrade this tower", gameObject);
            return false;
        }
        towerTotalValue += MoneyToNextUpgrade;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IUpgradeTowerEvent>(go, null, (x, y) => x.OnUpgradeTower(this, MoneyToNextUpgrade));
        }
        CurrentTowerUpgradeLevel++;
        return true;
    }
}
