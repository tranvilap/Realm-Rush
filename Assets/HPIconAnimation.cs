using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPIconAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText = null;

    PlayerHQ playerHQ;
    Animator animator;

    const string raiseHPTrigger = "RaiseHP";
    const string loseHpTrigger = "LoseHP";

    int raiseHPHash;
    int loseHPHash;

    private void OnEnable()
    {
        if (playerHQ == null)
        {
            playerHQ = FindObjectOfType<PlayerHQ>();
        }
        playerHQ.TookDamage += OnHQTakeDamage;

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        raiseHPHash = Animator.StringToHash(raiseHPTrigger);
        loseHPHash = Animator.StringToHash(loseHpTrigger);
    }

    void OnHeal(int amount)
    {
        amountText.text = "+" + amount.ToString();
        animator.SetTrigger(raiseHPHash);
    }


    public void OnHQTakeDamage(int amount)
    {
        amountText.text = "-" + amount.ToString();
        animator.SetTrigger(loseHPHash);
    }

    private void OnDisable()
    {
        playerHQ.TookDamage -= OnHQTakeDamage;
    }
}
