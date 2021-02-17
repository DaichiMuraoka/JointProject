using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ModeSetting")]
public class ModeSetting : ScriptableObject
{
    [SerializeField]
    private int playerCount = 1;

    public int PlayerCount
    {
        get { return playerCount; }
        set { playerCount = value; }
    }

    [SerializeField]
    private PLAY_MODE playMode = PLAY_MODE.LOCAL;

    public PLAY_MODE PlayMode
    {
        get { return playMode; }
        set { playMode = value; }
    }
}

public enum PLAY_MODE
{
    LOCAL,
    ONLINE
}
