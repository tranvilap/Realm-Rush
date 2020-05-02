using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] GameObject enemyBaseMenu = null;
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
        enemyBaseMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        enemyBaseMenu.SetActive(false);
    }

    private void OnDisable()
    {
        gameController.OnCompleteOneWave -= OpenMenu;
        spawningController.OnSpawnedNextWave -= CloseMenu;
    }
}
