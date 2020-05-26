using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buffs;
using TowerEvents;
public abstract class BuffingTower : UpgradeableTower
{
    protected TowerEvents.TowerEvents towerEvent = null;
    protected List<BaseTowerBuff> givingBuffs = new List<BaseTowerBuff>();

    public List<BaseTowerBuff> GivingBuffs { get => givingBuffs; }

    public abstract void BuffTower(Tower tower, int buffLevel);
    public abstract void CancleBuff(BaseTowerBuff buff);

    protected abstract void OnSuccessPlacedTower(TowerData tower, GameObject placedTower);
    protected abstract void OnPostSellingTower(Tower placedTower);

    protected virtual void OnEnable()
    {
        if (towerEvent == null)
        {
            towerEvent = FindObjectOfType<TowerEvents.TowerEvents>();
        }
        towerEvent.OnSuccessPlacedTowerEvent += OnSuccessPlacedTower;
        towerEvent.OnPostSellingTowerEvent += OnPostSellingTower;
    }
    protected virtual void OnDisable()
    {
        towerEvent.OnSuccessPlacedTowerEvent -= OnSuccessPlacedTower;
        towerEvent.OnPostSellingTowerEvent -= OnPostSellingTower;
    }
}
