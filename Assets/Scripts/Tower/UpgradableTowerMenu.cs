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

    public void UpdateTowerUI(UpgradeableTower tower)
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

    
}
