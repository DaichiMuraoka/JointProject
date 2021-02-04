using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalBullet : Bullet
{
    private int reboundCount = 0;
    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Wall")
        {
            reboundCount++;
            if (reboundCount >= 2)
            {
                Destroy(gameObject);
            }
        }
        if (collisionTag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
