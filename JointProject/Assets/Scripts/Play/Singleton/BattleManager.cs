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
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3f);
        foreach(Tank tank in enemyList)
        {
            tank.GetComponent<Controller>().State = MOVE_STATE.MOVE;
        }
    }

    private void GameOver(SIDE side)
    {
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
