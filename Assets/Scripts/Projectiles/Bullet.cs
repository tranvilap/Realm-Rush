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

    float timer = 0f;

    Vector3 targetPos;

    public float BulletPower { get => bulletPower; set => bulletPower = value; }

    public void ShootTo(Transform enemy, float bulletSpeed, float bulletPower)
    {
        this.target = enemy;
        this.speed = bulletSpeed;
        this.bulletPower = bulletPower;
        if (enemy == null) { return; }
        transform.LookAt(enemy);
        targetPos = enemy.position;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (target == null) { return; }
        FlyToTarget();
    }

    void FlyToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (autoDestroy)
        {
            timer += Time.deltaTime;
            if (timer >= destroyAfter)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(OnHitCollider());
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit(BulletPower);
        }
    }

    IEnumerator OnHitCollider()
    {
        target = null;
        projectileParticle.gameObject.SetActive(false);
        onHitParticle.Play();
        yield return new WaitForSeconds(onHitParticle.main.duration);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        timer = 0f;
    }
    private void OnEnable()
    {
        projectileParticle.gameObject.SetActive(true);
    }
}
