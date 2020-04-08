using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    public static Player instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
}
