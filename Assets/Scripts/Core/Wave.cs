using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee;
using System;

[Serializable]
public class MinorWave
{
    [SerializeField] [Min(0f)] int enemyNumber = 1;
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] [Min(0f)] float timeDelayBetweenSpawns = 0.5f;
    [SerializeField] [Min(0f)] float timeDelayNextWave = 1f;
    [SerializeField] [Min(-1f)][Tooltip("-1 for random path")] int pathIndex = 0;

    public int EnemyNumber { get => enemyNumber; set => enemyNumber = value; }
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float TimeDelayBetweenSpawns { get => timeDelayBetweenSpawns; set => timeDelayBetweenSpawns = value; }
    public float TimeDelayNextWave { get => timeDelayNextWave; set => timeDelayNextWave = value; }
    public int PathIndex { get => pathIndex; }
}

[Serializable]
public class MinorWaveList : ReorderableArray<MinorWave> { }

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    [Reorderable(sortable = false)]
    public MinorWaveList minorWaves;
}