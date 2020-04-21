using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAnimationController : MonoBehaviour
{
    [SerializeField] ObjectPoolerKai goldCoin = null;
    [SerializeField] ObjectPoolerKai health = null;

    PlayerHQ playerHQ;

    private void OnEnable()
    {
        if (playerHQ == null)
        {
            playerHQ = FindObjectOfType<PlayerHQ>();
        }
        playerHQ.OnAddMoneyEvent += OnMoneyIncrement;
        playerHQ.OnDeductMoneyEvent += OnMoneyDecrement;
        playerHQ.TookDamage += OnHealthDecrement;


    }
    private void OnMoneyIncrement(int amount)
    {
        var go = goldCoin.GetObject();
        if (go == null) { return; }
        go.GetComponent<IconAnimations>().PlayIncrementAnimation(amount);
    }
    private void OnMoneyDecrement(int amount)
    {
        var go = goldCoin.GetObject();
        if (go == null) { return; }
        go.GetComponent<IconAnimations>().PlayDecrementAnimation(amount);
    }
    private void OnHealthIncrement(int amount) { }
    private void OnHealthDecrement(int amount) { }
}
