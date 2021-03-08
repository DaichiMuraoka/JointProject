using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapDeliverer")]
public class MapDeliverer : ScriptableObject
{
    [SerializeField] private GameObject[] maps = null;

    private int level = 0;

    public GameObject Map
    {
        get { return maps[level]; }
    }
    
    public int LevelMax
    {
        get { return maps.Length - 1; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}
