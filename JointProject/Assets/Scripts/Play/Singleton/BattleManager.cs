using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            tank.GetComponent<PlayerController>().ID = playerList.Count + 1;
            Vector3 pos = tank.transform.position;
            if(ModeSettingLoader.Instance.ModeSetting.PlayMode == PLAY_MODE.LOCAL)
            {
                //カメラを着ける
                Vector3 cameraPos = new Vector3(pos.x, playerCameraPrefab.transform.position.y, pos.z - 3);
                Quaternion rotate = playerCameraPrefab.transform.rotation;
                Camera playerCamera = Instantiate(playerCameraPrefab, cameraPos, rotate, tank.transform);
                tank.Camera = playerCamera;
                //2人プレイ
                if (playerList.Count == 1)
                {
                    playerList[0].Camera.rect = new Rect(0f, 0f, 0.5f, 1f);
                    tank.Camera.rect = new Rect(0.5f, 0f, 1f, 1f);
                }
            }
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
        LoadAllTanks();
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        timeCounter.SetTime(60f);
        yield return new WaitForSeconds(3f);
        GameStart();
    }

    private void LoadAllTanks()
    {
        List<GameObject> tanks = new List<GameObject>();
        tanks.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        tanks.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach(GameObject tank in tanks)
        {
            tank.GetComponent<Tank>().AddTankList();
        }
    }

    private bool gameOver = false;

    public void GameOver(SIDE side)
    {
        if (gameOver) return;
        gameOver = true;
        if (side == SIDE.PLAYER)
        {
            foreach (Tank enemy in enemyList)
            {
                enemy.GetComponent<Controller>().State = MOVE_STATE.FREEZE;
            }
            Debug.Log("you lose.");
        }
        else if(side == SIDE.ENEMY)
        {
            foreach (Tank player in playerList)
            {
                player.GetComponent<Controller>().State = MOVE_STATE.FREEZE;
            }
            Debug.Log("you win!");
        }
        announcePanel.GameOver(side);
        StartCoroutine(LoadSceneCoroutine(3f));
    }

    [SerializeField]
    private AnnouncePanel announcePanel = null;

    [SerializeField]
    private TimeCounter timeCounter = null;

    private void GameStart()
    {
        announcePanel.GameStart();
        timeCounter.StartCountDown();
        foreach (Tank tank in playerList)
        {
            tank.GetComponent<Controller>().State = MOVE_STATE.MOVE;
        }
        foreach (Tank tank in enemyList)
        {
            tank.GetComponent<Controller>().State = MOVE_STATE.MOVE;
        }
    }

    private IEnumerator LoadSceneCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject mapparts = GameObject.FindGameObjectWithTag("MapParts");
        Destroy(mapparts.transform.parent.gameObject);
        SceneManager.LoadScene("LevelSelect");
    }
}
