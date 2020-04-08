using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private float effectRadius = 2f;
    [SerializeField] LayerMask whatIsTarget;

    public float EffectRadius { get => effectRadius; set => effectRadius = value; }
    public LayerMask WhatIsTarger { get => whatIsTarget; set => whatIsTarget = value; }

    protected BulletPooler ammoPooler;


    protected virtual void Start()
    {
        ammoPooler = GetComponent<BulletPooler>();
    }
    protected abstract void SeekTarget();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}
