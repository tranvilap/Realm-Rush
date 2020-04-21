using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyIconAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText = null;

    PlayerHQ playerHQ;
    Animator animator;

    const string addMoneyTrigger = "AddMoney";
    const string loseMoneyTrigger = "LoseMoney";

    int addMoneyHash;
    int loseMoneyHash;

    private void OnEnable()
    {
        if(playerHQ == null)
        {
            playerHQ = FindObjectOfType<PlayerHQ>();
        }
        playerHQ.OnAddMoneyEvent += OnAddMoney;
        playerHQ.OnDeductMoneyEvent += OnDeductMoney;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        addMoneyHash = Animator.StringToHash(addMoneyTrigger);
        loseMoneyHash = Animator.StringToHash(loseMoneyTrigger);
    }

    void OnAddMoney(int amount)
    {
        amountText.text = "+" + amount.ToString();
        animator.SetTrigger(addMoneyHash);
    }

    void OnDeductMoney(int amount)
    {
        amountText.text = "-" + amount.ToString();
        animator.SetTrigger(loseMoneyHash);
    }

}
