using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBullet : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.transform.parent.gameObject.tag;
        Debug.Log(collisionTag);
        if (wallTag.Contains(collisionTag) || collisionTag == groundTag)
        {
            Explosion();
        }
    }
}
