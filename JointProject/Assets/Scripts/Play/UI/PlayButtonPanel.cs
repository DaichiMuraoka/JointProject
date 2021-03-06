using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonPanel : Panel
{
    [SerializeField]
    private RetryOrNextButton retryOrNextButton = null;

    private void Start()
    {
        SetActive(false);
    }

    public void Open(SIDE side)
    {
        SetActive(true);
        retryOrNextButton.Init(side);
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
}
