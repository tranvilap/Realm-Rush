using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradableTowerMenu : LookAtCameraObject
{
    [SerializeField] Button upgradeButton = null;
    [SerializeField] TextMeshProUGUI upgradeCostText = null;
    [SerializeField] TextMeshProUGUI sellPriceText = null;

    UpgradeableTower tower;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (tower == null)
        {
            tower = GetComponentInParent<UpgradeableTower>();
        }
        if(tower != null)
        {
            tower.OnUpgradeEvent += OnUpgradeTower;
        }
    }
     private void Start()
    {
        gameObject.SetActive(false);
        if(tower!= null)
        {
            UpdateTowerUI(tower);
        }
    }
    private void OnUpgradeTower(UpgradeableTower tower)
    {
        UpdateTowerUI(tower);
    }

    private void UpdateTowerUI(UpgradeableTower tower)
    {
        if (tower.isFinalLevel)
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = tower.NextUpgradeCost + "$";
        }
        sellPriceText.text = tower.SellingPrice + "$";
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (tower != null)
        {
            tower.OnUpgradeEvent -= OnUpgradeTower;
        }
    }
}
