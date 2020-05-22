using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee;
using System;

public class SpawningController : MonoBehaviour
{
    [SerializeField] Wave[] waves = null;

    public event Action OnSpawnedNextWave;

    private int waveIndex = -1;
    private PlayerHQ playerHQ;
    GameController gameController;
    Map map;

    private bool isSpawning = false;
    public bool IsSpawning { get => isSpawning; }

    private void Start()
    {
        playerHQ = FindObjectOfType<PlayerHQ>();
        gameController = FindObjectOfType<GameController>();
        map = FindObjectOfType<Map>();
    }

    Wave GetNextWave()
    {
        CurrentWaveIndex++;
        if (CurrentWaveIndex >= waves.Length)
        {
            return null;
        }
        else
        {
            return waves[CurrentWaveIndex];
        }
    }

    public Wave NextWave
    {
        get
        {
            if (CurrentWaveIndex + 1 >= waves.Length)
            {
                return null;
            }
            return waves[CurrentWaveIndex + 1];
        }
    }

    public Wave CurrentWave
    {
        get
        {
            if (CurrentWaveIndex < 0)
            {
                return null;
            }
            return waves[CurrentWaveIndex];
        }
    }

    public bool IsFinalWave { get => CurrentWaveIndex == (waves.Length - 1); }
    public int CurrentWaveIndex { get => waveIndex; private set => waveIndex = value; }
    public int WaveQuantity { get => waves.Length; }

    public void SpawMinorWavelist(MinorWave minorWave)
    {
        if (isSpawning) { return; }
        StartCoroutine(MinorWaveSpawning(minorWave));
    }

    public void SpawnWave(Wave wave)
    {
        if (isSpawning) { return; }
        StartCoroutine(WaveSpawning(wave));
    }

    public void SpawNextWave()
    {
        if (isSpawning) { return; }
        if(FindObjectOfType<Enemy>() != null) { return; }
        var nextWave = GetNextWave();
        if (nextWave != null)
        {
            SpawnWave(nextWave);
            OnSpawnedNextWave?.Invoke();
        }
        else
        {
            Debug.LogWarning("Couldn't get next wave");
        }
    }

    public void SetIsSpawning(bool value)
    {
        isSpawning = value;
    }

    IEnumerator WaveSpawning(Wave wave)
    {
        isSpawning = true;
        foreach (var minorWave in wave.minorWaves)
        {
            gameController.AddAliveEnemy(minorWave.EnemyNumber);
        }
        foreach (var minorWave in wave.minorWaves)
        {
            WaypointPath path = map.GetPath(minorWave.PathIndex);
            for (int i = 0; i < minorWave.EnemyNumber; i++)
            {
                GameObject go = Instantiate(minorWave.EnemyPrefab, 
                    path.StartWaypoint.transform.position,
                    Quaternion.identity);
                var enemyScript = go.GetComponent<Enemy>();
                if(enemyScript != null)
                {
                    enemyScript.SetPath(path);
                }
                yield return new WaitForSeconds(minorWave.TimeDelayBetweenSpawns);
            }
            yield return new WaitForSeconds(minorWave.TimeDelayNextWave);
        }
        isSpawning = false;
    }

    IEnumerator MinorWaveSpawning(MinorWave minorWave)
    {
        isSpawning = true;
        for (int i = 0; i < minorWave.EnemyNumber; i++)
        {
            GameObject go = Instantiate(minorWave.EnemyPrefab);
            yield return new WaitForSeconds(minorWave.TimeDelayBetweenSpawns);
        }
        yield return new WaitForSeconds(minorWave.TimeDelayNextWave);
        isSpawning = false;
    }
}


