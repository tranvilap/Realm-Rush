using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee;


[RequireComponent(typeof(Map))]
public class SpawningController : MonoBehaviour
{

    public void SpawMinorWavelist(MinorWave minorWave)
    {
        Debug.Log("Spawning");
        StartCoroutine(MinorWaveSpawning(minorWave));
    }

    public void SpawnWave(Wave wave)
    {
        StartCoroutine(WaveSpawning(wave));
    }

    IEnumerator WaveSpawning(Wave wave)
    {
        foreach (var minorWave in wave.minorWaves)
        {
            for (int i = 0; i < minorWave.EnemyNumber; i++)
            {
                GameObject go = Instantiate(minorWave.EnemyPrefab);
                yield return new WaitForSeconds(minorWave.TimeDelayBetweenSpawns);
            }
            yield return new WaitForSeconds(minorWave.TimeDelayNextWave);
        }
    }
    IEnumerator MinorWaveSpawning(MinorWave minorWave)
    {
        for (int i = 0; i < minorWave.EnemyNumber; i++)
        {
            GameObject go = Instantiate(minorWave.EnemyPrefab);
            yield return new WaitForSeconds(minorWave.TimeDelayBetweenSpawns);
        }
        yield return new WaitForSeconds(minorWave.TimeDelayNextWave);
    }
}


