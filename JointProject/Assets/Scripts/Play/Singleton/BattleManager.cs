using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField]
    private List<Tank> playerList = new List<Tank>();

    public List<Tank> PlayerList
    {
        get { return playerList; }
    }

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
            Vector3 pos = tank.transform.position;
            //カメラを着ける
            Instantiate
                (
                playerCameraPrefab,
                new Vector3(pos.x, playerCameraPrefab.transform.position.y, pos.z - 3),
                playerCameraPrefab.transform.rotation,
                tank.transform
                );
        }
        else
        {
            enemyList.Add(tank);
        }
    }

    public void DeleteTankList(Tank tank)
    {
        if (playerList.Contains(tank))
        {
            playerList.Remove(tank);
        }
        else if (enemyList.Contains(tank))
        {
            enemyList.Remove(tank);
        }
        CheckGameOver();
    }

    [SerializeField]
    Camera playerCameraPrefab = null;

    private void CheckGameOver()
    {
        if(playerList.Count == 0)
        {
            GameOver(SIDE.PLAYER);
        }
        else if(enemyList.Count == 0)
        {
            GameOver(SIDE.ENEMY);
        }
    }

    private void Start()
    {
        LoadMap();
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3f);
        foreach (Tank tank in playerList)
        {
            tank.GetComponent<Controller>().State = MOVE_STATE.MOVE;
        }
        foreach (Tank tank in enemyList)
        {
            tank.GetComponent<Controller>().State = MOVE_STATE.MOVE;
        }
    }

    [SerializeField]
    private MapDeliverer mapDeliverer = null;

    private void LoadMap()
    {
        if(mapDeliverer.Map != null)
        {
            Instantiate(mapDeliverer.Map);
        }
        else
        {
            Debug.LogError("MapDeliverer.Map is null.");
        }
    }

    private void GameOver(SIDE side)
    {
        foreach(Tank player in playerList)
        {
            player.GetComponent<Controller>().State = MOVE_STATE.FREEZE;
        }
        foreach(Tank enemy in enemyList)
        {
            enemy.GetComponent<Controller>().State = MOVE_STATE.FREEZE;
        }
        if (side == SIDE.PLAYER)
        {
            Debug.Log("you lose.");
        }
        else if(side == SIDE.ENEMY)
        {
            Debug.Log("you win!");
        }
    }
}
