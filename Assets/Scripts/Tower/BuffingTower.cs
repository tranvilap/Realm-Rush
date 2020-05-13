using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerBuffs;

public abstract class BuffingTower : UpgradeableTower
{
    protected PlaceTowerController placeTowerController = null;
    protected List<BaseTowerBuff> givingBuffs = new List<BaseTowerBuff>();

    public List<BaseTowerBuff> GivingBuffs { get => givingBuffs; }

    public abstract void BuffTower(Tower tower, int buffLevel);
    public abstract void CancleBuff(BaseTowerBuff buff);

    protected abstract void OnSuccessPlacingTower(TowerData tower, GameObject placedTower);
    protected abstract void OnSellingTower(Tower placedTower);

    protected virtual void OnEnable()
    {
        if (placeTowerController == null)
        {
            placeTowerController = FindObjectOfType<PlaceTowerController>();
        }
        placeTowerController.SuccessPlacingTowerEvent += OnSuccessPlacingTower;
    }
    protected virtual void OnDisable()
    {
        placeTowerController.SuccessPlacingTowerEvent -= OnSuccessPlacingTower;
    }
}
