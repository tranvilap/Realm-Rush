using UnityEngine;
using Stats;
using Game.Sound;

[RequireComponent(typeof(ObjectPooler))]
public abstract class ShootingTower : UpgradeableTower
{
    [Header("Shooting Info")]
    [SerializeField] protected BaseStat firingRate;
    [SerializeField] protected BaseStat bulletSpeed;
    [SerializeField] protected BaseStat power;
    [SerializeField] protected Transform shootingPoint = null;

    [Space(25f)]
    [SerializeField] protected SFXObj onShootSFX = null;

    public BaseStat FiringRate { get => firingRate; }
    public BaseStat BulletSpeed { get => bulletSpeed; }
    public BaseStat Power { get => power;}

    protected AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    protected Bullet PrepareBullet()
    {
        var bullet = bulletPooler.GetObject();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        return bullet.GetComponent<Bullet>();
    }
    protected Bullet PrepareBulletAt(Transform target)
    {
        var bullet = bulletPooler.GetObject();
        if (bullet == null) { return null; }
        bullet.transform.position = target.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public abstract void Shoot();

}
