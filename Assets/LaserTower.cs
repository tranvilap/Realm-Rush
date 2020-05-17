using Game.Sound;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserTower : UpgradeableTower
{
    [SerializeField] protected BaseStat power;
    [SerializeField] protected Transform shootingPoint = null;

    [Space(25f)]
    [SerializeField] protected SFXObj onShootSFX = null;
    protected AudioSource audioSource;

    public BaseStat Power { get => power; }

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void Shoot();

}
