using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee;
using System;

public class SpawningController : MonoBehaviour
{
    [SerializeField] Wave[] waves = null;
    

    private int waveIndex = -1;
    private PlayerHQ playerHQ;
    GameController gameController;

    private bool isSpawning = false;
    public bool IsSpawning { get => isSpawning; }

    private void Start()
    {
        playerHQ = FindObjectOfType<PlayerHQ>();
        gameController = FindObjectOfType<GameController>();
    }

    Wave GetNextWave()
    {
        waveIndex++;
        if (waveIndex >= waves.Length)
        {
            return null;
        }
        else
        {
            return waves[waveIndex];
        }
    }

    public Wave NextWave
    {
        get
        {
            if (waveIndex + 1 >= waves.Length)
            {
                return null;
            }
            return waves[waveIndex + 1];
        }
    }

    public Wave CurrentWave
    {
        get
        {
            if (waveIndex < 0)
            {
                return null;
            }
            return waves[waveIndex];
        }
    }

    public bool IsFinalWave { get => waveIndex == (waves.Length - 1); }

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
        var nextWave = GetNextWave();
        if (nextWave != null)
        {
            SpawnWave(nextWave);
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
            for (int i = 0; i < minorWave.EnemyNumber; i++)
            {
                GameObject go = Instantiate(minorWave.EnemyPrefab);
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


