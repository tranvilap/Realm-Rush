using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] private float effectRadius = 2f;
    [SerializeField] LayerMask whatIsTarget;
    [SerializeField] Canvas menuCanvas = null;

    [Tooltip("Must be assign with the value in Tower Data price, can be change in run time")]
    [SerializeField] private int summonPrice = 0;

    [Min(0)] protected int towerTotalValue = 0;

    public float EffectRadius { get => effectRadius; set => effectRadius = value; }
    public LayerMask WhatIsTarger { get => whatIsTarget; set => whatIsTarget = value; }
    public virtual int SellingPrice { get => (int)(towerTotalValue * 0.8f); }
    public Canvas MenuCanvas { get => menuCanvas; set => menuCanvas = value; }

    protected ObjectPooler bulletPooler;
    protected PlayerHQ playerHQ;
    public TowerPlacePoint placingPoint;

    protected Camera mainCamera;
    protected InputsHandler inputsHandler;

    protected virtual void Start()
    {
        bulletPooler = GetComponent<ObjectPooler>();
        playerHQ = FindObjectOfType<PlayerHQ>();
        towerTotalValue = summonPrice;
        inputsHandler = FindObjectOfType<InputsHandler>();
        //CloseTowerMenu();
    }
    
    public virtual void PlaceTowerAt(TowerPlacePoint point)
    {
        placingPoint = point;
    }

    public virtual void SellTower()
    {
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<ITowerEvent>(go, null, (x, y) => x.OnSellingTower(this));
        }
        placingPoint.IsPlaceable = true;
        Destroy(gameObject);
    }

    public virtual void OnSellTowerButton()
    {
        SellTower();
    }

    public virtual void OpenTowerMenu()
    {
        MenuCanvas.gameObject.SetActive(true);
    }

    public virtual void CloseTowerMenu()
    {
        MenuCanvas.gameObject.SetActive(false);
    }

    protected abstract void SeekTarget();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }

}
