using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected bool autoDestroy = true;
    [SerializeField] protected float destroyAfter = 3f;

    protected bool alreadyHit = false;
    protected float speed = 7f;
    protected float bulletPower = 1f;

    protected float autoDestroyTimer = 0f;
    protected Vector3 targetPos;

    public float BulletPower { get => bulletPower; set => bulletPower = value; }

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

    public virtual void AimTo(Transform aimFrom, Transform enemy, float bulletSpeed, float bulletPower)
    {
        this.speed = bulletSpeed;
        this.bulletPower = bulletPower;

        if (enemy == null) { return; }
        Vector3 enemyVelo = Vector3.zero;
        if (enemy.GetComponent<Rigidbody>())
        {
            enemyVelo = enemy.GetComponent<Rigidbody>().velocity;
        }
        targetPos = FirstOrderIntercept(aimFrom.position, Vector3.zero, bulletSpeed, enemy.position, enemyVelo);
        transform.LookAt(targetPos);
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

    public abstract void Shoot();

    #region Intercept
    public static Vector3 FirstOrderIntercept
(
    Vector3 shooterPosition,
    Vector3 shooterVelocity,
    float shotSpeed,
    Vector3 targetPosition,
    Vector3 targetVelocity
)
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }
    #endregion
}

