using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyForwardTagetBullet : Bullet
{
    protected override void Update()
    {
        base.Update();
        FlyForward();
    }

    protected void FlyForward()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
