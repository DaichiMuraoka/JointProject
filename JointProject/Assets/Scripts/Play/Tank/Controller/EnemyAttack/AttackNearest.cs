using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNearest : EnemyAttack
{
    public override void AttackPerFrame()
    {
        //一定間隔で回転→発射
    }

    public override void CopyOtherEnemyAttack(EnemyAttack ea) { }

    private Vector3 GetNearestPlayerPos()
    {
        List<Tank> playerList = BattleManager.Instance.PlayerList;
        Tank nearestPlayer = null;
        foreach(Tank player in playerList)
        {
            if (nearestPlayer == null)
            {
                nearestPlayer = player;
            }
            else
            {
                if (Vector3.Distance(player.transform.position, transform.position)
                    > Vector3.Distance(nearestPlayer.transform.position, transform.position))
                {
                    nearestPlayer = player;
                }
            }
        }
        return nearestPlayer.transform.position;
    }
}
