using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSetter : MonoBehaviour
{
    [SerializeField]
    private PLAY_MODE playMode = PLAY_MODE.LOCAL;
    
    public void OnClick()
    {
        ModeSettingLoader.Instance.ModeSetting.PlayMode = playMode;
    }
}
