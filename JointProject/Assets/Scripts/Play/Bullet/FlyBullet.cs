using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBullet : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        if(GetComponent<Rigidbody>().velocity.y > 0)
        {
            return;
        }
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Wall" || collisionTag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
