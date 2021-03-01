using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private TextMeshProUGUI text = null;

    private float currentTime = 0f;

    private bool counting = false;

    public void SetTime(float time)
    {
        currentTime = time;
    }

    public void StartCountDown()
    {
        counting = true;
    }

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (counting)
        {
            currentTime -= Time.deltaTime;
        }
        text.text = "残り時間:" + Mathf.CeilToInt(currentTime).ToString();
        if(currentTime <= 0f)
        {
            counting = false;
            BattleManager.Instance.GameOver(SIDE.ENEMY);
        }
    }
}
