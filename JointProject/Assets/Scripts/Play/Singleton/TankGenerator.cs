using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenerator : Singleton<TankGenerator>
{
    [SerializeField]
    private Tank tankPrefab = null;

    [SerializeField]
    private Camera playerCameraPrefab = null;

    public void Generate(SIDE side, StartPosition startPosition)
    {
        Vector3 pos = startPosition.transform.position;
        Tank tank = Instantiate(tankPrefab, pos, Quaternion.identity);
        tank.Side = side;
        if(side == SIDE.PLAYER)
        {
            //カメラを着ける
            Instantiate
                (
                playerCameraPrefab,
                new Vector3(pos.x, playerCameraPrefab.transform.position.y, pos.z - 3),
                playerCameraPrefab.transform.rotation,
                tank.transform
                );
        }
        BattleManager.Instance.AddTankList(tank);
    }
}

public enum SIDE
{
    NONE,
    PLAYER,
    ENEMY
}
