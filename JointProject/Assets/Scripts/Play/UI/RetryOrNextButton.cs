﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryOrNextButton : MonoBehaviour
{
    [SerializeField]
    private Button retryButton = null;
    
    [SerializeField]
    private Button battleRetryButton = null;

    [SerializeField]
    private Button nextButton = null;

    public void Init(SIDE side)
    {
        if(side == SIDE.PLAYER)
        {
            Retry();
        }
        else
        {
            if (!BattleManager.Instance.IsLevelMax)
            {
                Next();
            }
        }
    }

    private void Retry()
    {
        retryButton.gameObject.SetActive(true);
        battleRetryButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }

    private void Next()
    {
        retryButton.gameObject.SetActive(false);
        battleRetryButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    
}
