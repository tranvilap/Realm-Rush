using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPooler))]
public abstract class TowerGun : MonoBehaviour, IShootable
{
    [SerializeField] private float firingRate = 0.3f;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float power = 1f;
    protected BulletPooler ammoPooler;

    public float Power { get => power; set => power = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float FiringRate { get => firingRate; set => firingRate = value; }

    protected virtual void Start()
    {
        ammoPooler = GetComponent<BulletPooler>();
    }

    protected abstract void SeekEnemy();
    public abstract void Shoot();
}
