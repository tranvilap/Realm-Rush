using UnityEngine.EventSystems;

public interface ITowerEvent : IEventSystemHandler
{
    void OnSellingTower(Tower tower);
    void OnUpgradeTower(Tower tower, int moneyToUpgrade);
}
