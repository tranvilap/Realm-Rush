using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerEvents
{
    public enum PLACE_TOWER_FAIL_REASON { NOT_ENOUGH_MONEY, UNPLACEABLE_POINT }
    public enum UPGRADE_TOWER_RESULT { SUCCESS,NOT_ENOUGH_MONEY, TOWER_REACHED_MAX_LEVEL, INVALID_LEVEL }

    public class TowerEvents : MonoBehaviour
    {
        public event Action<TowerData> OnPlacingTowerEvent;
        public event Action<TowerData, GameObject> OnSuccessPlacedTowerEvent;
        public event Action<TowerData, PLACE_TOWER_FAIL_REASON> FailedPlacedTowerEvent;

        public event Action<UpgradeableTower> OnPreUpgradeTowerEvent; //Invoke before upgrade tower
        public event Action<UpgradeableTower> OnSuccessUpgradedTowerEvent; //Invoke after upgrade tower - success
        public event Action<UpgradeableTower, UPGRADE_TOWER_RESULT> OnFailedUpgradedTowerEvent;

        public event Action<Tower> OnPreSellingTowerEvent;
        public event Action<Tower> OnPostSellingTowerEvent;


        private void Start()
        {
            EventSystemListener.main.AddListener(gameObject);
        }


        public void OnPlacingTower(TowerData towerData)
        {
            OnPlacingTowerEvent?.Invoke(towerData);
        }
        public void OnSuccessPlacedTower(TowerData towerData, GameObject placedTower)
        {
            OnSuccessPlacedTowerEvent?.Invoke(towerData, placedTower);
        }
        public void OnFailedPlacedTower(TowerData towerData, PLACE_TOWER_FAIL_REASON reason)
        {
            FailedPlacedTowerEvent?.Invoke(towerData, reason);
        }

        public void OnPreUpgradeTower(UpgradeableTower tower)
        {
            OnPreUpgradeTowerEvent?.Invoke(tower);
        }
        public void OnSuccessUpgradedTower(UpgradeableTower tower)
        {
            OnSuccessUpgradedTowerEvent?.Invoke(tower);
        }
        public void OnFailedUpgradedTower(UpgradeableTower tower, UPGRADE_TOWER_RESULT reason)
        {
            OnFailedUpgradedTowerEvent?.Invoke(tower, reason);
        }

        public void OnPostSellingTower(Tower tower)
        {
            OnPostSellingTowerEvent?.Invoke(tower);
        }
        public void OnPreSellingTower(Tower tower)
        {
            OnPreSellingTowerEvent?.Invoke(tower);
        }
    }

}