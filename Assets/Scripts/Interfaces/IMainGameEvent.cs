using UnityEngine.EventSystems;

public interface IMainGameEvent : IEventSystemHandler
{
    void OnHQTakeDamage(PlayerHQ playerHQ);
    void OnGameOverWin();
    void OnGameOverLose();
}
