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
        playerHQ.OnEarningMoneyEvent += OnMoneyEarning;
        playerHQ.OnSpendingMoneyEvent += OnMoneySpending;
        playerHQ.OnTakingDamage += OnHealthDecrement;


    }
    private void OnMoneyEarning(int amount)
    {
        var go = goldCoin.GetObject();
        if (go == null) { return; }
        go.GetComponent<IconAnimations>().PlayIncrementAnimation(amount);
    }
    private void OnMoneySpending(int amount)
    {
        var go = goldCoin.GetObject();
        if (go == null) { return; }
        go.GetComponent<IconAnimations>().PlayDecrementAnimation(amount);
    }
    private void OnHealthIncrement(int amount) { }
    private void OnHealthDecrement(int amount)
    {
        var go = health.GetObject();
        if (go == null) { return; }
        go.GetComponent<IconAnimations>().PlayDecrementAnimation(amount);
    }
}
