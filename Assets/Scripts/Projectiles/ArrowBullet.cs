using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : ForwardTargetNormalBullet
{
    [SerializeField] private TrailRenderer trailRenderer = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        trailRenderer.Clear();
    }
}
