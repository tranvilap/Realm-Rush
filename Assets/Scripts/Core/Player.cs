using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEnemyEvent
{
    [SerializeField] int playerHealth = 10;

    private void Start()
    {
        EventSystemListener.main.AddListener(gameObject);
    }

    public void TakeDamage(int amount)
    {
        if(playerHealth> 0)
        {
            playerHealth -= amount;
            if(playerHealth <= 0)
            {
                playerHealth = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.LogError("Player died");
    }

    public void OnEnemyReachedGoal(Enemy enemy)
    {
        Debug.Log("Enemy reached goal");
        TakeDamage(enemy.Damage);
    }

    public void OnEnemyDie(Enemy enemy)
    {
    }
}
