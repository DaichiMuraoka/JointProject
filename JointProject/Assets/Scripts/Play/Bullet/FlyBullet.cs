using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBullet : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == wallTag || collisionTag == groundTag)
        {
            Explosion();
        }
    }
}
