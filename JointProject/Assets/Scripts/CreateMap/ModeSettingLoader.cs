using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSettingLoader : Singleton<ModeSettingLoader>
{
    [SerializeField]
    private ModeSetting modeSetting = null;

    public ModeSetting ModeSetting
    {
        get { return modeSetting; }
    }
}
