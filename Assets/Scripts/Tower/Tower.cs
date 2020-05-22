using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Stats;
using TowerBuffs;
using TowerEvents;
[SelectionBase]
public abstract class Tower : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] protected BaseStat effectRangeRadius;
    [SerializeField] protected GameObject rangeEffectField = null;
    [Space(10f)]
    [Tooltip("Must be assign with the value in Tower Data price, can be change in run time")]
    [SerializeField] private int summonPrice = 0;

    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected Canvas menuCanvas = null;
    [SerializeField] protected Color gizmoColor = Color.red;

    [Min(0)] protected int towerTotalValue = 0;

    public virtual BaseStat EffectRangeRadius { get => effectRangeRadius; set => effectRangeRadius = value; }
    public virtual LayerMask WhatIsTarget { get => whatIsTarget; set => whatIsTarget = value; }
    public virtual int SellingPrice { get => (int)(towerTotalValue * 0.8f); }
    public Canvas MenuCanvas { get => menuCanvas; set => menuCanvas = value; }

    [HideInInspector] public List<BaseTowerBuff> receivingBuffs = new List<BaseTowerBuff>();
    protected ObjectPooler bulletPooler;
    protected PlayerHQ playerHQ;
    protected TowerEvents.TowerEvents towerEvents;
    [HideInInspector]public TowerPlacePoint placingPoint;
    protected BoxCollider towerCollider;

    protected Camera mainCamera;
    protected InputsHandler inputsHandler;


    protected virtual void Start()
    {
        bulletPooler = GetComponent<ObjectPooler>();
        playerHQ = FindObjectOfType<PlayerHQ>();
        towerEvents = FindObjectOfType<TowerEvents.TowerEvents>();
        towerTotalValue = summonPrice;
        inputsHandler = FindObjectOfType<InputsHandler>();
        towerCollider = GetComponent<BoxCollider>();
        var parent = GameObject.Find("Towers");
        if(parent == null)
        {
            parent = new GameObject("Towers");
            parent.transform.position = Vector3.zero;
        }
        transform.parent = parent.transform;
    }
    
    public virtual void PlaceTowerAt(TowerPlacePoint point)
    {
        placingPoint = point;
    }

    public virtual void SellTower()
    {
        PreSellingTower();
        placingPoint.IsPlaceable = true;
        Destroy(gameObject);
        playerHQ.EarnMoney(SellingPrice);
        var sellingVFX = SharedObjectPooler.main.GetPooledObject(Constants.SELL_TOWER_VFX);
        if(sellingVFX != null)
        {
            sellingVFX.transform.position = new Vector3(transform.position.x, sellingVFX.transform.position.y, transform.position.z);
            sellingVFX.SetActive(true);
        }
        PostSellingTower();
    }
    protected virtual void PreSellingTower()
    {
        towerEvents.OnPreSellingTower(this);
    }
    protected virtual void PostSellingTower()
    {
        towerEvents.OnPostSellingTower(this);
    }
    public virtual void OnSellTowerButton()
    {
        SellTower();
    }

    public virtual void OpenTowerMenu()
    {
        MenuCanvas.gameObject.SetActive(true);
        ShowEffectRange();
    }

    public virtual void CloseTowerMenu()
    {
        MenuCanvas.gameObject.SetActive(false);
        HideEffectRange();
    }

    public virtual void AddBuff(BaseTowerBuff towerBuff, int buffLevel)
    {
        towerBuff.SetTargetTower(this, buffLevel);
        towerBuff.ApplyBuffsToTarget();

        receivingBuffs.Add(towerBuff);
    }

    public virtual void RemoveBuff(BaseTowerBuff towerBuff)
    {
        towerBuff.RemoveTargetBuffs();

        if(towerBuff is InstantBuff)
        {
            var buffSourceTower = ((InstantBuff)towerBuff).SourceTower;
            if (buffSourceTower!= null)
            {
                buffSourceTower.GivingBuffs.Remove(towerBuff);
            }
        }

        Destroy(towerBuff.gameObject);

        receivingBuffs.Remove(towerBuff);
    }

    protected abstract void SeekTarget();

    public virtual void ShowEffectRange()
    {
        if (rangeEffectField == null) { return; }
        rangeEffectField.transform.position = transform.position;
        float rangeDiameter = EffectRangeRadius.CalculatedValue * 2;
        rangeEffectField.transform.localScale = new Vector3(rangeDiameter, rangeDiameter, rangeDiameter);
        rangeEffectField.SetActive(true);
    }

    public virtual void HideEffectRange()
    {
        if(rangeEffectField == null) { return; }
        rangeEffectField.SetActive(false);
    }

    protected void SetUpCollider(BoxCollider targetBoxCollider)
    {
        towerCollider.size = targetBoxCollider.size;
        towerCollider.center = targetBoxCollider.center;
    }

    private void OnDrawGizmos()
    {
        if(EffectRangeRadius.CalculatedValue <= 0) { return; }
        Gizmos.color = gizmoColor;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, effectRangeRadius.CalculatedValue);
    }

}
