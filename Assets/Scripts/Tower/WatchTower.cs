using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Buffs;
public class WatchTower : BuffingTower
{
    [Header("Watch Tower Buffs")]
    [SerializeField] WatchTowerBuff level0WatchTowerBuff = null;
    [SerializeField] WatchTowerBuff level1WatchTowerBuff = null;
    [SerializeField] WatchTowerBuff level2WatchTowerBuff = null;

    [Header("Level 0")]
    [SerializeField] float level0EffectRange = 1;
    [SerializeField] GameObject level0TowerModel = null;
    [Header("Level 1")]
    [SerializeField] float level1EffectRange = 2;
    [SerializeField] GameObject level1TowerModel = null;
    [Header("Level 2")]
    [SerializeField] float level2EffectRange = 3;
    [SerializeField] GameObject level2TowerModel = null;


    WatchTowerBuff usingBuff = null;

    protected override void Start()
    {
        base.Start();
        SeekTarget();
    }

    protected override void SeekTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EffectRangeRadius.CalculatedValue, WhatIsTarget);
        foreach (var collider in hitColliders)
        {
            if(collider.transform == transform) { continue; }
            Tower shootingTower = collider.GetComponent<Tower>();
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
        SeekTarget();
    }

    protected override void OnSuccessPlacedTower(TowerData tower, GameObject placedTower)
    {
        if (placedTower == gameObject) { return; }
        SeekTarget();
    }

    protected override void OnPostSellingTower(Tower soldTower)
    {
        if (soldTower == this) { return; }
        SeekTarget();
    }

    protected override void SetUpTower()
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    EffectRangeRadius.BaseValue = level0EffectRange;
                    usingBuff = level0WatchTowerBuff;
                    level0TowerModel.SetActive(true);
                    level1TowerModel.SetActive(false);
                    level2TowerModel.SetActive(false);
                    break;
                }
            case 1:
                {
                    EffectRangeRadius.BaseValue = level1EffectRange;
                    usingBuff = level1WatchTowerBuff;
                    level0TowerModel.SetActive(false);
                    level1TowerModel.SetActive(true);
                    level2TowerModel.SetActive(false);
                    break;
                }
            case 2:
            default:
                EffectRangeRadius.BaseValue = level2EffectRange;
                usingBuff = level2WatchTowerBuff;
                level0TowerModel.SetActive(false);
                level1TowerModel.SetActive(false);
                level2TowerModel.SetActive(true);
                break;
        }
    }

}
