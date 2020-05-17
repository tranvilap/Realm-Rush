﻿using Game.Sound;
using System;
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

    [SerializeField] private int currentTowerUpgradeLevel = 0;

    UpgradableTowerMenu towerMenu;


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
    public int NextUpgradeCost
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
    public bool isFinalLevel
    {
        get
        {
            return currentTowerUpgradeLevel >= upgradeLevelCap;
        }
    }

    protected override void Start()
    {
        base.Start();
        if (menuCanvas != null)
        {
            towerMenu = MenuCanvas.GetComponent<UpgradableTowerMenu>();
        }
        if (towerMenu != null)
        {
            towerMenu.UpdateTowerUI(this);
        }
        CurrentTowerUpgradeLevel = currentTowerUpgradeLevel;
        SetUpTower();
    }
    public virtual void Upgrade()
    {
        if (CheckUpgradeable() != TowerEvents.UPGRADE_TOWER_RESULT.SUCCESS)
        {
            FailedUpgrade(CheckUpgradeable());
            return;
        }

        PreUpgrade();

        DisplayUpgradeEffect();
        playerHQ.SpendMoney(NextUpgradeCost);
        towerTotalValue += NextUpgradeCost;
        CurrentTowerUpgradeLevel++;

        PostUpgrade();

        SetUpTower();
        towerMenu.UpdateTowerUI(this);
        if (rangeEffectField.activeInHierarchy)
        {
            ShowEffectRange();
        }
    }

    protected virtual void DisplayUpgradeEffect()
    {
        GameObject upgradeEffect = SharedObjectPooler.main.GetPooledObject(Constants.UPGRADE_TOWER_VFX);
        if (upgradeEffect != null)
        {
            Vector3 effectPosition = transform.position;
            effectPosition.y = upgradeEffect.transform.position.y;
            upgradeEffect.transform.position = effectPosition;
            upgradeEffect.SetActive(true);
        }
    }

    protected virtual void FailedUpgrade(TowerEvents.UPGRADE_TOWER_RESULT reason)
    {
        towerEvents.OnFailedUpgradedTower(this, reason);
    }
    protected virtual void PreUpgrade()
    {
        towerEvents.OnPreUpgradeTower(this);
    }
    protected virtual void PostUpgrade()
    {
        towerEvents.OnSuccessUpgradedTower(this);
    }
    protected virtual TowerEvents.UPGRADE_TOWER_RESULT CheckUpgradeable()
    {
        if (CurrentTowerUpgradeLevel >= upgradeLevelCap)
        {
            Debug.LogWarning("Tower reached level cap", gameObject);
            return TowerEvents.UPGRADE_TOWER_RESULT.TOWER_REACHED_MAX_LEVEL;
        }
        if (playerHQ.Money < NextUpgradeCost)
        {
            Debug.LogWarning("Not enough money to upgrade this tower", gameObject);
            return TowerEvents.UPGRADE_TOWER_RESULT.NOT_ENOUGH_MONEY;
        }
        return TowerEvents.UPGRADE_TOWER_RESULT.SUCCESS;
    }

    public virtual void OnUpgradeButton()
    {
        Upgrade();
    }
    protected abstract void SetUpTower();
}
