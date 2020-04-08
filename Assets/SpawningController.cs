using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee;
[RequireComponent(typeof(Map))]
public class SpawningController : MonoBehaviour
{
    [SerializeField] [Reorderable(sortable =false)] WaveList waves;
    Map map;
    Waypoint startWaypoint = null, endWayPoint = null;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Map>();
        StartCoroutine(EnemySpawning());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawning()
    {
        foreach(var wave in waves)
        {
            for(int i = 0; i<wave.EnemyNumber; i++)
            {
                GameObject go = Instantiate(wave.EnemyPrefab);
                yield return new WaitForSeconds(wave.TimeDelayBetweenSpawns);
            }
            yield return new WaitForSeconds(wave.TimeDelayNextWave);
        }
    }
}

[System.Serializable]
public class Wave 
{
    [SerializeField][Min(0f)] int enemyNumber = 1;
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField][Min(0f)] float timeDelayBetweenSpawns = 0.5f;
    [SerializeField][Min(0f)] float timeDelayNextWave = 1f;

    public int EnemyNumber { get => enemyNumber; set => enemyNumber = value; }
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float TimeDelayBetweenSpawns { get => timeDelayBetweenSpawns; set => timeDelayBetweenSpawns = value; }
    public float TimeDelayNextWave { get => timeDelayNextWave; set => timeDelayNextWave = value; }
}
[System.Serializable]
public class WaveList : ReorderableArray<Wave>{}