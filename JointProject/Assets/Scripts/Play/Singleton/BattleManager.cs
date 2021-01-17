using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField]
    private List<Tank> playerList = new List<Tank>();

    [SerializeField]
    private List<Tank> enemyList = new List<Tank>();

    public void AddTankList(Tank tank)
    {
        SIDE side = tank.Side;
        if(side == SIDE.NONE)
        {
            Debug.LogError("side is none");
        }
        else if(side == SIDE.PLAYER)
        {
            playerList.Add(tank);
        }
        else
        {
            enemyList.Add(tank);
        }
    }
}
