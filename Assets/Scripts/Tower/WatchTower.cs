using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using TowerBuffs;
public class WatchTower : BuffingTower
{
    [Header("Watch Tower Buffs")]
    [SerializeField] WatchTowerBuff firstLevelWatchTowerBuff = null;
    [SerializeField] WatchTowerBuff secondLevelWatchTowerBuff = null;
    [SerializeField] WatchTowerBuff thirdLevelWatchTowerBuff = null;

    WatchTowerBuff usingBuff = null;


    protected override void OnEnable()
    {
        base.OnEnable();
        placeTowerController.OnSellingTowerEvent += OnSellingTower;
    }

    protected override void Start()
    {
        base.Start();
        SetCurrentLevelBuff();
        SeekTarget();
    }

    protected override void SeekTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EffectRangeRadius.CalculatedValue, WhatIsTarget);
        foreach (var collider in hitColliders)
        {
            ShootingTower shootingTower = collider.GetComponent<ShootingTower>();
            if (shootingTower != null)
            {
                bool havingSameBuff = false;
                foreach (var buff in shootingTower.receivingBuffs)
                {
                    if (buff.type == usingBuff.type)
                    {
                        if (buff.BuffLevel >= CurrentTowerUpgradeLevel)
                        {
                            havingSameBuff = true;
                        }
                        else
                        {
                            shootingTower.RemoveBuff(buff);
                        }
                        break;
                    }
                }
                if (havingSameBuff) { continue; }
                BuffTower(shootingTower, CurrentTowerUpgradeLevel);
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        placeTowerController.OnSellingTowerEvent -= OnSellingTower;
    }

    public override void BuffTower(Tower tower, int buffLevel)
    {
        var buffGO = Instantiate(usingBuff, tower.transform);
        buffGO.SetSourceTower(this);
        givingBuffs.Add(buffGO);

        tower.AddBuff(buffGO, buffLevel);
    }

    public override void CancleBuff(BaseTowerBuff buff)
    {
        if (buff == null) { return; }
        givingBuffs.Remove(buff);
        buff.TargetTower.RemoveBuff(buff);
    }

    public override void SellTower()
    {
        for (int i = givingBuffs.Count - 1; i >= 0; i--)
        {
            CancleBuff(givingBuffs[i]);
        }
        base.SellTower();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        SetCurrentLevelBuff();
        SeekTarget();
    }

    protected override void OnSuccessPlacingTower(TowerData tower, GameObject placedTower)
    {
        if (placedTower == gameObject) { return; }
        SeekTarget();
    }

    protected override void OnSellingTower(Tower sellingTower)
    {
        if (sellingTower == this) { return; }
        SeekTarget();
    }

    private void SetCurrentLevelBuff()
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    usingBuff = firstLevelWatchTowerBuff;
                    break;
                }
            case 1:
                {
                    usingBuff = secondLevelWatchTowerBuff;
                    break;
                }
            case 2:
            default:
                usingBuff = thirdLevelWatchTowerBuff;
                break;

        }
    }
}
