using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankAdjuster : Singleton<PlayerTankAdjuster>
{
    private int tankCount = 0;

    public void Adjust(PlayerController player)
    {
        tankCount++;
        if(tankCount > ModeSettingLoader.Instance.ModeSetting.PlayerCount)
        {
            player.gameObject.SetActive(false);
        }
    }
}
