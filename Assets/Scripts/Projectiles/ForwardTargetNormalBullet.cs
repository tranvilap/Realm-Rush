using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTargetNormalBullet : NormalBullet
{
    protected IEnumerator FlyForward()
    {
        while (!alreadyHit)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            yield return null;
        }
        
    }
    public override void Shoot()
    {
        StartCoroutine(FlyForward());
    }
}
