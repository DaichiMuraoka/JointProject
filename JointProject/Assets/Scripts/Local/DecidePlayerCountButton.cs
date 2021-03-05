using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecidePlayerCountButton : MonoBehaviour
{
    [SerializeField]
    private int playerCount = 1;

    public void OnClick()
    {
        ModeSettingLoader.Instance.ModeSetting.PlayerCount = playerCount;
    }
}
