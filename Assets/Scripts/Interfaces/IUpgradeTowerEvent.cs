using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IUpgradeTowerEvent : IEventSystemHandler
{
    void OnUpgradeTower(UpgradeableTower tower, int moneyToUpgrade);
}
