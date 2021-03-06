using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalBullet : Bullet
{
    private int reboundCount = 0;

    private Vector3? hitPosition = null;

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag;
        if (collision.transform.parent != null)
        {
            collisionTag = collision.transform.parent.gameObject.tag;
        }
        else
        {
            collisionTag = collision.gameObject.tag;
        }
        Debug.Log(collisionTag);
        if (wallTag.Contains(collisionTag))
        {
            reboundCount++;
            if (reboundCount >= 2 && hitPosition != transform.position)
            {
                Explosion();
            }
            hitPosition = transform.position;
        }
        if (collisionTag == groundTag)
        {
            Explosion();
        }
    }
}
