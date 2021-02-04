using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SIDE Side = SIDE.NONE;

    public void Explosion()
    {
        Destroy(gameObject);
    }
}
