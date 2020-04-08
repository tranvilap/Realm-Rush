using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem projectileParticle = null;
    [SerializeField] ParticleSystem onHitParticle = null;
    [SerializeField] protected bool autoDestroy = true;
    [SerializeField] protected float destroyAfter = 3f;

    protected float speed = 7f;
    protected float bulletPower = 1f;
    
    public Transform target = null;

    protected float timer = 0f;
    protected Vector3 targetPos;

    ShootingTower tower = null;

    public float BulletPower { get => bulletPower; set => bulletPower = value; }

    void Start()
    {
        tower = GetComponentInParent<ShootingTower>();
        if (tower != null)
        {
            ChangePower(tower.Power);
        }
    }

    protected virtual void Update()
    {
        if(target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (autoDestroy)
        {
            timer += Time.deltaTime;
            if (timer >= destroyAfter)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    public void AimTo(Transform enemy, float bulletSpeed, float bulletPower)
    {
        this.target = enemy;
        this.speed = bulletSpeed;
        this.bulletPower = bulletPower;

        if (enemy == null) { return; }
        transform.LookAt(enemy);
        targetPos = enemy.position;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HitExplosion());
        target = null;
    }

    IEnumerator HitExplosion()
    {
        projectileParticle.gameObject.SetActive(false);
        onHitParticle.Play();
        yield return new WaitForSeconds(onHitParticle.main.duration);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        timer = 0f;
    }

    protected virtual void OnEnable()
    {
        projectileParticle.gameObject.SetActive(true);
    }

    public void ChangePower(float amount)
    {
        BulletPower = amount;
    }
}

