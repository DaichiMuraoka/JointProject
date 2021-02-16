using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ModeSetting")]
public class ModeSetting : ScriptableObject
{
    private int playerCount = 0;

    public int PlayerCount
    {
        get { return playerCount; }
        set { playerCount = value; }
    }
}
