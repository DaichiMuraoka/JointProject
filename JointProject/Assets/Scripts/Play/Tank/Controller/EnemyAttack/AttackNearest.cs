using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNearest : EnemyAttack
{
    [SerializeField]
    private FIRE_TYPE fireType = FIRE_TYPE.NOMAL;

    [SerializeField]
    private float interval = 2f;

    private float time = 0f;

    private float fireCoolTime = 0f;

    public override void AttackPerFrame()
    {
        //一定間隔で回転→発射
        time += Time.deltaTime;
        if(time >= interval)
        {
            controller.State = MOVE_STATE.ATTACK;
            Vector3 targetPos = GetNearestPlayerPos();
            if (enemyMove.Rotate(targetPos))
            {
                time = 0f;
                fireManager.Fire(fireType);
            }
        }
        
    }

    private Vector3 GetNearestPlayerPos()
    {
        List<Tank> playerList = BattleManager.Instance.PlayerList;
        if(playerList.Count == 0)
        {
            Debug.Log("player is none.");
            return new Vector3(0, 0, 0);
        }
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
