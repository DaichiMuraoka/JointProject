using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : Singleton<SaveDataManager>
{
    public Progress LoadProgress()
    {
        Progress progress;
        if (PlayerPrefs.HasKey("progress"))
        {
            string json = PlayerPrefs.GetString("progress");
            progress = JsonUtility.FromJson<Progress>(json);
        }
        else
        {
            progress = new Progress();
        }
        return progress;
    }

    public void LevelClear(int _level)
    {
        Progress progress;
        if (PlayerPrefs.HasKey("progress"))
        {
            string json = PlayerPrefs.GetString("progress");
            progress = JsonUtility.FromJson<Progress>(json);
        }
        else
        {
            progress = new Progress();
        }

        //クリアデータ挿入
        progress.list.Add(new ProgressData { level = _level, clear = true });

        //json化
        string json_saved = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString("progress", json_saved);

    }
}

[Serializable]
public class Progress
{
    public List<ProgressData> list = new List<ProgressData>();
}

[Serializable]
public class ProgressData
{
    public int level = 0;
    public bool clear = false;
}
