using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour, IEnemyEvent
{
    public TowerData[] bringingTowers;
    public event Action OnCompleteOneWave;
    SpawningController spawningController;
    EnemyBase enemyBase;
    int aliveEnemies = 0;
    bool gameOver = false;

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
        Time.timeScale = 1f;
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

    private void RemoveDeadEnemy(Enemy enemy)
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
                    OnCompleteOneWave?.Invoke();
                }
            }
        }
        Debug.Log("Alive enemies: " + AliveEnemies);
    }

    public void GameOverLose()
    {
        if (gameOver) { return; }
        gameOver = true;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IMainGameEvent>(go, null, (x, y) => x.OnGameOverLose());
        }
    }

    public void GameOverWin()
    {
        if (gameOver) { return; }
        gameOver = true;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IMainGameEvent>(go, null, (x, y) => x.OnGameOverWin());
        }
    }

    public void OnEnemyReachedGoal(Enemy enemy)
    {
        RemoveDeadEnemy(enemy);
    }

    public void OnEnemyDie(Enemy enemy)
    {
        RemoveDeadEnemy(enemy);
    }

    public void OnEnemySpawned(Enemy enemy) { }
}
