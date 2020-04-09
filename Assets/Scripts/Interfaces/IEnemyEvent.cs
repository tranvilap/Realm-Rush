using UnityEngine.EventSystems;

public interface IEnemyEvent : IEventSystemHandler
{
    void OnEnemyReachedGoal(Enemy enemy);
    void OnEnemyDie(Enemy enemy);
}
