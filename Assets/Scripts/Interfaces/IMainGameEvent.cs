using UnityEngine.EventSystems;

public interface IMainGameEvent : IEventSystemHandler
{
    void OnGameOverWin();
    void OnGameOverLose();
}
