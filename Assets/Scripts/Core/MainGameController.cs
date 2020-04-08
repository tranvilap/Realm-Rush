using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    SpawningController spawningController;
    [SerializeField] Wave[] waves;
    // Start is called before the first frame update
    void Start()
    {
        spawningController = GetComponent<SpawningController>();
        spawningController.SpawnWave(waves[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
