using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncePanel : Panel
{
    [SerializeField]
    private TextMeshProUGUI announcement = null;

    [SerializeField]
    private GameObject gameClearPrefab = null;

    [SerializeField]
    private GameObject gameoverPrefab = null;

    private string gameStart = "GAME START!";

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
        announcement.text = "";
        if (side == SIDE.PLAYER)
        {
            Instantiate(gameoverPrefab, Root);
        }
        else if(side == SIDE.ENEMY)
        {
            Instantiate(gameClearPrefab, Root);
        }
        SetActive(true);
    }

    private IEnumerator ClosePanel(float time)
    {
        yield return new WaitForSeconds(time);
        SetActive(false);
    }
}
