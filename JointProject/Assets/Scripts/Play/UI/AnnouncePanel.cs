using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncePanel : Panel
{
    [SerializeField]
    private TextMeshProUGUI announcement = null;

    private string gameStart = "GAME START!";

    private string win = "WIN!!";

    private string lose = "LOSE...";

    private void Start()
    {
        SetActive(false);
    }

    public void GameStart()
    {
        announcement.text = gameStart;
        SetActive(true);
        StartCoroutine(ClosePanel(2f));
    }

    public void GameOver(SIDE side)
    {
        if (side == SIDE.PLAYER)
        {
            announcement.text = lose;
        }
        else if(side == SIDE.ENEMY)
        {
            announcement.text = win;
        }
        SetActive(true);
    }

    private IEnumerator ClosePanel(float time)
    {
        yield return new WaitForSeconds(time);
        SetActive(false);
    }
}
