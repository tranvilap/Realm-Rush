using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour, IEnemyEvent
{
    SpawningController spawningController;
    int aliveEnemies = 0;
    bool gameOver = false;
    public TowerData[] bringingTowers;

    public int AliveEnemies
    {
        get => aliveEnemies;
        set
        {
            if (value <= 0) { aliveEnemies = 0; }
            else { aliveEnemies = value; }
        }
    }

    private void Start()
    {
        spawningController = FindObjectOfType<SpawningController>();
        EventSystemListener.main.AddListener(gameObject);
        RecheckAliveEnemies();
    }


    private void RecheckAliveEnemies()
    {
        var foundEnemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in foundEnemies)
        {
            if (!enemy.isDead && !enemy.atGoal)
            {
                AddAliveEnemy(1);
            }
        }
    }

    public void AddAliveEnemy(int quantity)
    {
        AliveEnemies += quantity;
        Debug.Log(AliveEnemies);
    }

    private void RemoveAliveEnemy(Enemy enemy)
    {
        AliveEnemies -= 1;
        if (AliveEnemies <= 0)
        {
            RecheckAliveEnemies();
            if (AliveEnemies <= 0)
            {
                if (spawningController.IsFinalWave)
                {
                    if (FindObjectOfType<PlayerHQ>().HQHealth > 0)
                    {
                        GameOverWin();
                    }
                }
                else
                {
                    Debug.LogWarning("Still have more waves");
                }
            }
        }
        Debug.Log(AliveEnemies);
    }

    public void GameOverLose()
    {
        if (gameOver) { return; }
        gameOver = true;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IMainGameEvent>(go, null, (x, y) => x.OnGameOverLose());
        }
        Debug.LogError("Game Over Lose");
    }

    public void GameOverWin()
    {
        if (gameOver) { return; }
        gameOver = true;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IMainGameEvent>(go, null, (x, y) => x.OnGameOverWin());
        }
        Debug.LogError("WIN");
    }

    public void OnEnemyReachedGoal(Enemy enemy)
    {
        RemoveAliveEnemy(enemy);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        RemoveAliveEnemy(enemy);
    }

    public void OnEnemySpawned(Enemy enemy) { }
}
