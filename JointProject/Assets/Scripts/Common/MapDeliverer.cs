using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapDeliverer")]
public class MapDeliverer : ScriptableObject
{
    private GameObject map = null;

    private int level = 0;

    public GameObject Map
    {
        get { return map; }
        set { map = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}
