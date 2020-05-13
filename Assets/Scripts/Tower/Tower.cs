using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Stats;
using TowerBuffs;
[SelectionBase]
public abstract class Tower : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] private BaseStat effectRangeRadius;
    [SerializeField] LayerMask whatIsTarget;
    [SerializeField] Canvas menuCanvas = null;
    [SerializeField] Color gizmoColor = Color.red;

    [Tooltip("Must be assign with the value in Tower Data price, can be change in run time")]
    [SerializeField] private int summonPrice = 0;

    [Min(0)] protected int towerTotalValue = 0;

    public virtual BaseStat EffectRangeRadius { get => effectRangeRadius; set => effectRangeRadius = value; }
    public virtual LayerMask WhatIsTarget { get => whatIsTarget; set => whatIsTarget = value; }
    public virtual int SellingPrice { get => (int)(towerTotalValue * 0.8f); }
    public Canvas MenuCanvas { get => menuCanvas; set => menuCanvas = value; }

    [HideInInspector] public List<BaseTowerBuff> receivingBuffs = new List<BaseTowerBuff>();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, effectRangeRadius.CalculatedValue);
    }

}
