using UnityEngine.EventSystems;

public interface IEnemyEvent : IEventSystemHandler
{
    void OnEnemySpawned(Enemy enemy);
    void OnEnemyReachedGoal(Enemy enemy);
    void OnEnemyDie(Enemy enemy);
}
