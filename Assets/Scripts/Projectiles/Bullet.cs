using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected bool autoDestroy = true;
    [SerializeField] protected float destroyAfter = 3f;

    protected bool alreadyHit = false;
    protected float speed = 7f;
    protected float bulletPower = 1f;
    
    protected float autoDestroyTimer = 0f;
    protected Vector3 targetPos;

    ShootingTower tower = null;

    public float BulletPower { get => bulletPower; set => bulletPower = value; }

    void Start()
    {
        tower = GetComponentInParent<ShootingTower>();
        if (tower != null)
        {
            ChangePower(tower.power);
        }
    }

    protected virtual void Update()
    {
        if (autoDestroy)
        {
            autoDestroyTimer += Time.deltaTime;
            if (autoDestroyTimer >= destroyAfter)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    public void AimTo(Transform enemy, float bulletSpeed, float bulletPower)
    {
        this.speed = bulletSpeed;
        this.bulletPower = bulletPower;

        if (enemy == null) { return; }
        transform.LookAt(enemy);
        targetPos = enemy.position;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        alreadyHit = true;
    }
    
    protected virtual void OnEnable()
    {
        alreadyHit = false;
        autoDestroyTimer = 0f;
    }

    public void ChangePower(float amount)
    {
        BulletPower = amount;
    }
}

