using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapDeliverer")]
public class MapDeliverer : ScriptableObject
{
    private GameObject map = null;

    public GameObject Map
    {
        get { return map; }
        set { map = value; }
    }
}
