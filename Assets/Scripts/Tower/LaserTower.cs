using Game.Sound;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserTower : UpgradeableTower
{
    [SerializeField] protected BaseStat dps;
    protected Transform currentShootingPoint = null;

    protected AudioSource audioSource;

    public BaseStat DPS { get => dps; }

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void Shoot();
    protected virtual void DealDamagePerSecond(Enemy enemy)
    {
        enemy.GetHit(dps.CalculatedValue * Time.deltaTime);
    }
}
