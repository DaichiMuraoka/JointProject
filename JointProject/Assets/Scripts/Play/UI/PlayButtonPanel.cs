using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonPanel : Panel
{
    [SerializeField]
    private RetryOrNextButton retryOrNextButton = null;

    [SerializeField]
    private GameObject TutorialRoot = null;

    private void Start()
    {
        SetActive(false);
        TutorialRoot.SetActive(false);
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
