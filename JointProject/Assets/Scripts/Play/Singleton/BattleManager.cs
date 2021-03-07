using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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
            if(ModeSettingLoader.Instance.ModeSetting.PlayMode != PLAY_MODE.ONLINE)
            {
                Vector3 pos = tank.transform.position;
                Vector3 forward = tank.transform.forward;
                Vector3 cameraPosXZ = pos - 3f * forward;
                //カメラを着ける
                Vector3 cameraPos = new Vector3(cameraPosXZ.x,
                    playerCameraPrefab.transform.position.y,
                    cameraPosXZ.z);
                Camera playerCamera = Instantiate(playerCameraPrefab, cameraPos, Quaternion.identity, tank.transform);
                Vector3 lookPos = pos + 3f * forward;
                playerCamera.transform.LookAt(lookPos);
                tank.Camera = playerCamera;
                //2人プレイ
                if (playerList.Count == 1)
                {
                    playerList[0].Camera.rect = new Rect(0f, 0f, 0.5f, 1f);
                    tank.Camera.rect = new Rect(0.5f, 0f, 1f, 1f);
                }
            }
            playerList.Add(tank);
            Debug.Log(tank.name + "added.");
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
            Debug.Log(tank.name);
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
                Debug.Log(enemy.name + " freeze");
                enemy.GetComponent<Controller>().State = MOVE_STATE.FREEZE;
            }
            Debug.Log("you lose.");
        }
        else if(side == SIDE.ENEMY)
        {
            foreach (Tank player in playerList)
            {
                Debug.Log(player.name + " freeze");
                player.GetComponent<Controller>().State = MOVE_STATE.FREEZE;
                player.gameObject.GetComponent<Animator>().SetBool("win", true);
            }
            int level = mapDeliverer.Level;
            SaveDataManager.Instance.LevelClear(level);
            Debug.Log("you win!");
        }
        howToPanel.Close();
        announcePanel.GameOver(side);
        playButtonPanel.Open(side);
    }

    [SerializeField]
    private MapDeliverer mapDeliverer = null;

    public bool IsLevelMax
    {
        get
        {
            return mapDeliverer.Level == mapDeliverer.LevelMax;
        }
    }

    [SerializeField]
    private HowToPanel howToPanel = null;

    [SerializeField]
    private AnnouncePanel announcePanel = null;

    [SerializeField]
    private PlayButtonPanel playButtonPanel = null;

    [SerializeField]
    private TimeCounter timeCounter = null;

    private void GameStart()
    {
        announcePanel.GameStart();
        bool isLocalCount2 = false;
        int playerCount = ModeSettingLoader.Instance.ModeSetting.PlayerCount;
        PLAY_MODE mode = ModeSettingLoader.Instance.ModeSetting.PlayMode;
        if (playerCount == 2 && mode == PLAY_MODE.LOCAL)
        {
            isLocalCount2 = true;
        }
        howToPanel.Open(isLocalCount2);
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

    public void LoadScene(PLAY_TO_NEXT nextScene)
    {
        string sceneName;
        if(nextScene == PLAY_TO_NEXT.LEVELSELECT)
        {
            sceneName = "LevelSelect";
        }
        else if(nextScene == PLAY_TO_NEXT.NEXTLEVEL)
        {
            sceneName = "CreateMap";
            mapDeliverer.Level++;
        }
        else if(nextScene == PLAY_TO_NEXT.RETRY)
        {
            sceneName = "CreateMap";
        }
        else
        {
            sceneName = "Home";
        }
        GameObject mapparts = GameObject.FindGameObjectWithTag("MapParts");
        Destroy(mapparts.transform.parent.gameObject);
        SceneManager.LoadScene(sceneName);
    }
}

public enum PLAY_TO_NEXT
{
    LEVELSELECT,
    NEXTLEVEL,
    RETRY,
    HOME
}
