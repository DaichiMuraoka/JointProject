using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonPanel : Panel
{
    [SerializeField]
    private RetryOrNextButton retryOrNextButton = null;

    [SerializeField]
    private GameObject TutorialRoot = null;
    
    private GameObject LevelOriginal;
    private GameObject LevelCopy;
    
    private void Awake()
    {
        //バトルからリトライ の準備
        //マップ(Level)を複製して非アクティブにしておく
        
        //レベルの取得
        LevelOriginal = GameObject.FindGameObjectWithTag("MapParts").transform.parent.gameObject;
        //一度レベルを非アクティブにする
        LevelOriginal.SetActive(false);
        //複製を生成
        LevelCopy = Clone(LevelOriginal);
        
        //レベルをアクティブにする
        LevelOriginal.SetActive(true);
    }

    private void Start()
    {
        //このパネルを非アクティブにする
        SetActive(false);
        TutorialRoot.SetActive(false);
    }
    
    //複製したオブジェクトを返す
    public GameObject Clone( GameObject go )
    {
        var clone = GameObject.Instantiate( go ) as GameObject;
        clone.transform.parent = go.transform.parent;
        clone.transform.localPosition = go.transform.localPosition;
        clone.transform.localScale = go.transform.localScale;
        return clone;
    }
   
    public void Open(SIDE side)
    {
        if(ModeSettingLoader.Instance.ModeSetting.PlayMode == PLAY_MODE.TUTORIAL)
        {
            TutorialRoot.SetActive(true);
        }
        else
        {
            SetActive(true);
            retryOrNextButton.Init(side);
        }
    }

    public void OnClickRetry()
    {
        BattleManager.Instance.LoadScene(PLAY_TO_NEXT.RETRY);
    }
    
    public void OnClickBattleRetry()
    {
        //元のマップを削除
        Destroy(LevelOriginal);
        
        //PlayerTankAdjusterのカウントを初期化しておく
        PlayerTankAdjuster.Instance.InitCount();
        //レベルをアクティブにする
        LevelCopy.SetActive(true);
        DontDestroyOnLoad(LevelCopy);
        
        SceneManager.LoadScene("Play");
    }

    public void OnClickNext()
    {
        BattleManager.Instance.LoadScene(PLAY_TO_NEXT.NEXTLEVEL);
    }

    public void OnClickLevel()
    {
        BattleManager.Instance.LoadScene(PLAY_TO_NEXT.LEVELSELECT);
    }

    public void OnClickHome()
    {
        BattleManager.Instance.LoadScene(PLAY_TO_NEXT.HOME);
    }
}
