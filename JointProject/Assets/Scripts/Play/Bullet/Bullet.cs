using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SIDE Side = SIDE.NONE;

    public List<string> wallTag = new List<string>();

    public string groundTag = "Ground";

    public void Explosion()
    {
        Destroy(gameObject);
    }
}
