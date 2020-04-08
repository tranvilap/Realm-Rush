using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPooler))]
public abstract class ShootingTower : Tower, IShootable
{
    [SerializeField] private float firingRate = 0.3f;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float power = 1f;

    public float Power { get => power; set => power = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float FiringRate { get => firingRate; set => firingRate = value; }

    public abstract void Shoot();
}
