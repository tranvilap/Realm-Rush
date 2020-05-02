using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] GameObject enemyMenu;
    GameController gameController;
    SpawningController spawningController;
    private void OnEnable()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
        if(spawningController == null)
        {
            spawningController = FindObjectOfType<SpawningController>();
        }
        gameController.OnCompleteOneWave += OpenMenu;
        spawningController.OnSpawnedNextWave += CloseMenu;
    }
    private void Start()
    {
        OpenMenu();
    }
    public void OpenMenu()
    {
        enemyMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        enemyMenu.SetActive(false);
    }

    private void OnDisable()
    {
        gameController.OnCompleteOneWave -= OpenMenu;
        spawningController.OnSpawnedNextWave -= CloseMenu;
    }
}
