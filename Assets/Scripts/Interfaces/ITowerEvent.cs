using UnityEngine.EventSystems;

public interface ITowerEvent : IEventSystemHandler
{
    void OnSellingTower(Tower tower);
}
